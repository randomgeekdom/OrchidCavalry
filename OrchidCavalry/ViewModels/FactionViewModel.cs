using OrchidCavalry.Domain;
using OrchidCavalry.Models;

namespace OrchidCavalry.ViewModels
{
    public class FactionViewModel(Game game) : ViewModelBase
    {
        private readonly Game game = game;
        private Faction selectedFaction;

        public List<Faction> Factions => [.. game.Factions];

        public Faction SelectedFaction
        {
            get => selectedFaction;
            set
            {
                selectedFaction = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int SelectedFactionReputation => this.SelectedFaction?.OrchidCavalryReputation ?? 0;
    }
}