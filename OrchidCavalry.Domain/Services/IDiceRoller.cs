using OrchidCavalry.Domain.Enumerations;

namespace OrchidCavalry.Domain.Services
{
    public interface IDiceRoller
    {
        DieResult Roll(int modifier = 0, bool isDebilitated = false, bool isEnhanced = false);

        /// <summary>
        /// A 50/50 coin flip
        /// </summary>
        bool FlipCoin();
    }
}