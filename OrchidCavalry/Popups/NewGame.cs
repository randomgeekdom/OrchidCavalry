namespace OrchidCavalry.Popups;

public class NewGame : ContentPage
{
	public NewGame()
	{
		var button = new Button { Text = "Close" };
		button.Clicked += (s, e) => { Navigation.PopModalAsync(); };

        Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI! TEST TEST"
				},
		button		
			}
		};
	}
}