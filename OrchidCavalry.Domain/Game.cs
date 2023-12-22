﻿using OrchidCavalry.Domain;
using OrchidCavalry.Domain.Quests;
using System.Reflection.Metadata.Ecma335;

namespace OrchidCavalry.Models
{
    // All the code in this file is included in all platforms.
    public class Game : Entity
    {
        /// <summary>
        /// Public constructor for loading JSON
        /// </summary>
        public Game()
        { }

        /// <summary>
        /// Constructor used for beginning of the game
        /// </summary>
        /// <param name="commander"></param>
        public Game(Character commander)
        {
            Commander = commander;
            this.Characters.Add(commander);

            commander.MakeCommander();
        }

        /// <summary>
        /// The Alerts that get displayed after a turn is taken
        /// </summary>
        public List<Alert> Alerts { get; set; } = new List<Alert>();

        /// <summary>
        /// The members of the Orchid Cavalry
        /// </summary>
        public List<Character> Characters { get; set; } = new List<Character>();

        /// <summary>
        /// The cities in the world
        /// </summary>
        public List<City> Cities { get; set; } = new List<City>();

        /// <summary>
        /// The leader of the Orchid Cavalry
        /// </summary>
        public Character? Commander { get; set; }

        /// <summary>
        /// The quests that the cavalry can undertake or are currently undertaking
        /// </summary>
        public List<Quest> Quests { get; set; } = new List<Quest>();

        /// <summary>
        /// Add an alert to display to the user before they take their next turn
        /// </summary>
        /// <param name="header">The header of the alert</param>
        /// <param name="message">The body of the alert</param>
        public void AddAlert(string header, string message)
        {
            Alerts.Add(new Alert(header, message));
        }

        public City GetCityByName(string name)
        {
            return this.Cities.Single(x => x.Name == name);
        }

        /// <summary>
        /// The available quests are the ones that aren't currently being undertaken
        /// </summary>
        /// <returns></returns>
        public List<Quest> GetCurrentAvailableQuests() => Quests.Where(x => !x.IsActive).ToList();

        public void KillCharacter(Character character)
        {
            this.Characters.Remove(character);

            var activeQuest = this.Quests.FirstOrDefault(x => x.Characters.Contains(character));
            activeQuest?.RemoveCharacter(character);
        }

        public void RemoveCityByName(string cityName)
        {
            this.Cities.Remove(GetCityByName(cityName));
        }

        /// <summary>
        /// Replace the leader.  Takes the highest ranking highest skilled member
        /// </summary>
        /// <returns></returns>
        public Character? ReplaceLeader() => this.Characters.OrderByDescending(x => x.GetRank()).ThenByDescending(x => x.Skills.Sum(y => y.Value.Value)).FirstOrDefault();
    }
}