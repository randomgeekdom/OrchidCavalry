using OrchidCavalry.Domain.Enumerations;

namespace OrchidCavalry.Services
{
    public interface IDiceRoller
    {
        DieResult Roll(int modifier, bool? epicRoll = null);

        DieResult Roll(int modifier, bool isDebilitated = false, bool isEnhanced = false);
    }
}