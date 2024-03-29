﻿using Newtonsoft.Json;
using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    public class GameSaver : IGameSaver
    {
        private JsonSerializerSettings serializerSettings;

        public GameSaver()
        {
            this.serializerSettings = new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
        }

        public async Task<Game> LoadGameAsync()
        {
            const string fileName = "orchid.sav";
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            if (!File.Exists(filePath))
            {
                return null;
            }

            using (var stream = new StreamReader(filePath))
            {
                var serializedData = await stream.ReadToEndAsync();
                return JsonConvert.DeserializeObject<Game>(serializedData, this.serializerSettings);
            }
        }

        public async Task SaveGameAsync(Game obj)
        {
            var fileName = "orchid.sav";
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            if (File.Exists(filePath))
            {
                File.Move(filePath, Path.Combine(FileSystem.AppDataDirectory, filePath + ".backup"), true);
            }

            var serializedData = JsonConvert.SerializeObject(obj, this.serializerSettings);

            using (var stream = new StreamWriter(filePath))
            {
                await stream.WriteAsync(serializedData);
            }
        }
    }
}