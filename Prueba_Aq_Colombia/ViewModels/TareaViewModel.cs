using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Prueba_Aq_Colombia.DataAccess;
using Prueba_Aq_Colombia.DTOs;
using Prueba_Aq_Colombia.Utilidades;
using Prueba_Aq_Colombia.Modelos;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;


namespace Prueba_Aq_Colombia.ViewModels
{
    public partial class TareaViewModel : ObservableObject, IQueryAttributable
    {
        private HttpClient httpClient = new HttpClient();
        private readonly TareaDBContext _dbContext;

        [ObservableProperty]
        private TareaDto tareadto = new TareaDto();

        private ObservableCollection<string> opciones;
        public ObservableCollection<string> Opciones
        {
            get => opciones;
            set => SetProperty(ref opciones, value);
        }

        [ObservableProperty]
        private string titulopagina;

        private int idTarea;

        [ObservableProperty]
        private bool loading = false;

        public TareaViewModel(TareaDBContext context)
        {
            _dbContext = context;
            LoadOptionsAsync();
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString());
            idTarea = id;
            if (idTarea == 0)
            {
                Titulopagina = "Nuevo Empleado";
            }
            else
            {
                Titulopagina = "Editar Empleado";
                Loading = true;
                await Task.Run(async () =>
                {
                    var encontrado = await _dbContext.Tarea.FirstAsync(e => e.Id == idTarea);
                    Tareadto.Id = encontrado.Id;
                    Tareadto.Descripcion = encontrado.Descripcion;
                    Tareadto.Nombre = encontrado.Nombre;
                    Tareadto.Estado = encontrado.Estado;
                    Tareadto.Categoria = encontrado.Categoria;
                    MainThread.BeginInvokeOnMainThread(() => { Loading = false; });
                });
            }
        }

        private async void LoadOptionsAsync()
        {
            try
            {
                var uri = new Uri("https://eqtools.eqtax.com:8585/api/TechnicalTest");
                var options = await httpClient.GetFromJsonAsync<List<CategoriasDto>>(uri);
                if (options != null)
                {
                    Opciones = new ObservableCollection<string>(options.Select(opt => opt.Category));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"No se pudo cargar las opciones: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task Guardar()
        {
            Loading = true;
            TareaMensaje Mensaje = new TareaMensaje();

            await Task.Run(async () =>
            {
                try
                {
                    if (idTarea == 0)
                    {
                        var validador = new Validador();
                        var resultValidador = await validador.validadorDatos(Tareadto);

                        if (string.IsNullOrEmpty(resultValidador))
                        {
                            var tbTarea = new Tareas
                            {
                                Nombre = Tareadto.Nombre,
                                Categoria = Tareadto.Categoria,
                                Estado = Tareadto.Estado,
                                Descripcion = Tareadto.Descripcion,
                            };
                            _dbContext.Tarea.Add(tbTarea);
                            await _dbContext.SaveChangesAsync();

                            Tareadto.Id = tbTarea.Id;
                            Mensaje = new TareaMensaje
                            {
                                EsCrear = true,
                                Tarea = Tareadto
                            };
                        }
                        else
                        {
                            Loading = false;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await Shell.Current.DisplayAlert("Error", resultValidador, "OK");
                            });
                        }
                    }
                    else
                    {
                        var encontrar = await _dbContext.Tarea.FirstAsync(e => e.Id == idTarea);
                        encontrar.Nombre = Tareadto.Nombre;
                        encontrar.Descripcion = Tareadto.Descripcion;
                        encontrar.Estado = Tareadto.Estado;
                        encontrar.Categoria = Tareadto.Categoria;

                        Mensaje = new TareaMensaje
                        {
                            EsCrear = false,
                            Tarea = Tareadto
                        };
                        await _dbContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        Debug.WriteLine("Ocurrió un error: " + ex.Message);
                        await Shell.Current.DisplayAlert("Error", "Un error ha ocurrido. Por favor intente nuevamente.", "OK");
                    });
                }
            });

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (Mensaje.Tarea != null)
                {
                    Loading = false;
                    WeakReferenceMessenger.Default.Send(new TareaMensajeria(Mensaje));
                    await Shell.Current.Navigation.PopAsync();
                }
            });
        }
    }
}
