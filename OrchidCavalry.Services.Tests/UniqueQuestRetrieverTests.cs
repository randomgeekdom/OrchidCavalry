using OrchidCavalry.Domain;
using OrchidCavalry.Domain.Quests.Unique;
using OrchidCavalry.Domain.Services;
using OrchidCavalry.Models;

namespace OrchidCavalry.Services.Tests
{
    public class UniqueQuestRetrieverTests
    {
        private static (Game game, City city) CreateGame()
        {
            var game = new Game();
            var faction = game.AddFaction("TestFaction");
            var city = game.AddNewCity("Testville", faction.Id, "Forest");
            return (game, city);
        }

        [Fact]
        public void BitzDenniganQuest_HasTheSameIdOnEveryInstance()
        {
            Assert.Equal(new BitzDenniganQuest("A").Id, new BitzDenniganQuest("B").Id);
        }

        [Fact]
        public async Task GetUniqueQuestAsync_ReturnsBitzDenniganQuest_WhenAvailable()
        {
            var (game, city) = CreateGame();

            var quest = await new UniqueQuestRetriever().GetUniqueQuestAsync(game, city);

            Assert.IsType<BitzDenniganQuest>(quest);
        }

        [Fact]
        public async Task GetUniqueQuestAsync_SkipsCompletedQuest()
        {
            var (game, city) = CreateGame();
            var quest = new BitzDenniganQuest(city.Name);
            game.AddQuest(quest);
            game.RemoveQuest(quest);

            Assert.Null(await new UniqueQuestRetriever().GetUniqueQuestAsync(game, city));
        }

        [Fact]
        public async Task GetUniqueQuestAsync_SkipsQuestAlreadyOnTheBoard()
        {
            var (game, city) = CreateGame();
            game.AddQuest(new BitzDenniganQuest(city.Name));

            Assert.Null(await new UniqueQuestRetriever().GetUniqueQuestAsync(game, city));
        }
    }
}
