using OrchidCavalry.Models;

namespace OrchidCavalry.Domain
{
    /// <summary>
    /// A municipality is a town, city, village
    /// </summary>
    public class City : Entity
    {
        public City(string name, string rulingFaction, int population = 500)
        {
            this.Name = name;
            this.Population = population;
            this.RulingFaction = rulingFaction;
        }

        /// <summary>
        /// The Name of the municipality
        /// </summary>
        public string Name { get; set; }

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