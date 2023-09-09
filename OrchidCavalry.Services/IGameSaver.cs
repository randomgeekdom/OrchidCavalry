using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    public interface IGameSaver
    {
        Game LoadGame();
        Task SaveGameAsync(Game obj);
    }
}