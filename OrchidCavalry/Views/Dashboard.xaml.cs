using OrchidCavalry.Popups;
using OrchidCavalry.ViewModels;

namespace OrchidCavalry.Views;

public partial class Dashboard : ContentPage
{
    private readonly CharacterView characterView;

    public Dashboard(DashboardViewModel dashboardViewModel, CharacterView characterView)
    {
        InitializeComponent();
        this.BindingContext = dashboardViewModel;
        this.DashboardViewModel = dashboardViewModel;
        this.characterView = characterView;
    }

    public DashboardViewModel DashboardViewModel { get; }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        characterView.LoadViewModel(
            this.DashboardViewModel.Game.Commander,
            this.DashboardViewModel.Game);
        await Navigation.PushAsync(characterView, true);
    }

    //protected override bool OnBackButtonPressed()
    //{
    //    Navigation.PopAsync(true);
    //    return base.OnBackButtonPressed();
    //}
}