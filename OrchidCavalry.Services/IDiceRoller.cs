using OrchidCavalry.Domain.Enumerations;

namespace OrchidCavalry.Services
{
    public interface IDiceRoller
    {
        DieResult Roll(int modifier, bool? epicRoll = null);
    }
}