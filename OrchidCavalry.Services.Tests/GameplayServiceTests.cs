using OrchidCavalry.Domain;
using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Domain.Services;
using OrchidCavalry.Models;
using OrchidCavalry.Services;

namespace OrchidCavalry.Services.Tests
{
    public class GameplayServiceTests
    {
        [Fact]
        public async Task NextTurn_ReplacesDeadCommander()
        {
            var commander = new Character("Old", "Commander");
            commander.MakeCommander();
            commander.UpdateCharacterSkillValue(Skill.Melee, 5);
            var game = new Game(commander);

            // Seed enough characters that recruitment doesn't kick in mid-test.
            for (var i = 0; i < 8; i++)
            {
                var c = new Character($"Member{i}", "Test");
                c.UpdateCharacterSkillValue(Skill.Melee, i);
                game.AddCharacter(c);
            }

            game.KillCharacter(commander);

            var service = new GameplayService(new StubCharacterService(), new DiceRoller(), new NoOpQuestService());

            await service.NextTurnAsync(game);

            Assert.NotNull(game.Commander);
            Assert.NotEqual(commander, game.Commander);
            Assert.Contains(game.Commander!, game.Characters);
            Assert.Contains(game.Alerts, a => a.Header == "A New Commander");
        }

        [Fact]
        public void ReplaceLeader_ReturnsNull_WhenNoCharactersRemain()
        {
            var game = new Game();

            var replacement = game.ReplaceLeader();

            Assert.Null(replacement);
        }
    }
}