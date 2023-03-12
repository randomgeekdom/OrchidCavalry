using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    public interface IGameSaver
    {
        Task<Game> DeserializeObjectFromFile<T>(string fileName);
        Task SerializeObjectToFile<T>(Game obj, string fileName);
    }
}