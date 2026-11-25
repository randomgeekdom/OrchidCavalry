using Newtonsoft.Json;
using OrchidCavalry.Domain;
using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Domain.Services;

namespace OrchidCavalry.Services.Tests
{
    public class FatherSamsonTests
    {
        [Fact]
        public void NewGame_StartsWithOrderOfFire()
        {
            var faction = Assert.Single(NewGame().Factions);

            Assert.Equal("Order of Fire", faction.Name);
            Assert.Equal(4, faction.OrchidCavalryReputation);
        }

        [Theory]
        [InlineData(25, false)]
        [InlineData(26, true)]
        [InlineData(27, true)]
        public void JoinsOnlyAtRequiredReputation(int reputation, bool joins)
        {
            var game = NewGame(reputation);

            FatherSamson.JoinIfReady(game);

            Assert.Equal(joins, game.Characters.Any(x => x.Id == FatherSamson.CharacterId));
            Assert.Equal(joins, game.Alerts.Any(x => x.Header == "New Character Joined"));
        }

        [Fact]
        public void DoesNotRejoinAfterDeath_EvenAfterReload()
        {
            var game = NewGame(26);
            FatherSamson.JoinIfReady(game);
            game.KillCharacter(game.Characters.Single(x => x.Id == FatherSamson.CharacterId));

            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            var loaded = JsonConvert.DeserializeObject<Game>(JsonConvert.SerializeObject(game, settings), settings)!;
            FatherSamson.JoinIfReady(loaded);

            Assert.Single(loaded.Factions);
            Assert.DoesNotContain(loaded.Characters, x => x.Id == FatherSamson.CharacterId);
        }

        [Fact]
        public void HasExpectedTitleAndSkills()
        {
            var character = FatherSamson.Create();

            Assert.Equal("Father Samson", character.GetName());
            Assert.Equal(["Flame Father"], character.Titles);
            Assert.Equal(
                new Dictionary<Skill, int> { [Skill.Aura] = 2, [Skill.Brawn] = 1, [Skill.Influence] = 1, [Skill.Knowledge] = 1, [Skill.Target] = 1 },
                character.Skills.ToDictionary(x => x.Skill, x => (int)x.Value.Value));
        }

        [Fact]
        public async Task NextTurn_FatherSamsonJoinsOnce()
        {
            var game = NewGame(26);
            for (var i = 0; i < 8; i++)
            {
                game.AddCharacter(new Character($"Member{i}", "Test"));
            }

            var service = new GameplayService(new StubCharacterService(), new DiceRoller(), new NoOpQuestService());
            await service.NextTurnAsync(game);
            await service.NextTurnAsync(game);

            Assert.Single(game.Characters, x => x.Id == FatherSamson.CharacterId);
        }

        private static Game NewGame(int? reputation = null)
        {
            var game = new Game(new Character("Test", "Orchid"));
            if (reputation != null)
            {
                game.Factions.Single().OrchidCavalryReputation = reputation.Value;
            }

            return game;
        }
    }
}
