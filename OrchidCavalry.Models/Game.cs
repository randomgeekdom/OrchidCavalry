namespace OrchidCavalry.Models
{
    // All the code in this file is included in all platforms.
    public class Game
    {
        public Game(Character playerCharacter)
        {
            PlayerCharacter = playerCharacter;
            playerCharacter.MakeRuler();
        }

        public Character PlayerCharacter { get; set; }
        public List<Character> UnassignedCitizens { get; set; } = new List<Character>();

        public IEnumerable<Character> GetAllCitizens()
        {
            var citizens = this.UnassignedCitizens.ToList();

            citizens.Add(this.PlayerCharacter);

            // Add assignment characters

            return citizens;
        }
    }
}