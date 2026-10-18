using OrchidCavalry.Domain;
using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Domain.Services;
using OrchidCavalry.Models;
using OrchidCavalry.Services;

namespace OrchidCavalry.Services.Tests
{
    public class ScoreTests
    {
        [Fact]
        public void GetScore_SumsEveryComponent()
        {
            var game = new Game { TurnCount = 4 };

            game.SuccessfulQuestIds.Add(Guid.NewGuid());
            game.SuccessfulQuestIds.Add(Guid.NewGuid());

            var faction = game.AddFaction("TestFaction");
            game.AddToFactionReputation(faction, 3);

            var character = new Character("Hero", "One");
            character.AddVictory();
            character.AddVictory();
            game.AddCharacter(character);

            Assert.Equal(2, game.GetSuccessfulQuestCount());
            Assert.Equal(3, game.GetTotalFactionReputation());
            Assert.Equal(2, game.GetTotalVictories());
            Assert.Equal(11, game.GetScore());
        }

        [Fact]
        public async Task NextTurn_IncrementsTurnCount()
        {
            var game = new Game();
            for (var i = 0; i < 8; i++)
            {
                game.AddCharacter(new Character($"Member{i}", "Test"));
            }

            var service = new GameplayService(new StubCharacterService(), new DiceRoller(), new NoOpQuestService());

            await service.NextTurnAsync(game);
            await service.NextTurnAsync(game);

            Assert.Equal(2, game.TurnCount);
        }

        [Fact]
        public async Task EvaluateQuest_OnSuccess_RecordsWonQuestAndAwardsVictory()
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

            var dice = new FakeDiceRoller(DieResult.PerfectSuccess);

            await quest.EvaluateQuestAsync(game, dice);

            Assert.Contains(quest.Id, game.CompletedQuestIds);
            Assert.Contains(quest.Id, game.SuccessfulQuestIds);
            Assert.Equal(1, character.Victories);
        }

        [Fact]
        public async Task EvaluateQuest_OnFailure_DoesNotRecordWonQuest()
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

            // Catastrophe kills the character; MinorSuccess ends the fail-state rampage.
            var dice = new FakeDiceRoller(DieResult.Catastrophe, DieResult.MinorSuccess);

            await quest.EvaluateQuestAsync(game, dice);

            Assert.Contains(quest.Id, game.CompletedQuestIds);
            Assert.Empty(game.SuccessfulQuestIds);
            Assert.Equal(0, game.GetScore());
        }
    }
}
