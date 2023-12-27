using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Models;

namespace OrchidCavalry.Domain.Services
{
    public interface IUniqueQuestRetriever
    {
        Task<Quest?> GetUniqueQuestAsync(Game game, City city);
    }
}