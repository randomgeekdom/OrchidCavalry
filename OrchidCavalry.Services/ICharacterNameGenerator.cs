using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    public interface ICharacterNameGenerator
    {
        string GenerateFirstName(Gender gender);
        string GenerateLastName();
    }
}