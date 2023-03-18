
using OrchidCavalry.Models;
using OrchidCavalry.ViewModels;
using System.ComponentModel;

namespace OrchidCavalry.Popups;

public partial class NewGame : ContentPage, INotifyPropertyChanged
{
    private readonly NewGameViewModel newGameViewModel;

    public NewGame(NewGameViewModel newGameViewModel)
    {
        InitializeComponent();

        this.BindingContext = newGameViewModel;
        this.newGameViewModel = newGameViewModel;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        this.newGameViewModel.Start();
        Navigation.PopModalAsync();
        this.Closed();
    }

    public Action Closed { get; set; }
    public Game Game => this.newGameViewModel.Game;
}