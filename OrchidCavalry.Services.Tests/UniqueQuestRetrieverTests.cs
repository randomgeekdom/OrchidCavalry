using OrchidCavalry.Domain;
using OrchidCavalry.Domain.Quests;
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
        public void FungalsaurQuest_HasTheSameIdOnEveryInstance()
        {
            Assert.Equal(new FungalsaurQuest("A").Id, new FungalsaurQuest("B").Id);
        }

        [Fact]
        public async Task GetUniqueQuestAsync_ReturnsAUniqueQuest_WhenAvailable()
        {
            var (game, city) = CreateGame();

            var quest = await new UniqueQuestRetriever().GetUniqueQuestAsync(game, city);

            Assert.True(quest is BitzDenniganQuest or FungalsaurQuest);
        }

        [Fact]
        public async Task GetUniqueQuestAsync_SkipsCompletedQuest()
        {
            var (game, city) = CreateGame();
            var bitz = new BitzDenniganQuest(city.Name);
            game.AddQuest(bitz);
            game.RemoveQuest(bitz);

            Assert.IsType<FungalsaurQuest>(await new UniqueQuestRetriever().GetUniqueQuestAsync(game, city));
        }

        [Fact]
        public async Task GetUniqueQuestAsync_SkipsQuestAlreadyOnTheBoard()
        {
            var (game, city) = CreateGame();
            game.AddQuest(new FungalsaurQuest(city.Name));

            Assert.IsType<BitzDenniganQuest>(await new UniqueQuestRetriever().GetUniqueQuestAsync(game, city));
        }

        [Fact]
        public async Task GetUniqueQuestAsync_ReturnsNull_WhenAllUniqueQuestsAreUsed()
        {
            var (game, city) = CreateGame();
            var bitz = new BitzDenniganQuest(city.Name);
            game.AddQuest(bitz);
            game.RemoveQuest(bitz);
            game.AddQuest(new FungalsaurQuest(city.Name));

            Assert.Null(await new UniqueQuestRetriever().GetUniqueQuestAsync(game, city));
        }
    }
}
