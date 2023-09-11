using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    public interface IGameplayService
    {
        void NextTurn(Game game);
    }
}