namespace OrchidCavalry.Models
{
    // All the code in this file is included in all platforms.
    public class Game
    {
        public Game() { }

        public Game(Character commander)
        {
            Commander = commander;
            this.Characters.Add(commander);

            commander.MakeCommander();
        }

        public List<Alert> Alerts { get; set; } = new List<Alert>();
        public List<Character> Characters { get; set; } = new List<Character>();
        public Character? Commander { get; set; }

        public void AddAlert(string header, string message)
        {
            Alerts.Add(new Alert(header, message));
        }

        public Character? ReplaceLeader() => this.Characters.OrderByDescending(x => x.Skills.Sum(y => y.Value.Value)).FirstOrDefault();
    }
}