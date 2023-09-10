
namespace OrchidCavalry.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public override bool Equals(object obj)
        {
            return Id.Equals((obj as Entity)?.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}