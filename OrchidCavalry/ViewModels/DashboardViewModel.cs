using OrchidCavalry.Models;
using System.Windows.Input;
using OrchidCavalry.Services;
using CommunityToolkit.Mvvm.Input;

namespace OrchidCavalry.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IGameplayService gameplayService;

        private Game game;

        public DashboardViewModel(IGameplayService gameplayService)
        {
            this.gameplayService = gameplayService;
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

        public void LoadGame(Game game)
        {
            this.Game = game;

            this.EndTurnCommand = new AsyncRelayCommand(async () => await this.gameplayService.NextTurnAsync(this.game));
            OnPropertyChanged(string.Empty);
        }
    }
}