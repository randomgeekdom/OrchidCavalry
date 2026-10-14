using OrchidCavalry.Domain;
using OrchidCavalry.Models;

namespace OrchidCavalry.Services.Tests
{
    public class FactionReputationTests
    {
        [Fact]
        public void AddToFactionReputation_ByFaction_IncreasesReputation()
        {
            var game = new Game();
            var faction = game.AddFaction("Orchids");

            game.AddToFactionReputation(faction, 3);

            Assert.Equal(3, faction.OrchidCavalryReputation);
        }

        [Fact]
        public void AddToFactionReputation_ByGuid_IncreasesReputation()
        {
            var game = new Game();
            var faction = game.AddFaction("Orchids");

            game.AddToFactionReputation(faction.Id, -2);

            Assert.Equal(-2, faction.OrchidCavalryReputation);
        }

        [Fact]
        public void AddToFactionReputation_AccumulatesAcrossCalls()
        {
            var game = new Game();
            var faction = game.AddFaction("Orchids");

            game.AddToFactionReputation(faction, 2);
            game.AddToFactionReputation(faction.Id, 5);

            Assert.Equal(7, faction.OrchidCavalryReputation);
        }
    }
}