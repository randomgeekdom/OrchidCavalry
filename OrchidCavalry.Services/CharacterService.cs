using OrchidCavalry.Models;
using Rollbard.Library.Rollers.Interfaces;

namespace OrchidCavalry.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRoller characterRoller;

        public CharacterService(ICharacterRoller characterRoller)
        {
            this.characterRoller = characterRoller;
        }

        public Character GenerateCharacter()
        {
            var generatedCharacter = this.characterRoller.Get();
            return new Character(generatedCharacter.FirstName, generatedCharacter.LastName);
        }
    }
}
