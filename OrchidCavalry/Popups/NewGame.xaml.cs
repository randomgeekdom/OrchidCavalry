using OrchidCavalry.ViewModels;
using System.ComponentModel;

namespace OrchidCavalry.Popups;

public partial class NewGame : ContentPage, INotifyPropertyChanged
{
    public NewGame()
    {
        InitializeComponent();

        this.BindingContext = new NewGameViewModel();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}