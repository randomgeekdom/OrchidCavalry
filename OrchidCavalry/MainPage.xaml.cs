using OrchidCavalry.Popups;
using OrchidCavalry.Services;

namespace OrchidCavalry;

public partial class MainPage : ContentPage
{
	int count = 0;
    private readonly IGameSaver gameSaver;
    private readonly NewGame newGame;

    public MainPage(IGameSaver gameSaver, NewGame newGame)
	{
		InitializeComponent();
        this.gameSaver = gameSaver;
        this.newGame = newGame;
    }

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
    }

    // change this to button press
    protected override void OnAppearing()
    {
        base.OnAppearing();

        var game = this.gameSaver.LoadGameAsync().Result;

        if (game == null)
        {
            var task = Navigation.PushModalAsync(this.newGame);
            task.Wait();
        }
    }
}

