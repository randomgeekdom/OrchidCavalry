namespace OrchidCavalry.Models.Tests
{
    public class CharacterTests
    {
        [Fact]
        public void CanGetAgeFromAgeByDays()
        {
            Assert.Equal(1, GetCharacter(1).Age);  
        }

        [Fact]
        public void CanInitilaizeCharacter()
        {
            Character character = GetCharacter();
            Assert.NotNull(character);
        }

        private static Character GetCharacter(int age = 0)
        {
            return new Character("firs", "last", Gender.Female, age);
        }
    }
}