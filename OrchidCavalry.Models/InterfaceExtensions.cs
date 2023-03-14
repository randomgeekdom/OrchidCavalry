namespace OrchidCavalry.Models
{
    public static class InterfaceExtensions
    {
        public static void InitializeSkills(this ISkillBased skillBased)
        {
            skillBased.Skills = new Dictionary<Skill, int>();

            foreach (var skill in Enum.GetValues<Skill>().OfType<Skill>())
            {
                skillBased.Skills.Add(skill, 0);
            }
        }
    }
}