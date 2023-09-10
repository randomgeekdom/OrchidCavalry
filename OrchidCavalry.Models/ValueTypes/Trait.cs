namespace OrchidCavalry.Models.ValueTypes
{
    public record Trait
    {
        public string Name { get; }

        public Trait(string name, Percent level)
        {
            Name = name;
            Level = level;
        }

        public void IncreaseValue(int amount)
        {
            Level += amount;
        }

        public Percent Level { get; set; }
    }
}