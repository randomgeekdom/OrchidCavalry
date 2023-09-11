namespace OrchidCavalry.Models.Tests
{
    public class CharacterTests
    {
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
            character.Titles.Add("Loser of the Rinyard");
            character.Titles.Add(positiveTitle);

            Assert.Equal(positiveTitle, character.Titles.First());
        }

        [Fact]
        public void CharacterTitleReturnsCompilationTitleIfNoGivenTitle()
        {
            var character = GetCharacter();

            Assert.NotNull(character.Titles.First());
        }

        private static Character GetCharacter()
        {
            return new Character("firs", "last");
        }
    }
}