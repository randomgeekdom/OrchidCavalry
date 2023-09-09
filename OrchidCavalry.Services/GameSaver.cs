﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

        public Game LoadGame()
        {
            var fileName = "orchid.sav";
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            if (!File.Exists(filePath))
            {
                return null;
            }

            using (var stream = new StreamReader(filePath))
            {
                var serializedData = stream.ReadToEndAsync().Result;
                return JsonConvert.DeserializeObject<Game>(serializedData, this.serializerSettings);
            }
        }

        public void SaveGame(Game obj)
        {
            var fileName = "orchid.sav";
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            var serializedData = JsonConvert.SerializeObject(obj, this.serializerSettings);

            using (var stream = new StreamWriter(filePath))
            {
                stream.WriteAsync(serializedData).Wait();
            }
        }
    }
}