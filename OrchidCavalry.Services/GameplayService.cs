using OrchidCavalry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Services
{
    public class GameplayService
    {
        public GameplayService(ICharacterService characterService, IAlertService alertService)
        {
            this.characterService = characterService;
            this.alertService = alertService;
        }

        private readonly Random random = new();
        private readonly ICharacterService characterService;
        private readonly IAlertService alertService;

        public async Task NextTurnAsync(Game game)
        {
            if(game.Characters.Count < 8)
            {
                var numberOfConscripts = this.random.Next(1, 4);
                var conscripts = new List<Character>();
                for(int i = 0; i < numberOfConscripts; i++)
                {
                    conscripts.Add(this.characterService.GenerateCharacter());
                }

                this.alertService.DisplayAlert($"Due to the lack of members, you have conscripted {numberOfConscripts} people into the Orchid Cavalry from the civilian population of Orchid Island.  They are the following: {string.Join(", ", conscripts.SelectMany(x => x.GetName()))}", "Civilians Conscripted");
            }
        }
    }
}
