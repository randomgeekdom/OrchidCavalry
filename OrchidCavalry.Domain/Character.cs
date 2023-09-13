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
        public List<CharacterTrait> Traits { get; set; } = new List<CharacterTrait>();
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

        public override string ToString()
        {
            return this.GetNameAndRank();
        }

        public TraitValue UpdateCharacterTraitValue(Trait trait, int value)
        {
            var characterTrait = Traits.FirstOrDefault(x => x.Trait == trait);
            if (characterTrait == null)
            {
                characterTrait = new CharacterTrait(trait, 0);
                Traits.Add(characterTrait);
            }

            characterTrait.IncreaseValue(value);

            // If sum of traits is greater than 35 need to lower a trait
            if (Traits.Sum(x => x.Value) > 35)
            {
                Traits.OrderBy(x => Guid.NewGuid()).First().IncreaseValue(-1);
            }

            return characterTrait.Value;
        }

        private bool IsCommander()
        {
            return this.Titles.Contains(CommanderTitle);
        }
    }
}