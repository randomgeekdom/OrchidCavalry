using OrchidCavalry.Domain.Services;
using OrchidCavalry.Models;

namespace OrchidCavalry.Domain.Quests.Unique
{
    public class BitzDenniganQuest : Quest
    {
        public BitzDenniganQuest(string cityName) : base("The Trial of Bitz Dennigan", $"Bitz Dennigan, an ex-priestess, has been charged with murder in the city of {cityName}.", 6, 1, 1, cityName)
        {
            this.BitzDenniganCharacter = new Character("Bitz", "Dennigan")
            {
                Id = new Guid("907e6e6e-9115-4cee-8198-71751c9b0aa2")
            };

            this.BitzDenniganCharacter.AddTitle("The Priestess of Fire");
            this.BitzDenniganCharacter.UpdateCharacterSkillValue(Enumerations.Skill.Aura, 2);
            this.BitzDenniganCharacter.UpdateCharacterSkillValue(Enumerations.Skill.Assist, 1);
            this.BitzDenniganCharacter.UpdateCharacterSkillValue(Enumerations.Skill.Destiny, 2);
            this.BitzDenniganCharacter.UpdateCharacterSkillValue(Enumerations.Skill.Willpower, 4);
        }

        public Character BitzDenniganCharacter { get; set; }

        public override async Task<string?> EvaluateFailStateAsync(Game game, IDiceRoller diceRoller)
        {
            return await Task.Run(() => $"Bitz Dennigan has been found guilty on all charges and executed in the town of {this.CityName}");
        }

        public override List<QuestResolution> GetQuestResolutions(Game game)
        {
            return [
                new QuestResolution(Enumerations.Skill.Deduction, "Thanks to the Orchid Cavalry, the true murderer has been found and Bitz Dennigan has been set free.  She has agreed to join the Orchid Cavalry", "Murder Investigator", 1, ()=>game.AddCharacter(BitzDenniganCharacter)),

                new QuestResolution(Enumerations.Skill.Influence, "Thanks to a resounding defense, Bitz Dennigan has been found innocent of her crimes and has agreed to join the Orchid Cavalry.", "Resounding Orator", 1, ()=>game.AddCharacter(BitzDenniganCharacter)),

                new QuestResolution(Enumerations.Skill.Movement, "With no other option, the Orchid Cavalry has broken Bitz Dennigan out of jail.  She has scattered to the wind.", string.Empty, -1)
                ];
        }
    }
}