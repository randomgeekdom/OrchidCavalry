using OrchidCavalry.Models;
using OrchidCavalry.Services;

namespace OrchidCavalry.ViewModels
{
    public class NewGameViewModel : ViewModelBase
    {
        private readonly IGameSaver gameSaver;
        private string characterName;

        public NewGameViewModel(IGameSaver gameSaver)
        {
            this.gameSaver = gameSaver;
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

        public async Task StartAsync()
        {
            if (CanStart)
            {
                var startingCharacterName = this.CharacterName.Trim();
                var character = new Character(startingCharacterName, "Orchid");
                this.Game = new Game(character);

                await this.gameSaver.SaveGameAsync(this.Game);
            }
        }
    }
}