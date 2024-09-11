using Microsoft.Extensions.Logging;
using Module02Exercise01.View;
using Module02Exercise01.Services;

namespace Module02Exercise01
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
                    fonts.AddFont("Nunito-Regular.ttf", "NunitoRegular");
                    fonts.AddFont("Nunito-Bold.ttf", "NunitoBold");
                });

#if DEBUG
            // Service
            builder.Services.AddSingleton<IMyService, MyService>();

            // ContentPage
            builder.Services.AddTransient<LoginPage>();

            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
