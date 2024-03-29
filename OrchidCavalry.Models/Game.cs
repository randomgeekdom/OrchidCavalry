﻿namespace OrchidCavalry.Models
{
    // All the code in this file is included in all platforms.
    public class Game
    {
        public Game()
        { }

        public Game(Character commander)
        {
            Commander = commander;
            this.Characters.Add(commander);

            commander.MakeRuler();
        }

        public List<Alert> Alerts { get; set; } = new List<Alert>();
        public List<Character> Characters { get; set; } = new List<Character>();
        public Character Commander { get; set; }

        public Character ReplaceLeader()
        {
            return this.Characters.OrderByDescending(x => x.Traits.Sum(y => y.Value.Value)).FirstOrDefault();
        }
    }
}