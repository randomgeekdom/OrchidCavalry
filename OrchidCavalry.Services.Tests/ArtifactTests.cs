using OrchidCavalry.Domain;
using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Domain.Services;
using OrchidCavalry.Models;

namespace OrchidCavalry.Services.Tests
{
    public class ArtifactTests
    {
        private class ArtifactQuest(string cityName) : Quest("Artifact Quest", "A test quest", 0, 1, 2, cityName)
        {
            public override Task<string?> EvaluateFailStateAsync(Game game, IDiceRoller diceRoller) => Task.FromResult<string?>(null);

            public override List<QuestResolution> GetQuestResolutions(Game game) =>
                [new QuestResolution(Skill.Melee, "The test quest was won.", "Tester")];

            protected override string GetArtifactAwardText(Character character, Artifact artifact) =>
                $"{character.GetName()} took the {artifact.Name} from the test.";
        }

        private static (Game game, Character character, ArtifactQuest quest) Arrange()
        {
            var game = new Game();
            var faction = game.AddFaction("CityFaction");
            game.AddNewCity("TestCity", faction.Id, "Plain");

            var character = new Character("Hero", "One");
            character.UpdateCharacterSkillValue(Skill.Melee, 4);
            game.AddCharacter(character);

            var quest = new ArtifactQuest("TestCity");
            game.AddQuest(quest);
            quest.CharacterSlots[0].AssignCharacter(character);

            return (game, character, quest);
        }

        [Fact]
        public async Task FatalRoll_WithoutArtifacts_KillsCharacter()
        {
            var (game, character, quest) = Arrange();
            var dice = new FakeDiceRoller(DieResult.Catastrophe);

            await quest.EvaluateQuestAsync(game, dice);

            Assert.DoesNotContain(character, game.Characters);
        }

        [Fact]
        public async Task FatalRoll_WithArtifact_HeadsLosesArtifactAndWithdraws()
        {
            var (game, character, quest) = Arrange();
            var artifact = new Artifact("Orb");
            character.AddArtifact(artifact);
            var dice = new FakeDiceRoller(DieResult.Catastrophe);
            dice.CoinFlips.Enqueue(true);

            var result = await quest.EvaluateQuestAsync(game, dice);

            Assert.Contains(character, game.Characters);
            Assert.Empty(character.Artifacts);
            Assert.Contains(artifact, game.LostArtifacts);
            Assert.Single(dice.Rolled);
            Assert.Contains("lost the Orb but survived, and has withdrawn from the quest.", result);
        }

        [Fact]
        public async Task FatalRoll_WithArtifact_TailsLosesArtifactAndFightsOn()
        {
            var (game, character, quest) = Arrange();
            character.AddArtifact(new Artifact("Orb"));
            var dice = new FakeDiceRoller(DieResult.Catastrophe, DieResult.Success);
            dice.CoinFlips.Enqueue(false);

            var result = await quest.EvaluateQuestAsync(game, dice);

            Assert.Contains(character, game.Characters);
            Assert.Equal(2, dice.Rolled.Count);
            Assert.Contains("lost the Orb but survived, and fights on.", result);
            Assert.Contains("The test quest was won.", result);
        }

        [Fact]
        public async Task FightingOn_AfterLosingLastArtifact_CanStillDie()
        {
            var (game, character, quest) = Arrange();
            character.AddArtifact(new Artifact("Orb"));
            var dice = new FakeDiceRoller(DieResult.Catastrophe, DieResult.Catastrophe);
            dice.CoinFlips.Enqueue(false);

            await quest.EvaluateQuestAsync(game, dice);

            Assert.DoesNotContain(character, game.Characters);
        }

        [Fact]
        public void LoseRandomArtifact_RemovesExactlyOne()
        {
            var game = new Game();
            var character = new Character("Hero", "One");
            character.AddArtifact(new Artifact("Orb"));
            character.AddArtifact(new Artifact("Crown"));

            var lost = game.LoseRandomArtifact(character);

            Assert.NotNull(lost);
            Assert.Single(character.Artifacts);
            Assert.DoesNotContain(lost, character.Artifacts);
            Assert.Equal([lost], game.LostArtifacts);
        }

        [Fact]
        public async Task QuestVictory_AwardsArtifactsToWinnerWithQuestText()
        {
            var (game, character, quest) = Arrange();
            var artifact = new Artifact("Crown");
            game.LostArtifacts.Add(artifact);
            quest.Artifacts.Add(artifact);
            var dice = new FakeDiceRoller(DieResult.Success);

            var result = await quest.EvaluateQuestAsync(game, dice);

            Assert.Contains(artifact, character.Artifacts);
            Assert.DoesNotContain(artifact, game.LostArtifacts);
            Assert.Contains("Hero One took the Crown from the test.", result);
        }
    }
}
