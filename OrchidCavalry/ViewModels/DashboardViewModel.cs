using OrchidCavalry.Models;
using System.Windows.Input;
using OrchidCavalry.Services;
using CommunityToolkit.Mvvm.Input;

namespace OrchidCavalry.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IAlertService alertService;
        private readonly ICharacterPopupService characterPopupService;
        private readonly IGameplayService gameplayService;
        private Game game;

        public DashboardViewModel(IGameplayService gameplayService, IAlertService alertService, ICharacterPopupService characterPopupService)
        {
            this.gameplayService = gameplayService;
            this.alertService = alertService;
            this.characterPopupService = characterPopupService;
            this.EndTurnCommand = new RelayCommand(async () => await this.NextTurnAsync());
        }

        public string CommanderName => $"{this.Game?.Commander?.GetName()}";

        public ICommand EndTurnCommand { get; set; }

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
        }

        public async Task NextTurnAsync()
        {
            if (this.game.Alerts.Any())
            {
                var alert = this.game.Alerts[0];
                await this.alertService.DisplayAlertAsync(alert.Message, alert.Header);
                this.game.Alerts.Remove(alert);
            }
            else
            {
                await this.gameplayService.NextTurnAsync(game);
            }

            OnPropertyChanged(string.Empty);
        }

        public async Task ShowCharacterPopup(CharacterPopupModel characterPopupModel)
        {
            await this.characterPopupService.ShowCharacterAsync(characterPopupModel);
        }
    }
}