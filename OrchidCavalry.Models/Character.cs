namespace OrchidCavalry.Models
{
    public class Character : Entity
    {
        public Character(string firstName, string lastName, int ageInDays = 0)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.AgeInDays = ageInDays;

            this.Skills = Skill.GetAll().Select(x => new SkillLevel(x, 0));
        }

        public int Age => this.AgeInDays / 365;
        public int AgeInDays { get; }

        public IEnumerable<string> AllTitles
        {
            get
            {
                var allTitles = this.GivenTitles.Keys.ToList();
                allTitles.Add(GeneratedTitle);
                return allTitles;
            }
        }

        public string FirstName { get; }

        public string GeneratedTitle
        {
            get
            {
                var orderedSkills = this.Skills.OrderByDescending(x => x.Level).ToArray();
                return $"{orderedSkills[0].Skill.GetTitle(orderedSkills[0].Level)} {orderedSkills[1].Skill.GetTitle(orderedSkills[1].Level)}";
            }
        }

        public Dictionary<string, bool> GivenTitles { get; } = new Dictionary<string, bool>();
        public string LastName { get; }

        public string Name => $"{FirstName} {LastName}";

        public IEnumerable<SkillLevel> Skills { get; }

        public string Title
        {
            get
            {
                if (this.GivenTitles.Any())
                {
                    return this.GivenTitles.OrderByDescending(x => x.Value).First().Key;
                }

                return GeneratedTitle;
            }
        }

        public void MakeRuler()
        {
            var currentTitles = GivenTitles.ToList();
            GivenTitles.Clear();
            GivenTitles.Add("Ruler of Orchid Island", true);
            foreach(var title in currentTitles)
            {
                GivenTitles.Add(title.Key, title.Value);
            }
        }
    }
}