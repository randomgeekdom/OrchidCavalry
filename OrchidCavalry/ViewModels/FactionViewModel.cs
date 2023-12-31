using OrchidCavalry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.ViewModels
{
    public class FactionViewModel(Game game) : ViewModelBase
    {
        private readonly Game game = game;
        private string selectedFaction;

        public List<string> Factions => game.Cities.Select(x => x.RulingFaction).OrderBy(x => x).Distinct().ToList();

        public string SelectedFaction
        {
            get => selectedFaction;
            set
            {
                selectedFaction = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public int SelectedFactionReputation
        {
            get
            {
                game.FactionReputation.TryGetValue(SelectedFaction ?? "", out var reputation);
                return reputation;
            }
        }
    }
}