using OrchidCavalry.Domain.Enumerations;

namespace OrchidCavalry.Domain.Quests
{
    public class QuestResolution(Skill skill, string successText, string victoryTitle)
    {
        public Skill Skill { get; set; } = skill;
        public string SuccessText { get; set; } = successText;
        public string VictoryTitle { get; set; } = victoryTitle;
    }
}