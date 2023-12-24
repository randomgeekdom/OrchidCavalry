using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    /// <summary>
    /// The quest service generates quests for characters to go on
    /// </summary>
    public class QuestService : IQuestService
    {
        public async Task GenerateQuestAsync(Game game)
        {
            await Task.Run(() =>
            {
                while (game.GetNumberOfInactiveQuestRequiredCharacterSlots() < game.GetNumberOfUndeployedCharacters())
                {
                    // Other quest.  Default to monster hunt


                }
            });
        }

        //private MonsterHuntQuest GenerateMonsterHunt() 
        //{ 
        //}
    }
}