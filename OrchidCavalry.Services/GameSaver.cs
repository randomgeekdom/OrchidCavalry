using System.Text.Json;
using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    public class GameSaver : IGameSaver
    {
        public void SaveGame(Game obj)
        {
            var fileName = "orchid.sav";
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            var serializedData = JsonSerializer.Serialize(obj);

            using (var stream = new StreamWriter(filePath))
            {
                stream.WriteAsync(serializedData).Wait();
            }
        }

        public Game LoadGame()
        {
            var fileName = "orchid.sav";
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            if(!File.Exists(filePath))
            {
                return null;
            }

            using (var stream = new StreamReader(filePath))
            {
                var serializedData = stream.ReadToEndAsync().Result;
                return JsonSerializer.Deserialize<Game>(serializedData);
            }
        }
    }
}