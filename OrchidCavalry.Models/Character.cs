namespace OrchidCavalry.Models
{
    public class Character : Entity, ISkillBased
    {
        private readonly int ageInDays = 0;

        public Character() : this(null, null, 0)
        { }

        public Character(string firstName, string lastName, Gender gender, int ageInyears = 0)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
            this.ageInDays = ageInyears * 365;

            this.InitializeSkills();
        }

        public int Age => this.ageInDays / 365;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; private set; }
        public Dictionary<Skill, int> Skills { get; set; }
    }
}