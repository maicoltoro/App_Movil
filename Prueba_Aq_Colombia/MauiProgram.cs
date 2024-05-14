using Microsoft.Extensions.Logging;
using Prueba_Aq_Colombia.DataAccess;
using Prueba_Aq_Colombia.ViewModels;
using Prueba_Aq_Colombia.Views;

namespace Prueba_Aq_Colombia
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var dbContext = new TareaDBContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();

            builder.Services.AddDbContext<TareaDBContext>();

            builder.Services.AddTransient<TareasPage>();
            builder.Services.AddTransient<TareaViewModel>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            Routing.RegisterRoute(nameof(TareasPage), typeof(TareasPage));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
