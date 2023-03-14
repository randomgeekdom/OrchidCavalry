namespace OrchidCavalry.Models
{
    public interface ISkillBased
    {
        Dictionary<Skill, int> Skills { get; set; }
    }
}