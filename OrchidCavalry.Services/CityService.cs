using OrchidCavalry.Domain;
using OrchidCavalry.Models;
using Rollbard.Library.Rollers.Interfaces;

namespace OrchidCavalry.Services
{
    public class CityService(IMunicipalityNameRoller nameRoller, IFactionRoller factionRoller) : ICityService
    {
        private readonly IFactionRoller factionRoller = factionRoller;
        private readonly IMunicipalityNameRoller nameRoller = nameRoller;
        private readonly Random random = new();

        public async Task<City> GetUnthreatenedRandomCityAsync(Game game)
        {
            return await Task.Run(() =>
            {
                // Get a list of cities that are not part of a quest
                var cities = game.Cities.Where(x => !game.Quests.Select(x => x.CityName).Contains(x.Name));

                var cityCount = cities.Count();
                var randomIndex = this.random.Next(cityCount + 2);
                if (randomIndex >= cityCount - 1)
                {
                    var factions = game.Factions;
                    var factionCount = factions.Count;
                    var factionIndex = this.random.Next(factionCount + 2);

                    var faction = factionIndex >= factionCount - 1 ? game.AddFaction(factionRoller.Get()) : factions[factionIndex];

                    return game.AddNewCity(this.nameRoller.Get(), faction.Id);
                }
                else
                {
                    return game.Cities[randomIndex];
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