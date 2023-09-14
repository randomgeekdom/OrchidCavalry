using OrchidCavalry.Domain.Enumerations;

namespace OrchidCavalry.Models.ValueTypes
{
    public class CharacterSkill : Entity
    {
        public CharacterSkill(Skill skill, SkillValue value)
        {
            Skill = skill;
            Value = value;
        }

        public Skill Skill { get; set; }
        public SkillValue Value { get; set; }

        public void IncreaseValue(int amount)
        {
            Value += amount;
        }
    }
}