using OrchidCavalry.ViewModels;

namespace OrchidCavalry.Views;

public partial class Dashboard : ContentPage
{
    public Dashboard(DashboardViewModel dashboardViewModel)
	{
		InitializeComponent();
        this.BindingContext = dashboardViewModel;
        this.DashboardViewModel = dashboardViewModel;
    }

    public DashboardViewModel DashboardViewModel { get; }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}