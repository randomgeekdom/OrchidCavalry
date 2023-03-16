namespace OrchidCavalry.Models
{
    public static class InterfaceExtensions
    {
        public static void InitializeSkills(this ISkillBased skillBased)
        {
            skillBased.Skills = Skill.GetAll().ToDictionary(x => x, y => 0);
        }
    }
}