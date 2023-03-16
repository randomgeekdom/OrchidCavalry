namespace OrchidCavalry.Models
{
    // All the code in this file is included in all platforms.
    public class Game
    {
        public Game()
        {
            this.Council = CouncilPosition.GetAll().ToDictionary(x => x, x => default(Character));
        }

        public Dictionary<CouncilPosition, Character> Council { get; }
        public Character PlayerCharacter { get; set; }
    }
}