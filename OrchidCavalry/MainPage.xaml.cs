using OrchidCavalry.Popups;
using OrchidCavalry.Services;
using OrchidCavalry.Views;

namespace OrchidCavalry;

public partial class MainPage : ContentPage
{
    private readonly IGameSaver gameSaver;
    private readonly NewGame newGame;
    private readonly Dashboard dashboard;

    public MainPage(IGameSaver gameSaver, NewGame newGame, Dashboard dashboard)
	{
		InitializeComponent();
        this.gameSaver = gameSaver;
        this.newGame = newGame;
        this.dashboard = dashboard;
    }

    
    public void StartGame()
    {
        Navigation.PushAsync(dashboard);
    }

    private void StartButton_Clicked(object sender, EventArgs e)
    {
        var game = this.gameSaver.LoadGame();
        if (game == null)
        {
            newGame.Closed += this.StartGame;
            Navigation.PushModalAsync(newGame).Wait();
        }
        else
        {
            StartGame();
        }
    }
}

