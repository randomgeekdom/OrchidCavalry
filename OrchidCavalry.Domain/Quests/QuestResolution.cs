using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Models;

namespace OrchidCavalry.Domain.Quests
{
    public class QuestResolution(Skill skill, string successText, string victoryTitle, int cityReputationModifier = 1, Action? extraAction = null)
    {
        public Skill Skill { get; } = skill;
        public string SuccessText { get; } = successText;
        public string VictoryTitle { get; } = victoryTitle;
        public int CityReputationModifier { get; } = cityReputationModifier;
        public Action? ExtraAction { get; } = extraAction;
    }
}