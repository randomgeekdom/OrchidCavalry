using OrchidCavalry.Domain;

namespace OrchidCavalry.Domain
{
    /// <summary>
    /// Something a character can collect.  When a character carrying artifacts would die, they lose one instead.
    /// </summary>
    public class Artifact(string name) : Entity
    {
        public string Name { get; set; } = name;

        public override string ToString() => this.Name;
    }
}
