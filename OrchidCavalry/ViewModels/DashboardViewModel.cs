using OrchidCavalry.Models;
using System.Windows.Input;
using OrchidCavalry.Services;
using CommunityToolkit.Mvvm.Input;

namespace OrchidCavalry.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IGameplayService gameplayService;
        private readonly IAlertService alertService;
        private Game game;

        public DashboardViewModel(IGameplayService gameplayService, IAlertService alertService)
        {
            this.gameplayService = gameplayService;
            this.alertService = alertService;
            this.EndTurnCommand = new Command(NextTurn);
        }

        public string CommanderName => $"{this.Game?.Commander?.GetName()}";

        public ICommand EndTurnCommand { get; set; }

        public Game Game
        {
            get => this.game;
            set
            {
                this.game = value;
                this.NotifyAll();
            }
        }

        public string EndTurnText
        {
            get
            {
                if (this.game?.Alerts.Any() == true)
                {
                    return $"Alerts ({game.Alerts.Count})";
                }
                else { return "Next Turn"; }
            }
        }

        public void LoadGame(Game game)
        {
            this.Game = game;
        }

        public void NextTurn()
        {
            if (this.game.Alerts.Any())
            {
                var alert = this.game.Alerts[0];
                this.alertService.DisplayAlert(alert.Message, alert.Header);
                this.game.Alerts.Remove(alert);
            }
            else
            {
                this.gameplayService.NextTurn(game);
            }

            OnPropertyChanged(string.Empty);
        }
    }
}