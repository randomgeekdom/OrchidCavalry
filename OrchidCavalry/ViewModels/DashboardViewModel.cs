using OrchidCavalry.Models;
using OrchidCavalry.Services;

namespace OrchidCavalry.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private Game game;

        public string CommanderName => $"{this.Game?.Commander?.GetName()}";

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
    }
}