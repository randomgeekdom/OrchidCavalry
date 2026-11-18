using OrchidCavalry.Domain;
using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Models;

namespace OrchidCavalry.Services.Tests
{
    public class QuestResolutionTests
    {
        private static (Game game, Faction faction, City city, Character character, MonsterQuest quest) Arrange()
        {
            var game = new Game();
            var faction = game.AddFaction("CityFaction");
            var city = game.AddNewCity("TestCity", faction.Id, "Plain");
            city.Population = 500;

            var character = new Character("Hero", "One");
            character.UpdateCharacterSkillValue(Skill.Melee, 4);
            game.AddCharacter(character);

            var quest = MonsterQuest.Create("Goblin", "TestCity");
            quest.Expiration = 0;
            game.AddQuest(quest);
            quest.CharacterSlots[0].AssignCharacter(character);

            return (game, faction, city, character, quest);
        }

        [Fact]
        public async Task EvaluateQuest_WhenDiceSucceeds_ResolvesAndRemovesQuest()
        {
            var (game, faction, city, character, quest) = Arrange();
            var dice = new FakeDiceRoller(DieResult.PerfectSuccess);

            var result = await quest.EvaluateQuestAsync(game, dice);

            Assert.NotNull(result);
            Assert.Contains("slain", result);
            Assert.DoesNotContain(quest, game.Quests);
            Assert.Equal(1, faction.OrchidCavalryReputation);
            Assert.Contains("Goblin Slayer", character.Titles);
        }

        [Fact]
        public async Task EvaluateQuest_WhenCatastrophe_Strikes_KillsCharacter()
        {
            var (game, faction, city, character, quest) = Arrange();
            // Catastrophe for the character; MinorSuccess stops the fail-state rampages.
            var dice = new FakeDiceRoller(DieResult.Catastrophe, DieResult.MinorSuccess);

            await quest.EvaluateQuestAsync(game, dice);

            Assert.DoesNotContain(character, game.Characters);
            Assert.DoesNotContain(quest, game.Quests);
        }

        [Fact]
        public async Task EvaluateQuest_AfterSuccess_RemainingCharactersDoNotRoll()
        {
            var (game, faction, city, character, quest) = Arrange();
            var second = new Character("Hero", "Two");
            second.UpdateCharacterSkillValue(Skill.Melee, 4);
            game.AddCharacter(second);
            quest.CharacterSlots[1].AssignCharacter(second);

            // First roller succeeds; a second roll would be a Catastrophe.
            var dice = new FakeDiceRoller(DieResult.Success, DieResult.Catastrophe);

            await quest.EvaluateQuestAsync(game, dice);

            Assert.Single(dice.Rolled);
            Assert.Contains(character, game.Characters);
            Assert.Contains(second, game.Characters);
            Assert.DoesNotContain(quest, game.Quests);
        }

        [Fact]
        public async Task EvaluateQuest_WithExpiredTimer_RunsFailState()
        {
            var (game, faction, city, character, quest) = Arrange();
            // Expire the quest timer.
            quest.Expiration = 0;
            // First call ticks expiration path? Actually Expiration > 0 check uses Expiration--; here 0 so enters else.
            // Force no filled slots to skip resolution loop.
            quest.CharacterSlots[0].UnassignCharacter();

            var dice = new FakeDiceRoller(DieResult.MinorSuccess);
            var result = await quest.EvaluateQuestAsync(game, dice);

            Assert.NotNull(result);
            Assert.DoesNotContain(quest, game.Quests);
        }

        [Fact]
        public async Task MarauderFailState_OnCatastrophe_WithUnknownBand_AddsBandFactionAsRuler()
        {
            var game = new Game();
            var cityFaction = game.AddFaction("CityFaction");
            var city = game.AddNewCity("TestCity", cityFaction.Id, "Plain");
            city.Population = 500;

            var quest = MarauderQuest.Create("Red Riders", "Bold Leader", "TestCity");
            var dice = new FakeDiceRoller(DieResult.Catastrophe);

            var result = await quest.EvaluateFailStateAsync(game, dice);

            Assert.Contains("taken control", result);
            var bandFaction = Assert.Single(game.Factions, x => x.Name == "Red Riders");
            Assert.Equal(bandFaction.Id, city.RulingFactionId);
        }
    }
}