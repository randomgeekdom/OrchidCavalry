namespace OrchidCavalry.Models.Tests
{
    public class CharacterTests
    {
        [Fact]
        public void CanGetAgeFromAgeByDays()
        {
            Assert.Equal(1, GetCharacter(365).Age);  
        }

        [Fact]
        public void CanInitilaizeCharacter()
        {
            Character character = GetCharacter();
            Assert.NotNull(character);
        }

        [Fact]
        public void CharacterTitleReturnsFirstGivenTitleThatIsPositive()
        {
            var character = GetCharacter();

            var positiveTitle = "Ruler of the Realm";
            character.GivenTitles.Add("Loser of the Rinyard", false);
            character.GivenTitles.Add(positiveTitle, true);

            Assert.Equal(positiveTitle, character.Title);
        }

        [Fact]
        public void CharacterTitleReturnsCompilationTitleIfNoGivenTitle()
        {
            var character = GetCharacter();

            Assert.NotNull(character.Title);
        }

        private static Character GetCharacter(int ageInDays = 0)
        {
            return new Character("firs", "last", Gender.Female, ageInDays);
        }
    }
}