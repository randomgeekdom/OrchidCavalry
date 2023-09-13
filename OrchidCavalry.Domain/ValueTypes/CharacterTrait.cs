using OrchidCavalry.Domain.Enumerations;

namespace OrchidCavalry.Models.ValueTypes
{
    public class CharacterTrait : Entity
    {
        public CharacterTrait(Trait trait, TraitValue value)
        {
            Trait = trait;
            Value = value;
        }

        public Trait Trait { get; set; }
        public TraitValue Value { get; set; }

        public void IncreaseValue(int amount)
        {
            Value += amount;
        }
    }
}