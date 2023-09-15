using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    public interface IGameplayService
    {
        Task NextTurnAsync(Game game, INavigation navigation);
    }
}