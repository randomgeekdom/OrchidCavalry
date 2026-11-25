using OrchidCavalry.Domain;

namespace OrchidCavalry.Services
{
    public interface IGameSaver
    {
        Task<Game> LoadGameAsync();
        Task SaveGameAsync(Game obj);
    }
}