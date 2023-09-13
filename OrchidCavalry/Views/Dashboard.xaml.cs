using OrchidCavalry.Models;
using OrchidCavalry.Popups;
using OrchidCavalry.Services;
using OrchidCavalry.ViewModels;

namespace OrchidCavalry.Views;

public partial class Dashboard : ContentPage
{
    private readonly ICharacterPopupService characterPopupService;
    private readonly CharacterView characterView;

    public Dashboard(DashboardViewModel dashboardViewModel, CharacterView characterView, ICharacterPopupService characterPopupService)
    {
        InitializeComponent();
        this.BindingContext = dashboardViewModel;
        this.DashboardViewModel = dashboardViewModel;
        this.characterView = characterView;
        this.characterPopupService = characterPopupService;
    }

    public DashboardViewModel DashboardViewModel { get; }

    private async Task ShowCharacter(Character character)
    {
        await this.characterPopupService.ShowCharacterAsync(new CharacterPopupModel { Character = character, Navigation = this.Navigation });
    }

    private async void ShowCommander(object sender, EventArgs e)
    {
        await ShowCharacter(this.DashboardViewModel.Game.Commander);
    }
}