using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Models;

namespace OrchidCavalry.Domain.Quests
{
    public class QuestResolution(Skill skill, string successText, string victoryTitle, Action? extraAction = null)
    {
        public Skill Skill { get; } = skill;
        public string SuccessText { get; } = successText;
        public string VictoryTitle { get; } = victoryTitle;
        public Action? ExtraAction { get; } = extraAction;
    }
}