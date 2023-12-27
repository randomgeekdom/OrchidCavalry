using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Domain.Quests.Unique;
using OrchidCavalry.Models;

namespace OrchidCavalry.Domain.Services
{
    public class UniqueQuestRetriever : IUniqueQuestRetriever
    {
        public async Task<Quest?> GetUniqueQuestAsync(Game game, City city)
        {
            return await Task.Run(() =>
            {
                var list = new List<Quest>
                {
                    new BitzDenniganQuest(city.Name)
                };

                return list.Where(x => !game.CompletedQuestIds.Contains(x.Id)).OrderBy(x => Guid.NewGuid().ToString()).FirstOrDefault();
            });
        }
    }
}