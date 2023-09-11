using OrchidCavalry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Services
{
    public class GameplayService : IGameplayService
    {
        public GameplayService(ICharacterService characterService)
        {
            this.characterService = characterService;
        }

        private readonly Random random = new();
        private readonly ICharacterService characterService;

        public void NextTurn(Game game)
        {
            if (game.Characters.Count < 8)
            {
                var numberOfConscripts = this.random.Next(2, 4);
                var conscripts = new List<Character>();
                for (int i = 0; i < numberOfConscripts; i++)
                {
                    var character = this.characterService.GenerateCharacter();
                    conscripts.Add(character);
                    game.Characters.Add(character);
                }

                var alertText = $"Due to the lack of members, you have conscripted {numberOfConscripts} people into the Orchid Cavalry from the civilian population of Orchid Island: {string.Join(", ", conscripts.Select(x => x.GetName()))}";
                game.Alerts.Add(new Alert("Civilians Conscripted", alertText));
            }
        }
    }
}
