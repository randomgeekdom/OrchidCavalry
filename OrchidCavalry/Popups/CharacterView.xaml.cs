using OrchidCavalry.Models;
using OrchidCavalry.ViewModels;

namespace OrchidCavalry.Popups;

public partial class CharacterView : ContentPage
{
    public CharacterView()
	{
		InitializeComponent();
    }

    internal void LoadViewModel(Character character, Game game)
    {
            this.BindingContext = new CharacterViewModel(character, game);
    }
}