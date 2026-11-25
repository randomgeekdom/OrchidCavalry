using OrchidCavalry.Domain;
using OrchidCavalry.Views;

namespace OrchidCavalry.Services
{
    public interface IGameViewPopupService
    {
        Task ShowPopupAsync<T>(Game game, INavigation navigation) where T : Page, IGameView;
    }
}