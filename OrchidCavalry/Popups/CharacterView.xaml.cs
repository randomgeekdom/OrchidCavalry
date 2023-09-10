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
        if (this.BindingContext is not CharacterViewModel vm || vm.Character != character)
        {
            this.BindingContext = new CharacterViewModel(character, game);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}