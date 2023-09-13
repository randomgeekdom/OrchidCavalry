namespace OrchidCavalry.Models.ValueTypes
{
    public class Trait : ValueObject<Percent>
    {
        public Trait(string name, Percent level) : base(level)
        {
            Name = name;
        }

        public string Name { get; }

        public void IncreaseValue(int amount)
        {
            Value += amount;
        }
    }
}