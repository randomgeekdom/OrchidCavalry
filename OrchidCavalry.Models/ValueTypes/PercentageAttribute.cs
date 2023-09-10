namespace OrchidCavalry.Models.ValueTypes
{
    public record PercentageAttribute<T>
    {
        public T Attribute { get; }

        public PercentageAttribute(T attribute, Percent level)
        {
            Attribute = attribute;
            Level = level;
        }

        public void IncreaseValue(int amount)
        {
            Level += amount;
        }

        public Percent Level { get; set; }
    }
}