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

        public Gender Gender { get; set; }

        public bool IsFemale
        {
            get => Gender == Gender.Female;
            set
            {
                Gender = value ? Gender.Female : Gender.Male;
                this.NotifyAll();
            }
        }

        public bool IsMale
        {
            get => Gender == Gender.Male;
            set
            {
                Gender = value ? Gender.Male : Gender.Female;
                this.NotifyAll();
            }
        }

        public void Start()
        {
            if (CanStart)
            {
                var startingCharacter = this.CharacterName.Trim();
                var game = new Game
                {
                    PlayerCharacter = new Character(startingCharacter, "Orchid", this.Gender, 20)
                };

                this.gameSaver.SaveGame(game);
            }
        }
    }
}