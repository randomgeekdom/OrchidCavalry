namespace OrchidCavalry.Models
{
    // All the code in this file is included in all platforms.
    public class Game
    {
        public Game()
        { }

        public Game(Character playerCharacter)
        {
            PlayerCharacter = playerCharacter;
            playerCharacter.MakeRuler();
        }

        public Character PlayerCharacter { get; set; }
        public IEnumerable<Unit> Units { get; set; }
    }
}