using OrchidCavalry.Models;
using OrchidCavalry.ViewModels;

namespace OrchidCavalry.Popups;

public partial class CharacterView : ContentPage
{
    private CharacterViewModel characterViewModel;

    public CharacterView()
	{
		InitializeComponent();
    }

    internal void LoadViewModel(Character playerCharacter, Game game)
    {
        this.BindingContext = this.characterViewModel = new CharacterViewModel(playerCharacter, game);
    }
}