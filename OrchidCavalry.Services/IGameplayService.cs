using OrchidCavalry.Domain;

namespace OrchidCavalry.Services
{
    public interface IGameplayService
    {
        Task NextTurnAsync(Game game);
    }
}