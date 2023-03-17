using OrchidCavalry.ViewModels;

namespace OrchidCavalry.Views;

public partial class Dashboard : ContentPage
{
    public Dashboard(DashboardViewModel dashboardViewModel)
	{
		InitializeComponent();
        this.BindingContext = dashboardViewModel;
    }
}