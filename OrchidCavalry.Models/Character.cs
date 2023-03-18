﻿namespace OrchidCavalry.Models
{
    public class Character : Entity
    {
        public int AgeInMonths { get; }

        public Character(string firstName, string lastName, Gender gender, int ageInMonths = 0)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
            this.AgeInMonths = ageInMonths;

            this.Skills = Skill.GetAll().Select(x => new SkillLevel(x, 0));
        }

        public int Age => this.AgeInMonths / 12;

        public string FirstName { get; }

        public Gender Gender { get; }

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

                var orderedSkills = this.Skills.OrderByDescending(x => x.Level).ToArray();
                return $"{orderedSkills[0].Skill.GetTitle(orderedSkills[0].Level)} {orderedSkills[1].Skill.GetTitle(orderedSkills[1].Level)}";
            }
        }
    }
}