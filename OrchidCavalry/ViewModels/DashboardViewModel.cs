using OrchidCavalry.Models;
using System.Windows.Input;
using OrchidCavalry.Services;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers.Commands;

namespace OrchidCavalry.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IAlertService alertService;
        private readonly ICharacterPopupService characterPopupService;
        private readonly IChoicePopupService choicePopupService;
        private readonly IGameplayService gameplayService;
        private Game game;
        private INavigation navigation;

        public DashboardViewModel(IGameplayService gameplayService, IAlertService alertService, ICharacterPopupService characterPopupService, IChoicePopupService choicePopupService)
        {
            this.gameplayService = gameplayService;
            this.alertService = alertService;
            this.characterPopupService = characterPopupService;
            this.choicePopupService = choicePopupService;
            this.EndTurnCommand = new AsyncRelayCommand(this.NextTurnAsync);
            this.ShowCharacterPopupCommand = new AsyncRelayCommand<Character>(x => this.ShowCharacterPopup(x));

            this.choicePopupService.ChoiceSelected += () => this.NotifyAll();
        }

        public Character Commander => this.Game?.Commander;
        public string CommanderName => $"{this.Game?.Commander?.GetName()}";
        public bool EnableNextTurn { get; set; } = true;
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

        public ICommand ShowCharacterPopupCommand { get; }

        public void LoadGame(Game game)
        {
            this.Game = game;
        }

        public async Task NextTurnAsync()
        {
            this.EnableNextTurn = false;
            OnPropertyChanged(string.Empty);

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

            this.EnableNextTurn = true;
            OnPropertyChanged(string.Empty);
        }

        public void SetNavigation(INavigation navigation)
        {
            this.navigation = navigation;
        }

        public async Task ShowCharacterPopup(Character character)
        {
            await this.characterPopupService.ShowCharacterAsync(new CharacterPopupModel { Character = character, Navigation = this.navigation });
        }
    }
}