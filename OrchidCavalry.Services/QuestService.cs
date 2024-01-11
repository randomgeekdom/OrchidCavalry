using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Domain.Services;
using OrchidCavalry.Models;
using Rollbard.Library.Rollers.Interfaces;

namespace OrchidCavalry.Services
{
    /// <summary>
    /// The quest service generates quests for characters to go on
    /// </summary>
    public class QuestService(IMonsterRoller monsterRoller, ICityService cityService, IDiceRoller diceRoller, INameRoller nameRoller, IFactionRoller factionRoller, IUniqueQuestRetriever uniqueQuestRetriever) : IQuestService
    {
        private readonly ICityService cityService = cityService;
        private readonly IDiceRoller diceRoller = diceRoller;
        private readonly IFactionRoller factionRoller = factionRoller;
        private readonly IMonsterRoller monsterRoller = monsterRoller;
        private readonly INameRoller nameRoller = nameRoller;
        private readonly IUniqueQuestRetriever uniqueQuestRetriever = uniqueQuestRetriever;

        public async Task GenerateQuestsAsync(Game game)
        {
            while (game.GetNumberOfAvailableQuestSlots() < game.GetNumberOfUndeployedCharacters())
            {
                var city = await this.cityService.GetUnthreatenedRandomCityAsync(game);

                if (diceRoller.Roll() >= Domain.Enumerations.DieResult.Success)
                {
                    var uniqueQuest = await this.uniqueQuestRetriever.GetUniqueQuestAsync(game, city);
                    if (uniqueQuest != null)
                    {
                        game.AddQuest(uniqueQuest);
                    }
                }

                if (diceRoller.Roll() >= Domain.Enumerations.DieResult.Success)
                {
                    game.AddQuest(MonsterQuest.Create(this.monsterRoller.Get(), city.Name));
                }

                if (diceRoller.Roll() >= Domain.Enumerations.DieResult.Success)
                {
                    game.AddQuest(MarauderQuest.Create(this.factionRoller.Get(),
                        $"{this.nameRoller.GetFirstName()} {this.nameRoller.GetLastName()}", city.Name));
                }
            }
        }
    }
}