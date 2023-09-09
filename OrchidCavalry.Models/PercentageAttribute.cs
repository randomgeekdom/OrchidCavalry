using OrchidCavalry.Models.ValueTypes;

namespace OrchidCavalry.Models
{
    public record PercentageAttribute<T>
    {
        public T Attribute { get; }

        public PercentageAttribute(T attribute, Percent level)
        {
            this.Attribute = attribute;
            this.Level = level;
        }

        public void IncreaseValue(int amount)
        {
            this.Level += amount;
        }

        public Percent Level { get; set; }
    }
}