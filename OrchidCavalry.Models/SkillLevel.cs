namespace OrchidCavalry.Models
{
    public record SkillLevel
    {
        public Skill Skill { get; }

        public SkillLevel(Skill skill, int level)
        {
            this.Skill = skill;
            this.Level = level;
        }

        public int Level { get; }
    }
}