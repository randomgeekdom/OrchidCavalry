using OrchidCavalry.Models;
using OrchidCavalry.Services;

namespace OrchidCavalry.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IGameSaver gameSaver;
        private Game game;

        public DashboardViewModel(IGameSaver gameSaver)
        {
            this.gameSaver = gameSaver;
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

        public string PlayerCharacterName => $"Governor: {this.Game?.PlayerCharacter?.Name}";
        public string PlayerCharacterDetails => $"{this.GenderSymbol} Age: {this.Game?.PlayerCharacter?.Age}";

        public char GenderSymbol => this.Game?.PlayerCharacter?.Gender == Gender.Male ? '♂' : '♀';

        public void LoadGame(Game game)
        {
            this.Game = game;
        }
    }
}