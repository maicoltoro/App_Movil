using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Prueba_Aq_Colombia.DataAccess;
using Prueba_Aq_Colombia.DTOs;
using Prueba_Aq_Colombia.Utilidades;
using System.Collections.ObjectModel;
using Prueba_Aq_Colombia.Views;
namespace Prueba_Aq_Colombia.ViewModels
{
    public partial class MainViewModel:ObservableObject
    {
        private readonly TareaDBContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<TareaDto> listaTareas = new ObservableCollection<TareaDto>();

        public MainViewModel(TareaDBContext context)
        {
            _dbContext = context;

            MainThread.BeginInvokeOnMainThread(new Action(async () =>
            {
                await Obteber();
            }));

            WeakReferenceMessenger.Default.Register<TareaMensajeria>(this, (r,m) =>
            {
                TareaMensajeRecibido(m.Value);
            });
        }

        public async Task Obteber()
        {
            var lista = await _dbContext.Tarea.ToListAsync();
            if (lista.Any())
            {
                foreach(var item in lista)
                {
                    ListaTareas.Add(new TareaDto
                    {
                        Id = item.Id,
                        Estado = item.Estado,
                        Descripcion = item.Descripcion,
                        Nombre = item.Nombre,
                        Categoria = item.Categoria,
                    });
                }
            }
        }

        private void TareaMensajeRecibido( TareaMensaje tareamensaje)
        {
            var tareaDto = tareamensaje.Tarea;
            if (tareamensaje.EsCrear)
            {
                ListaTareas.Add(tareaDto);
            }
            else
            {
                var encontrado = ListaTareas.First(e => e.Id == tareaDto.Id);
                encontrado.Nombre = tareaDto.Nombre;
                encontrado.Descripcion = tareaDto.Descripcion;
                encontrado.Estado = tareaDto.Estado;
                encontrado.Categoria = tareaDto.Categoria;
            }
        }

        [RelayCommand]
        private async Task Crear()
        {
            var uri = $"{nameof(TareasPage)}?id=0";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Editar(TareaDto tareas)
        {
            var uri = $"{nameof(TareasPage)}?id={tareas.Id}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Eliminar(TareaDto tareas)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea eliminar la tarea?", "Si", "No");
            if (answer)
            {
                var encontrado = await _dbContext.Tarea.FirstAsync(e => e.Id == tareas.Id);

                _dbContext.Tarea.Remove(encontrado);
                await _dbContext.SaveChangesAsync();
                ListaTareas.Remove(tareas);
            }
        }
    }
}
