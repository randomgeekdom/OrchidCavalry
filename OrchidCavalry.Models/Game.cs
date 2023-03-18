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

        public int CivilianPopulation { get; set; } = 50;
        public IEnumerable<CouncilMember> Council { get; }
        public Character PlayerCharacter { get; set; }
        public List<Character> UnassignedCitizens { get; set; } = new List<Character>();

        public IEnumerable<Character> GetAllCitizens()
        {
            var citizens = this.UnassignedCitizens.ToList();

            citizens.AddRange(Council.Select(x => x.Character));

            citizens.Add(this.PlayerCharacter);

            // Add assignment characters

            return citizens;
        }
    }
}