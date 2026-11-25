using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Domain;

namespace OrchidCavalry.Domain.Services
{
    public interface IUniqueQuestRetriever
    {
        Task<Quest?> GetUniqueQuestAsync(Game game, City city);
    }
}