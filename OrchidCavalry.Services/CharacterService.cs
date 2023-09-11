using OrchidCavalry.Models;
using Rollbard.Library.Rollers.Interfaces;

namespace OrchidCavalry.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly INameRoller nameRoller;

        public CharacterService(INameRoller nameRoller)
        {
            this.nameRoller = nameRoller;
        }

        public Character GenerateCharacter()
        {
            return new Character(this.nameRoller.GetFirstName(), this.nameRoller.GetLastName());
        }
    }
}