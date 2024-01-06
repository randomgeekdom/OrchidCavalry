using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    public interface IQuestPopupService
    {
        Task ShowPopupAsync(Game game, INavigation navigation);
    }
}