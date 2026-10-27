using OrchidCavalry.Domain;
using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Models;

namespace OrchidCavalry.Services.Tests
{
    public class CityServiceTests
    {
        [Fact]
        public async Task GetUnthreatenedRandomCityAsync_NeverReturnsAThreatenedCity()
        {
            var game = new Game();
            var faction = game.AddFaction("TestFaction");
            var threatenedCity = game.AddNewCity("ThreatenedCity", faction.Id, "Plain");
            game.AddNewCity("SafeCityOne", faction.Id, "Plain");
            game.AddNewCity("SafeCityTwo", faction.Id, "Plain");
            game.AddQuest(MonsterQuest.Create("Goblin", threatenedCity.Name));

            var service = CreateCityService();

            for (var i = 0; i < 100; i++)
            {
                var city = await service.GetUnthreatenedRandomCityAsync(game);

                Assert.NotEqual(threatenedCity.Name, city.Name);
                Assert.DoesNotContain(city.Name, game.Quests.Select(x => x.CityName));
            }
        }

        [Fact]
        public async Task GetUnthreatenedRandomCityAsync_UsesTheOnlyUnthreatenedCity()
        {
            var game = new Game();
            var faction = game.AddFaction("TestFaction");
            var city = game.AddNewCity("OnlyCity", faction.Id, "Plain");

            var service = CreateCityService();

            var returnedCityNames = new List<string>();
            for (var i = 0; i < 50; i++)
            {
                returnedCityNames.Add((await service.GetUnthreatenedRandomCityAsync(game)).Name);
            }

            Assert.Contains(city.Name, returnedCityNames);
        }

        private static CityService CreateCityService()
        {
            return new CityService(new FakeMunicipalityNameRoller(), new FakeFactionRoller(), new FakeBiomeRoller());
        }
    }
}
