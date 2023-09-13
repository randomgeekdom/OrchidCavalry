using OrchidCavalry.Popups;
using OrchidCavalry.Services;
using OrchidCavalry.ViewModels;

namespace OrchidCavalry.Views;

public partial class Dashboard : ContentPage
{
    private readonly CharacterView characterView;
    private readonly ICharacterPopupService characterPopupService;

    public Dashboard(DashboardViewModel dashboardViewModel, CharacterView characterView, ICharacterPopupService characterPopupService)
    {
        InitializeComponent();
        this.BindingContext = dashboardViewModel;
        this.DashboardViewModel = dashboardViewModel;
        this.characterView = characterView;
        this.characterPopupService = characterPopupService;
    }

    public DashboardViewModel DashboardViewModel { get; }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await this.characterPopupService.ShowCharacterAsync(this.DashboardViewModel.Game.Commander, this.Navigation);
    }

    //protected override bool OnBackButtonPressed()
    //{
    //    Navigation.PopAsync(true);
    //    return base.OnBackButtonPressed();
    //}
}