using OrchidCavalry.Domain;

namespace OrchidCavalry.Services
{
    public interface IQuestService
    {
        Task GenerateQuestsAsync(Game game);
    }
}