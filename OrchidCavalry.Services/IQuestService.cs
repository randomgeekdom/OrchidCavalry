using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    public interface IQuestService
    {
        Task GenerateQuestsAsync(Game game);
    }
}