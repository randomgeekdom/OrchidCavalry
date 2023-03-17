using OrchidCavalry.Models;
using OrchidCavalry.Services;

namespace OrchidCavalry.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IGameSaver gameSaver;
        private readonly Game game;

        public DashboardViewModel(IGameSaver gameSaver)
        {
            this.gameSaver = gameSaver;

            this.game = this.gameSaver.LoadGame();
        }

        public string PlayerCharacterName => this.game.PlayerCharacter.Name;
    }
}