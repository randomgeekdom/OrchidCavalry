namespace OrchidCavalry.Models.ValueTypes
{
    public class Trait : ValueObject<Percent>
    {
        public string Name { get; }

        public Trait(string name, Percent level)
        {
            Name = name;
            Value = level;
        }

        public void IncreaseValue(int amount)
        {
            Value += amount;
        }
    }
}