using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Models.ValueTypes;

namespace OrchidCavalry.Models
{
    public class Character : Entity
    {
        private const string CommanderTitle = "Orchid Cavalry Commander";

        public Character(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public int Defeats { get; set; } = 0;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Titles { get; set; } = new List<string>();
        public List<Trait> Traits { get; set; } = new List<Trait>();
        public int Victories { get; set; } = 0;

        public int AddDefeat() => ++this.Defeats;

        public int AddVictory() => ++this.Victories;

        public string GetName() => $"{FirstName} {LastName}";

        public string GetNameAndRank() => $"{this.GetRank()} {this.GetName()}";

        public string GetRank()
        {
            if (IsCommander())
            {
                return "Commander";
            }

            return Enum.GetValues<Rank>().Where(x => Victories > ((int)x) * (3 + (int)x)).OrderByDescending(x => x).FirstOrDefault().ToString();
        }

        public void MakeCommander()
        {
            if (Titles.Contains(CommanderTitle))
                return;

            Titles.Add("Orchid Cavalry Commander");
        }

        private bool IsCommander()
        {
            return this.Titles.Contains(CommanderTitle);
        }
    }
}