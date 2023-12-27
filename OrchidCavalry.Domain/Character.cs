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

        public bool IsDeployed { get; set; } = false;

        public string LastName { get; set; }

        public List<CharacterSkill> Skills { get; set; } = new List<CharacterSkill>();

        public List<string> Titles { get; set; } = new List<string>();

        public int Victories { get; set; } = 0;

        public int AddDefeat() => ++this.Defeats;

        public int AddVictory() => ++this.Victories;

        public void DebilitateRandomCharacterSkill()
        {
            var randomSkill = Skills.OrderBy(x => Guid.NewGuid()).First();
            if (randomSkill.IsEnhanced)
            {
                randomSkill.IsEnhanced = false;
            }
            else
            {
                randomSkill.IsDebilitated = true;
            }
        }

        public void EnhanceRandomCharacterSkill()
        {
            var randomSkill = Skills.OrderBy(x => Guid.NewGuid()).First();
            if (randomSkill.IsDebilitated)
            {
                randomSkill.IsDebilitated = false;
            }
            else
            {
                randomSkill.IsEnhanced = true;
            }
        }

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

        public bool IsCommander()
        {
            return this.Titles.Contains(CommanderTitle);
        }

        public void MakeCommander()
        {
            if (IsCommander())
                return;

            Titles.Add(CommanderTitle);
        }

        public override string ToString()
        {
            return this.GetNameAndRank();
        }

        public CharacterSkill UpdateCharacterSkillValue(Skill Skill, int value)
        {
            var characterSkill = Skills.FirstOrDefault(x => x.Skill == Skill);
            if (characterSkill == null)
            {
                characterSkill = new CharacterSkill(Skill, 0);
                Skills.Add(characterSkill);
            }

            characterSkill.IncreaseValue(value);

            // If sum of Skills is greater than 35 need to lower a Skill
            while (Skills.Sum(x => x.Value) > 35)
            {
                Skills.OrderBy(x => Guid.NewGuid()).First().IncreaseValue(-1);
            }

            return characterSkill;
        }

        public void AddTitle(string title)
        {
            if (!string.IsNullOrWhiteSpace(title) && !Titles.Contains(title))
            {
                Titles.Add(title);
            }
        }
    }
}