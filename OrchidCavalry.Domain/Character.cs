using Newtonsoft.Json;
using OrchidCavalry.Models.ValueTypes;

namespace OrchidCavalry.Models
{
    public class Character : Entity
    {
        public Character() { }
        public Character(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; set; }

        public List<string> Titles { get; set;  } = new List<string>();

        public string LastName { get; set; }

        public List<Trait> Traits { get; set; } = new List<Trait>();

        public string GetName() => $"{FirstName} {LastName}";

        public void MakeRuler()
        {
            if (Titles.Contains("Orchid Cavalry Commander"))
                return;

            Titles.Add("Orchid Cavalry Commander");
        }
    }
}