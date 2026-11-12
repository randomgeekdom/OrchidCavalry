using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using OrchidCavalry.Popups;
using OrchidCavalry.Services;
using OrchidCavalry.ViewModels;

namespace OrchidCavalry;

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
                fonts.AddFont("GreatVibes-Regular.ttf", "GreatVibes");
            });

#if WINDOWS
        // MAUI gives the window a Mica backdrop, which shows through its title bar as WinUI grey.
        // Drop it and paint the window root in Night (#0C0910) so the title bar matches the app.
        builder.ConfigureLifecycleEvents(events => events.AddWindows(windows => windows.OnWindowCreated(window =>
        {
            window.SystemBackdrop = null;

            if (window.Content is Microsoft.UI.Xaml.Controls.Panel root)
            {
                root.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.ColorHelper.FromArgb(0xFF, 0x0C, 0x09, 0x10));
            }
        })));
#endif

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Register();

        var app = builder.Build();

		return app;
	}

	
}
