namespace OrchidCavalry;

public partial class App : Application
{
	public App(AppShell appShell)
	{
		InitializeComponent();

		UserAppTheme = AppTheme.Dark;
		MainPage = appShell;
	}
}
