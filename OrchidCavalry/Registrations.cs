using OrchidCavalry.Popups;
using OrchidCavalry.Services;
using OrchidCavalry.ViewModels;
using OrchidCavalry.Views;

namespace OrchidCavalry;

public static class Registrations
{
    public static void Register(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton(builder);

        // Views
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<NewGame>();
        builder.Services.AddTransient<Dashboard>();
        builder.Services.AddTransient<CharacterView>();
        // View models
        builder.Services.AddTransient<NewGameViewModel>();
        builder.Services.AddTransient<DashboardViewModel>();

        // Services
        builder.Services.AddTransient<IGameSaver, GameSaver>();
        builder.Services.AddTransient<ICharacterService, CharacterService>();
        builder.Services.AddTransient<IUnitService, UnitService>();
        builder.Services.AddTransient<IGameSaver, GameSaver>();

        foreach (var entry in Rollbard.Library.RegistrationDictionary.Get())
        {
            builder.Services.AddTransient(entry.Key, entry.Value);
        }
    }
}