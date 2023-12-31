using OrchidCavalry.Models;
using OrchidCavalry.ViewModels;

namespace OrchidCavalry.Views;

public partial class FactionView : ContentPage, IGameView
{
	public FactionView()
	{
		InitializeComponent();
	}

	public void LoadGame(Game game)
	{
		this.BindingContext = new FactionViewModel(game);
	}
}