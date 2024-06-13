using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace MovieAppTest
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Poppins-Bold.ttf", "Bold");
                    fonts.AddFont("Poppins-BoldItalic.ttf", "BoldItalic");
                    fonts.AddFont("Poppins-SemiBold.ttf", "SemiBold");
                    fonts.AddFont("Poppins-SemiBoldItalic.ttf", "SemiBoldItalic");
                    fonts.AddFont("Poppins-Medium.ttf", "Medium");
                    fonts.AddFont("Poppins-MediumItalic.ttf", "MediumItalic");
                    fonts.AddFont("Poppins-Italic.ttf", "Italic");
                    fonts.AddFont("Poppins-Regular.ttf", "Regular");
                    fonts.AddFont("fa-solid-900.ttf", "Icon");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
