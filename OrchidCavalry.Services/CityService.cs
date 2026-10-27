using OrchidCavalry.Domain;
using OrchidCavalry.Models;
using Rollbard.Library.Rollers.Interfaces;

namespace OrchidCavalry.Services
{
    public class CityService(IMunicipalityNameRoller nameRoller, IFactionRoller factionRoller, IBiomeRoller biomeRoller) : ICityService
    {
        private readonly IBiomeRoller biomeRoller = biomeRoller;
        private readonly IFactionRoller factionRoller = factionRoller;
        private readonly IMunicipalityNameRoller nameRoller = nameRoller;
        private readonly Random random = new();

        public async Task<City> GetUnthreatenedRandomCityAsync(Game game)
        {
            return await Task.Run(() =>
            {
                // Get a list of cities that are not part of a quest
                var cities = game.Cities.Where(x => !game.Quests.Select(x => x.CityName).Contains(x.Name)).ToList();

                var cityCount = cities.Count;
                var randomIndex = this.random.Next(cityCount + 2);
                if (randomIndex >= cityCount)
                {
                    var factions = game.Factions;
                    var factionCount = factions.Count;
                    var factionIndex = this.random.Next(factionCount + 2);

                    var faction = factionIndex >= factionCount ? game.AddFaction(factionRoller.Get()) : factions[factionIndex];

                    return game.AddNewCity(this.nameRoller.Get(), faction.Id, this.biomeRoller.Get());
                }
                else
                {
                    return cities[randomIndex];
                }
            });
        }

        public async Task IncreasePopulationAsync(IEnumerable<City> cities)
        {
            await Task.Run(() =>
            {
                foreach (var city in cities)
                {
                    city.Population += Math.Max((int)(city.Population * 0.00287671232), 1);
                }
            });
        }
    }
}