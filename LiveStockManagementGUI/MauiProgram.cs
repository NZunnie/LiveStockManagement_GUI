using Microsoft.Extensions.Logging;

namespace LiveStockManagementGUI
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
                    Console.WriteLine("test");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            //register the following services to make them available for the app
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<DataPage>();
            builder.Services.AddTransient<ReportPage>();
            builder.Services.AddTransient<SearchPage>();
            builder.Services.AddTransient<ManagementPage>();
            return builder.Build();
        }
    }
}
