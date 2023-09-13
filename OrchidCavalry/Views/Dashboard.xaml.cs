using OrchidCavalry.Models;
using OrchidCavalry.Popups;
using OrchidCavalry.Services;
using OrchidCavalry.ViewModels;

namespace OrchidCavalry.Views;

public partial class Dashboard : ContentPage
{
    public Dashboard(DashboardViewModel dashboardViewModel)
    {
        InitializeComponent();
        this.BindingContext = dashboardViewModel;
        this.DashboardViewModel = dashboardViewModel;
        this.DashboardViewModel.SetNavigation(this.Navigation);
    }

    public DashboardViewModel DashboardViewModel { get; }
}