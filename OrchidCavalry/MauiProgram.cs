using Microsoft.Extensions.Logging;
using OrchidCavalry.Services;

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
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddTransient<IGameSaver, GameSaver>();
		
		builder.Services.AddSingleton<AppShell>();
		builder.Services.AddSingleton<MainPage>();

		var app = builder.Build();

		return app;
	}
}
