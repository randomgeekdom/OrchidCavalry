using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Domain.Services;
using OrchidCavalry.Models;

namespace OrchidCavalry.Domain.Quests
{
    /// <summary>
    /// A quest of 1 to 4 characters to stop a monster from terrorizing a city
    /// </summary>
    public class MonsterQuest(string title, string description, string monsterName, string cityName) : Quest(title, description, 5, 1, cityName)
    {
        public string MonsterName { get; set; } = monsterName;

        /// <summary>
        /// Create a monster quest
        /// </summary>
        /// <param name="monsterName">The name of the monster</param>
        /// <param name="cityName">The city where it's happening</param>
        /// <returns></returns>
        public static MonsterQuest Create(string monsterName, string cityName)
        {
            return new MonsterQuest($"A Monstrous Threat", $"A {monsterName} has been seen prowling near the city of {cityName}", monsterName, cityName);
        }

        public override async Task<string?> EvaluateFailState(Game game, IDiceRoller diceRoller)
        {
            return await Task.Run(() =>
            {
                var city = game.GetCityByName(this.CityName);
                var currentPopulation = city.Population;
                var newPopulation = currentPopulation;

                var monsterRampageComplete = false;
                var returnText = new List<string>();

                while (newPopulation > 0 && !monsterRampageComplete)
                {
                    var result = diceRoller.Roll(0, isDebilitated: true);

                    if (result < DieResult.Success)
                    {
                        newPopulation--;
                    }

                    if (result == DieResult.Catastrophe)
                    {
                        newPopulation /= 2;
                    }

                    if (result >= DieResult.MinorSuccess)
                    {
                        returnText.Add($"The {MonsterName} has been been killed by the local population of {CityName}.");
                        monsterRampageComplete = true;
                    }
                }

                if (newPopulation < 10)
                {
                    game.RemoveCityByName(this.CityName);
                    returnText.Add($"The {MonsterName}'s rampage killed most of the people of {CityName}.  Whoever was left escaped and the city is no more.");
                }
                else
                {
                    var populationKilled = currentPopulation = newPopulation;
                    if (populationKilled > 0)
                    {
                        returnText.Add($"The {MonsterName} did manage to kill {populationKilled} of the population.");
                    }
                }

                return string.Join(" ", returnText);
            });
        }

        public override List<QuestResolution> GetQuestResolutions()
        {
            return
            [
                new QuestResolution(Skill.Aura, $"The {MonsterName} was routed from the city of {CityName} thanks to the Orchid Cavalry.", $"{MonsterName} Whisperer"),
                new QuestResolution(Skill.Melee, $"The {MonsterName} has been slain in the city of {CityName} thanks to the Orchid Cavalry.", $"{MonsterName} Slayer"),
                new QuestResolution(Skill.Target, $"The {MonsterName} has been slain in the city of {CityName} thanks to the Orchid Cavalry.", $"{MonsterName} Slayer"),
            ];
        }
    }
}