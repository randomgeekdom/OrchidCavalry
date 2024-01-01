using OrchidCavalry.Models;

namespace OrchidCavalry.Domain
{
    public class Faction(string name, int orchidCavalryReputation) : Entity
    {
        public string Name { get; set; } = name;
        public int OrchidCavalryReputation { get; set; } = orchidCavalryReputation;

        public override string ToString()
        {
            return Name;
        }
    }
}