using OrchidCavalry.Models;

namespace OrchidCavalry.Domain
{
    public class Loot(string name) : Entity
    {
        public string Name { get; set; } = name;
    }
}