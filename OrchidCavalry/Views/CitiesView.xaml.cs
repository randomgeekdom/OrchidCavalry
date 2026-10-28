using OrchidCavalry.Models;
using OrchidCavalry.ViewModels;

namespace OrchidCavalry.Views;

public partial class CitiesView : ContentPage, IGameView
{
	public CitiesView()
	{
		InitializeComponent();
	}

    public void LoadGame(Game game)
    {
		this.BindingContext = new CitiesViewModel(game);
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}