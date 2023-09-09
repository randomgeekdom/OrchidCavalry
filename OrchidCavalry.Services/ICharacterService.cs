using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    public interface ICharacterService
    {
        Character GenerateCharacter(int minimumAge = 18, int maximumAge = 50);
    }
}