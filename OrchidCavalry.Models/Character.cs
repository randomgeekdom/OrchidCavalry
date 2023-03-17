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

            this.Skills = Skill.GetAll().ToDictionary(x => x, x => 0);
        }

        public int Age => this.ageInDays / 365;
        public string Title
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.GivenTitle))
                {
                    //var skills = this.Skills.OrderByDescending(x=>x.Value) ?? 
                }

                return GivenTitle;
            }
        }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GivenTitle { get; set; }
        public Gender Gender { get; private set; }
        public Dictionary<Skill, int> Skills { get; }
        public string Name => $"{FirstName} {LastName}";
    }
}