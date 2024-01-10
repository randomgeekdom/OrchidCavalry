using OrchidCavalry.Domain;
using OrchidCavalry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.ViewModels
{
    public class CitiesViewModel(Game game)
    {
        public string Title { get; } = "Cities";
        public Game Game { get; } = game;
        public IEnumerable<City> Cities => this.Game.Cities.OrderBy(x => x.Name);
    }
}
