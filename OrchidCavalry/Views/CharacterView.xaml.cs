using OrchidCavalry.Models;
using OrchidCavalry.ViewModels;

namespace OrchidCavalry.Popups;

public partial class CharacterView : ContentPage
{
    public CharacterView()
    {
        InitializeComponent();
    }

    internal void LoadViewModel(Character character)
    {
        this.BindingContext = new CharacterViewModel(character);
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}