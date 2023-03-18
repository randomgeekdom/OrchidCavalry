namespace OrchidCavalry.Models
{
    // All the code in this file is included in all platforms.
    public class Game
    {
        public Game(Character playerCharacter)
        {
            PlayerCharacter = playerCharacter;
            this.Council = CouncilPosition.GetAll().Select(x => new CouncilMember(null, x));
        }

        public IEnumerable<CouncilMember> Council { get; }
        public Character PlayerCharacter { get; set; }
    }
}