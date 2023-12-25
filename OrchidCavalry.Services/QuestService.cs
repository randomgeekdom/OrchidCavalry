using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Models;
using Rollbard.Library.Rollers.Interfaces;

namespace OrchidCavalry.Services
{
    /// <summary>
    /// The quest service generates quests for characters to go on
    /// </summary>
    public class QuestService(IMonsterRoller monsterRoller, ICityService cityService) : IQuestService
    {
        private readonly ICityService cityService = cityService;
        private readonly IMonsterRoller monsterRoller = monsterRoller;

        public async Task GenerateQuestsAsync(Game game)
        {
            while (game.GetNumberOfInactiveQuestRequiredCharacterSlots() < game.GetNumberOfUndeployedCharacters())
            {
                // Other quest.  Default to monster hunt
                game.AddQuest(await GenerateMonsterHuntAsync(game));
            }
        }

        private async Task<MonsterQuest> GenerateMonsterHuntAsync(Game game)
        {
            var city = await this.cityService.GetUnthreatenedRandomCityAsync(game);
            return MonsterQuest.Create(this.monsterRoller.Get(), city.Name);
        }
    }
}