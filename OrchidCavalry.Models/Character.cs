namespace OrchidCavalry.Models
{
    public class Character : Entity
    {
        private readonly int ageInDays = 0;

        public Character(string firstName, string lastName, Gender gender, int ageInyears = 0)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
            this.ageInDays = ageInyears * 365;

            this.Skills = Skill.GetAll().ToDictionary(x => x, y => 0);
        }

        public int Age => this.ageInDays / 365;
        public string Title
        {
            get
            {
                if (this.GivenTitles.Any())
                {
                    return this.GivenTitles.OrderByDescending(x => x.Value).First().Key;
                }

                var orderedSkills = this.Skills.OrderByDescending(x => x.Value).ToArray();
                return $"{orderedSkills[0].Key.GetTitle(orderedSkills[0].Value)} {orderedSkills[1].Key.GetTitle(orderedSkills[1].Value)}";
            }
        }


        public string FirstName { get; }
        public string LastName { get; }
        public Dictionary<string, bool> GivenTitles { get; } = new Dictionary<string, bool>();
        public Gender Gender { get; }
        public Dictionary<Skill, int> Skills { get; }
        public string Name => $"{FirstName} {LastName}";
    }
}