using OrchidCavalry.Models;
using OrchidCavalry.Services;

namespace OrchidCavalry.ViewModels
{
    public class NewGameViewModel : ViewModelBase
    {
        private readonly IGameSaver gameSaver;
        private readonly IUnitService unitService;
        private string characterName;

        public NewGameViewModel(IGameSaver gameSaver, IUnitService unitService)
        {
            this.gameSaver = gameSaver;
            this.unitService = unitService;
        }

        public bool CanStart => !string.IsNullOrWhiteSpace(this.CharacterName);

        public string CharacterName
        {
            get => characterName;
            set
            {
                characterName = value;
                this.NotifyAll();
            }
        }

        public Game Game { get; private set; }

        public void Start()
        {
            if (CanStart)
            {
                var startingCharacterName = this.CharacterName.Trim();
                var character = new Character(startingCharacterName, "Orchid", 20 * 365);
                this.Game = new Game(character)
                {
                    Units = new List<Unit>
                    {
                        this.unitService.GetStarterUnit()
                    }
                };

                this.gameSaver.SaveGame(this.Game);
            }
        }
    }
}