using OrchidCavalry.Models;

namespace OrchidCavalry.Domain
{
    /// <summary>
    /// A municipality is a town, city, village
    /// </summary>
    public class City : Entity
    {
        private readonly Random random = new();

        public City(string name, string rulingFaction)
        {
            this.Name = name;
            this.RulingFaction = rulingFaction;
            this.Population = this.random.Next(200, 1000);
        }

        /// <summary>
        /// The Name of the municipality
        /// </summary>
        public string Name { get; set; }

        public int OrchidCavalryReputation { get; set; }

        /// <summary>
        /// The current population
        /// </summary>
        public int Population { get; set; }

        /// <summary>
        /// The Ruling
        /// </summary>
        public string RulingFaction { get; set; }
    }
}