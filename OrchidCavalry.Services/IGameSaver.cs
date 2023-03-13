using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    public interface IGameSaver
    {
        Game LoadGame();
        void SaveGame(Game obj);
    }
}