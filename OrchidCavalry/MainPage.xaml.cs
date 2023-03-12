using OrchidCavalry.Popups;
using OrchidCavalry.Services;

namespace OrchidCavalry;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage(IGameSaver gameSaver)
	{
		InitializeComponent();
    }

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);



        Navigation.PushModalAsync(new NewGame(), true);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}

