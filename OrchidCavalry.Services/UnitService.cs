using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrchidCavalry.Models;
using Rollbard.Library.Rollers.Interfaces;

namespace OrchidCavalry.Services
{
    public class UnitService : IUnitService
    {
        private readonly ICharacterService characterService;

        public UnitService(ICharacterService characterService)
        {
            this.characterService = characterService;
        }

        public Unit GetStarterUnit()
        {
            return new Unit
            {
                Name = "Orchid Civilian Guard",
                Leader = this.characterService.GenerateCharacter(),
                Characters = new List<Character>
                {
                    this.characterService.GenerateCharacter(),
                    this.characterService.GenerateCharacter(),
                    this.characterService.GenerateCharacter(),
                    this.characterService.GenerateCharacter(),
                    this.characterService.GenerateCharacter(),
                    this.characterService.GenerateCharacter(),
                    this.characterService.GenerateCharacter(),
                    this.characterService.GenerateCharacter(),
                    this.characterService.GenerateCharacter(),
                }
            };
        }
    }
}
