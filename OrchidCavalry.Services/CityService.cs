using OrchidCavalry.Domain;
using OrchidCavalry.Models;
using Rollbard.Library.Rollers.Interfaces;

namespace OrchidCavalry.Services
{
    public class CityService : ICityService
    {
        public CityService(IMunicipalityNameRoller nameRoller, IFactionRoller factionRoller)
        {
            this.nameRoller = nameRoller;
            this.factionRoller = factionRoller;
        }
        private readonly Random random = new();
        private readonly IMunicipalityNameRoller nameRoller;
        private readonly IFactionRoller factionRoller;

        public City GetRandomCity(Game game)
        {
            var cityCount = game.Cities.Count;
            var randomIndex = this.random.Next(cityCount + 2);
            if (randomIndex >= cityCount - 1)
            {
                var factions = game.Cities.Select(x => x.RulingFaction).Distinct().ToList();
                var factionCount = factions.Count;
                var factionIndex = this.random.Next(factionCount + 2);

                var faction = factionIndex >= factionCount - 1 ? factionRoller.Get() : factions[factionIndex];
                return game.AddNewCity(this.nameRoller.Get(), faction);
            }
            else
            {
                return game.Cities[randomIndex];
            }
        }
    }
}
