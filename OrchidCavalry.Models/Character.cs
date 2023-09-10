﻿using Newtonsoft.Json;
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

            this.Skills = Skill.GetAll().Select(x => new PercentageAttribute<Skill>(x, 0)).ToList();
        }

        public string FirstName { get; set; }

        public Dictionary<string, bool> GivenTitles { get; } = new Dictionary<string, bool>();

        public string LastName { get; set; }

        public IEnumerable<PercentageAttribute<Skill>> Skills { get; set; }

        public IEnumerable<string> GetAllTitles()
        {
            var allTitles = this.GivenTitles.Keys.ToList();
            allTitles.Add(GetGeneratedTitle());
            return allTitles;
        }

        public string GetGeneratedTitle()
        {
            var orderedSkills = this.Skills.OrderByDescending(x => x.Level).ToArray();
            return $"{orderedSkills[0].Attribute.GetTitle(orderedSkills[0].Level)} {orderedSkills[1].Attribute.GetTitle(orderedSkills[1].Level)}";
        }

        public string GetName() => $"{FirstName} {LastName}";

        public string GetTitle()
        {
            if (this.GivenTitles.Any())
            {
                return this.GivenTitles.OrderByDescending(x => x.Value).First().Key;
            }

            return GetGeneratedTitle();
        }

        public void MakeRuler()
        {
            GivenTitles.Add("Orchid Cavalry Commander", true);
        }
    }
}