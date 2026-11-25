using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Domain;

namespace OrchidCavalry.Services
{
    public interface IQuestPopupService
    {
        Task ShowPopupAsync(Game game, INavigation navigation);
    }
}