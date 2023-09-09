using OrchidCavalry.Models;
using Rollbard.Library.Rollers.Interfaces;

namespace OrchidCavalry.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly Random random = new();
        private readonly ICharacterRoller characterRoller;

        public CharacterService(ICharacterRoller characterRoller)
        {
            this.characterRoller = characterRoller;
        }

        public Character GenerateCharacter(int minimumAge = 18, int maximumAge = 50)
        {
            var generatedCharacter = this.characterRoller.Get();
            return new Character(generatedCharacter.FirstName, generatedCharacter.LastName, random.Next(minimumAge, maximumAge + 1));
        }
    }
}
