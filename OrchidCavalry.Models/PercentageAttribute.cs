namespace OrchidCavalry.Models
{
    public record PercentageAttribute<T>
    {
        public T Attribute { get; }

        public PercentageAttribute(T attribute, int level)
        {
            this.Attribute = attribute;
            this.Level = level;
        }

        public void IncreaseValue(int amount)
        {
            var newLevel = this.Level + amount;
            this.Level = Math.Max(newLevel, 0);
            this.Level = Math.Min(newLevel, 99);
        }

        public int Level { get; set; }
    }
}