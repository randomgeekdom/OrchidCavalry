using OrchidCavalry.Domain.Services;
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
        builder.Services.AddTransient<ChoiceView>();
        builder.Services.AddTransient<QuestView>();

        // View models
        builder.Services.AddTransient<NewGameViewModel>();
        builder.Services.AddTransient<DashboardViewModel>();

        // UI service
        builder.Services.AddTransient<IAlertService, AlertService>();
        builder.Services.AddTransient<IGameplayService, GameplayService>();
        builder.Services.AddTransient<ICharacterPopupService, CharacterPopupService>();
        builder.Services.AddTransient<IChoicePopupService, ChoicePopupService>();
        builder.Services.AddTransient<IQuestPopupService, QuestPopupService>();
        builder.Services.AddTransient<IGameViewPopupService, GameViewPopupService>();


        // Services
        builder.Services.AddTransient<IGameSaver, GameSaver>();
        builder.Services.AddTransient<ICharacterService, CharacterService>();
        builder.Services.AddTransient<ICityService, CityService>();
        builder.Services.AddTransient<IQuestService, QuestService>();

        // Model Services
        builder.Services.AddTransient<IDiceRoller, DiceRoller>();
        builder.Services.AddTransient<IUniqueQuestRetriever, UniqueQuestRetriever>();

        foreach (var entry in Rollbard.Library.RegistrationDictionary.Get())
        {
            builder.Services.AddTransient(entry.Key, entry.Value);
        }
    }
}