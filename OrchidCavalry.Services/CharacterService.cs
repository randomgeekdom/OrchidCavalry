using OrchidCavalry.Models;
using Rollbard.Library.Rollers.Interfaces;

namespace OrchidCavalry.Services
{
    public class CharacterService(INameRoller nameRoller) : ICharacterService
    {
        private readonly INameRoller nameRoller = nameRoller;

        public Character GenerateCharacter()
        {
            return new Character(this.nameRoller.GetFirstName(), this.nameRoller.GetLastName());
        }
    }
}