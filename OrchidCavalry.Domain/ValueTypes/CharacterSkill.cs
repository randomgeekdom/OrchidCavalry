using OrchidCavalry.Domain.Enumerations;

namespace OrchidCavalry.Models.ValueTypes
{
    public class CharacterSkill : Entity
    {
        public CharacterSkill(Skill skill, SkillValue value, bool isEnhanced = false, bool isDebilitated = false)
        {
            this.Skill = skill;
            this.Value = value;
            this.IsEnhanced = isEnhanced;
            this.IsDebilitated = isDebilitated;
        }

        public bool IsDebilitated { get; set; }
        public bool IsEnhanced { get; set; }
        public Skill Skill { get; set; }
        public SkillValue Value { get; set; }

        public void IncreaseValue(int amount)
        {
            Value += amount;
        }
    }
}