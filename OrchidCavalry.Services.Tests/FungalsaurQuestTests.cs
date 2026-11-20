using OrchidCavalry.Domain;
using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Domain.Quests.Unique;
using OrchidCavalry.Models;

namespace OrchidCavalry.Services.Tests
{
    public class FungalsaurQuestTests
    {
        private static (Game game, Faction faction, Character first, Character second, FungalsaurQuest quest) Arrange()
        {
            var game = new Game();
            var faction = game.AddFaction("CityFaction");
            game.AddNewCity("TestCity", faction.Id, "Forest");

            var first = new Character("Hero", "One");
            first.UpdateCharacterSkillValue(Skill.Sense, 3);
            first.UpdateCharacterSkillValue(Skill.Melee, 3);
            game.AddCharacter(first);

            var second = new Character("Hero", "Two");
            second.UpdateCharacterSkillValue(Skill.Aura, 3);
            game.AddCharacter(second);

            var quest = new FungalsaurQuest("TestCity") { Expiration = 0 };
            game.AddQuest(quest);
            quest.CharacterSlots[0].AssignCharacter(first);
            quest.CharacterSlots[1].AssignCharacter(second);

            return (game, faction, first, second, quest);
        }

        [Fact]
        public async Task Investigation_ThenKill_GivesSlayerTitleAndReputation()
        {
            var (game, faction, first, second, quest) = Arrange();
            // Stage 1: first character succeeds.  Stage 2: first character succeeds again with Melee.
            var dice = new FakeDiceRoller(DieResult.Success, DieResult.Success);

            var result = await quest.EvaluateQuestAsync(game, dice);

            Assert.Equal(2, dice.Rolled.Count);
            Assert.Contains("slain", result);
            Assert.Contains("Fungalsaur Slayer", first.Titles);
            Assert.Equal(1, first.Victories);
            Assert.Equal(1, faction.OrchidCavalryReputation);
            Assert.Contains(quest.Id, game.SuccessfulQuestIds);
            Assert.DoesNotContain(game.Characters, x => x.Id == quest.SelenaPancoastCharacter.Id);
        }

        [Fact]
        public async Task Investigation_ThenTame_SelenaPancoastJoins()
        {
            var (game, faction, first, second, quest) = Arrange();
            // Stage 1: first fails, second succeeds.  Stage 2: first fails, second tames with Aura.
            var dice = new FakeDiceRoller(DieResult.Fail, DieResult.Success, DieResult.Fail, DieResult.Success);

            var result = await quest.EvaluateQuestAsync(game, dice);

            Assert.Contains("tamed", result);
            Assert.Contains("Fungalsaur Whisperer", second.Titles);
            Assert.Equal(1, faction.OrchidCavalryReputation);
            Assert.Contains(game.Characters, x => x.Id == quest.SelenaPancoastCharacter.Id);
        }

        [Fact]
        public async Task InvestigationSuccess_GivesNoTitleVictoryOrReputation()
        {
            var (game, faction, first, second, quest) = Arrange();
            // Stage 1 succeeds; both then die in stage 2.
            var dice = new FakeDiceRoller(DieResult.Success, DieResult.Catastrophe, DieResult.Catastrophe);

            var result = await quest.EvaluateQuestAsync(game, dice);

            Assert.Contains("slipped back into legend", result);
            Assert.Empty(first.Titles);
            Assert.Equal(0, first.Victories);
            Assert.Equal(0, faction.OrchidCavalryReputation);
            Assert.DoesNotContain(quest, game.Quests);
            Assert.DoesNotContain(quest.Id, game.SuccessfulQuestIds);
        }

        [Fact]
        public async Task PartyWipedInInvestigation_SkipsConfrontation()
        {
            var (game, faction, first, second, quest) = Arrange();
            var dice = new FakeDiceRoller(DieResult.Catastrophe, DieResult.Catastrophe);

            var result = await quest.EvaluateQuestAsync(game, dice);

            Assert.Equal(2, dice.Rolled.Count);
            Assert.Contains("slipped back into legend", result);
            Assert.Equal(0, faction.OrchidCavalryReputation);
            Assert.Contains(quest.Id, game.CompletedQuestIds);
        }

        [Fact]
        public async Task ExpiredWithNobodyAssigned_IsAbandoned()
        {
            var (game, faction, first, second, quest) = Arrange();
            quest.CharacterSlots[0].UnassignCharacter();
            quest.CharacterSlots[1].UnassignCharacter();

            var result = await quest.EvaluateQuestAsync(game, new FakeDiceRoller());

            Assert.Equal("The search for the ancient mushroom creature near TestCity has been abandoned. The Fungalsaur has slipped back into legend.", result);
            Assert.Contains(quest.Id, game.CompletedQuestIds);
        }
    }
}
