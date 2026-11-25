using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Domain.Services;
using OrchidCavalry.Domain;

namespace OrchidCavalry.Domain.Quests.Unique
{
    public class FungalsaurQuest : Quest
    {
        /// <summary>
        /// Fixed so the quest is recognized as the same unique quest across instances
        /// </summary>
        public static readonly Guid QuestId = new("aca05b6e-419f-4c5e-9c1b-3e21562560e2");

        public FungalsaurQuest(string cityName) : base("Seeking the Mushroom Monster", $"A wildlife expert in the city of {cityName} has been on the search for an ancient mushroom creature thought to be extinct.", 7, 2, 7, cityName)
        {
            this.Id = QuestId;

            this.SelenaPancoastCharacter = new Character("Selena", "Pancoast")
            {
                Id = new Guid("02966b95-7e51-4f4e-9932-04824ae7b02c")
            };

            this.SelenaPancoastCharacter.AddTitle("The Antenna");
            this.SelenaPancoastCharacter.UpdateCharacterSkillValue(Skill.Aura, 3);
            this.SelenaPancoastCharacter.UpdateCharacterSkillValue(Skill.Deduction, 2);
            this.SelenaPancoastCharacter.UpdateCharacterSkillValue(Skill.Movement, 1);
        }

        public Character SelenaPancoastCharacter { get; set; }

        public override async Task<string?> EvaluateFailStateAsync(Game game, IDiceRoller diceRoller)
        {
            return await Task.Run(() => $"The search for the ancient mushroom creature near {this.CityName} has been abandoned. The Fungalsaur has slipped back into legend.");
        }

        /// <summary>
        /// Investigation: the party must find the Fungalsaur before confronting it
        /// </summary>
        protected override bool EvaluatePreliminaryStages(Game game, IDiceRoller diceRoller, List<string> returnText)
        {
            return this.RollUntilSuccess(game, diceRoller, returnText, () => [Skill.Sense, Skill.Aura]) != null;
        }

        public override List<QuestResolution> GetQuestResolutions(Game game)
        {
            return [
                new QuestResolution(Skill.Melee, $"The Fungalsaur has been slain in the wilds near {this.CityName} thanks to the Orchid Cavalry.", "Fungalsaur Slayer"),
                new QuestResolution(Skill.Target, $"The Fungalsaur has been slain in the wilds near {this.CityName} thanks to the Orchid Cavalry.", "Fungalsaur Slayer"),
                new QuestResolution(Skill.Aura, $"The ancient Fungalsaur has been tamed in the wilds near {this.CityName} thanks to the Orchid Cavalry.", "Fungalsaur Whisperer", 1, () => game.AddCharacter(SelenaPancoastCharacter))
                ];
        }
    }
}
