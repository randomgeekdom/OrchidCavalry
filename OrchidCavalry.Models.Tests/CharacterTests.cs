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
            character.GivenTitles.Add("Loser of the Rinyard", false);
            character.GivenTitles.Add(positiveTitle, true);

            Assert.Equal(positiveTitle, character.GetTitle());
        }

        [Fact]
        public void CharacterTitleReturnsCompilationTitleIfNoGivenTitle()
        {
            var character = GetCharacter();

            Assert.NotNull(character.GetTitle());
        }

        private static Character GetCharacter()
        {
            return new Character("firs", "last");
        }
    }
}