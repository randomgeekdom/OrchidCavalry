using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    public interface IQuestService
    {
        Task GenerateQuestAsync(Game game);
    }
}