using OrchidCavalry.Domain.Enumerations;

namespace OrchidCavalry.Domain.Interfaces
{
    public interface IDiceRoller
    {
        DieResult Roll(int modifier = 0, bool isDebilitated = false, bool isEnhanced = false);
    }
}