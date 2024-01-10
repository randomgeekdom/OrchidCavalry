using OrchidCavalry.Models;
using System.Windows.Input;
using OrchidCavalry.Services;
using CommunityToolkit.Mvvm.Input;
using OrchidCavalry.Domain.Quests;
using System.Runtime.CompilerServices;
using MvvmHelpers.Commands;
using OrchidCavalry.Views;

namespace OrchidCavalry.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IAlertService alertService;
        private readonly ICharacterPopupService characterPopupService;
        private readonly IChoicePopupService choicePopupService;
        private readonly IGameplayService gameplayService;
        private readonly IGameSaver gameSaver;
        private readonly IGameViewPopupService gameViewPopupService;
        private readonly IQuestPopupService questPopupService;
        private Game game;
        private INavigation navigation;

        public DashboardViewModel(IGameplayService gameplayService, IAlertService alertService, ICharacterPopupService characterPopupService, IQuestPopupService questPopupService, IChoicePopupService choicePopupService, IGameSaver gameSaver, IGameViewPopupService gameViewPopupService)
        {
            this.gameplayService = gameplayService;
            this.alertService = alertService;
            this.characterPopupService = characterPopupService;
            this.questPopupService = questPopupService;
            this.choicePopupService = choicePopupService;
            this.gameSaver = gameSaver;
            this.gameViewPopupService = gameViewPopupService;
            this.EndTurnCommand = new AsyncRelayCommand(this.NextTurnAsync);
            this.ShowCharacterPopupCommand = new AsyncRelayCommand<Models.Character>(x => this.ShowCharacterPopupAsync(x));
            this.ShowQuestPopupCommand = new AsyncRelayCommand(this.ShowQuestPopupAsync);
            this.ShowFactionPopupCommand = new AsyncRelayCommand(() => gameViewPopupService.ShowPopupAsync<FactionView>(this.game, this.navigation));
            this.ShowCitiesPopupCommand = new AsyncRelayCommand(() => gameViewPopupService.ShowPopupAsync<CitiesView>(this.game, this.navigation));

        }

        public bool AreQuestsAvailable => (game?.Quests?.Count ?? 0) > 0;

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

        public string QuestButtonText => $"Quest Available: {GetAvailableQuests()?.Count()}";

        public ICommand ShowCharacterPopupCommand { get; }

        public AsyncRelayCommand ShowQuestPopupCommand { get; }
        public AsyncRelayCommand ShowFactionPopupCommand { get; }
        public AsyncRelayCommand ShowCitiesPopupCommand { get; }

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

            await this.gameSaver.SaveGameAsync(game);

            this.EnableNextTurn = true;
            OnPropertyChanged(string.Empty);
        }

        public void SetNavigation(INavigation navigation)
        {
            this.navigation = navigation;
        }

        public async Task ShowCharacterPopupAsync(Character character)
        {
            await this.characterPopupService.ShowCharacterAsync(new CharacterPopupModel { Character = character, Navigation = this.navigation });
        }

        private IEnumerable<Quest> GetAvailableQuests() => this.game?.Quests;

        private async Task ShowQuestPopupAsync()
        {
            await this.questPopupService.ShowPopupAsync(this.game, this.navigation);
            OnPropertyChanged(string.Empty);
        }
    }
}