using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Domain.Services;

namespace OrchidCavalry.Services.Tests
{
    public class DiceRollerTests
    {
        [Fact]
        public void Roll_ReturnsValidDieResult()
        {
            var roller = new DiceRoller();

            for (var i = 0; i < 100; i++)
            {
                var result = roller.Roll(0);
                Assert.True(Enum.IsDefined(typeof(DieResult), result));
            }
        }

        [Fact]
        public void Roll_WithModifiersAndFlags_DoesNotThrow()
        {
            var roller = new DiceRoller();

            for (var i = 0; i < 50; i++)
            {
                var result = roller.Roll(5, isDebilitated: true, isEnhanced: false);
                Assert.True(Enum.IsDefined(typeof(DieResult), result));

                var result2 = roller.Roll(5, isDebilitated: false, isEnhanced: true);
                Assert.True(Enum.IsDefined(typeof(DieResult), result2));
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(-4)]
        public void Roll_WithModifier_StaysInEnumRange(int modifier)
        {
            var roller = new DiceRoller();

            for (var i = 0; i < 25; i++)
            {
                var result = roller.Roll(modifier);
                Assert.True(Enum.IsDefined(typeof(DieResult), result));
            }
        }
    }
}