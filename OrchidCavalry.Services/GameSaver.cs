using System.Text.Json;
using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    public class GameSaver : IGameSaver
    {
        public async Task SaveGameAsync(Game obj)
        {
            var fileName = "orchid.sav";
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            var serializedData = JsonSerializer.Serialize(obj);

            using (var stream = new StreamWriter(filePath))
            {
                await stream.WriteAsync(serializedData);
            }
        }

        public async Task<Game> LoadGameAsync()
        {
            var fileName = "orchid.sav";
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            if(!File.Exists(filePath))
            {
                return null;
            }

            using (var stream = new StreamReader(filePath))
            {
                var serializedData = await stream.ReadToEndAsync();
                return JsonSerializer.Deserialize<Game>(serializedData);
            }
        }
    }
}