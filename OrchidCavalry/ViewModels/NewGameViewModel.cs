using OrchidCavalry.Models;
using OrchidCavalry.Services;
using Rollbard.Library.Rollers.Interfaces;

namespace OrchidCavalry.ViewModels
{
    public class NewGameViewModel : ViewModelBase
    {
        private readonly IGameSaver gameSaver;
        private string characterName;

        public NewGameViewModel(IGameSaver gameSaver, INameRoller nameRoller)
        {
            this.gameSaver = gameSaver;

            this.CharacterName = nameRoller.GetFirstName();
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

        public string OpeningText => "Nearly a millenium ago, retired general Mastdon Van Orchid received a vision that an otherworldly threat was coming to destroy the world.  He used the vast wealth and connections that he obtained over the years to raise an army to fight back and, when the invasion came, he ultimately defeated the invaders.  It was unanimously agreed upon by all nations that Mastodon's army, which became known as the Orchid Cavalry, was to be designated as the world's protector.  Mastodon Van Orchid was given reign over an island where a castle was built to house his armies in the event of another invasion.  \r\n\r\n Over the centuries, with no external threat, the Orchid Cavalry's reputation and importance diminished.  The nations of the world moved on and saw no need to support the endeavor.  Orchid Castle became a skeleton of ruin and the island has become a place to house the unwanted from other nations.  However, you don't plan to let the Orchid Cavalry fade further into obscurity.  As the last scion of Mastodon Van Orchid, you plan to make the Orchid Cavalry a force to be reckoned with again because, you too have received a vision of something coming...";
    }
}