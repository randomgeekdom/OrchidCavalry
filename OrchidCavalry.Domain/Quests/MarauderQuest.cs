using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Domain.Services;
using OrchidCavalry.Models;

namespace OrchidCavalry.Domain.Quests
{
    /// <summary>
    /// A quest of 1 to 4 characters to stop a monster from terrorizing a city
    /// </summary>
    public class MarauderQuest(string title, string description, string bandName, string bandLeaderName, string cityName) : Quest(title, description, 5, 1, 3, cityName)
    {
        public string BandLeaderName { get; set; } = bandLeaderName;
        public string BandName { get; set; } = bandName;

        /// <summary>
        /// Create a monster quest
        /// </summary>
        /// <param name="monsterName">The name of the monster</param>
        /// <param name="cityName">The city where it's happening</param>
        /// <returns></returns>
        public static MarauderQuest Create(string bandName, string bandLeaderName, string cityName)
        {
            return new MarauderQuest($"Marauders!!!", $"A band of marauders has been seen heading toward the city of {cityName}", bandName, bandLeaderName, cityName);
        }

        public override async Task<string?> EvaluateFailStateAsync(Game game, IDiceRoller diceRoller)
        {
            return await Task.Run(() =>
            {
                var city = game.GetCityByName(this.CityName);
                var currentPopulation = city.Population;
                var newPopulation = currentPopulation;

                var maraudingComplete = false;
                var returnText = new List<string>();

                while (newPopulation > 0 && !maraudingComplete)
                {
                    var result = diceRoller.Roll(0, isDebilitated: true);

                    if (result < DieResult.Success)
                    {
                        newPopulation--;
                    }

                    if (result == DieResult.Catastrophe)
                    {
                        returnText.Add($"The band of marauders known as {BandName} have taken control of the city of {CityName}.");
                        game.GetCityByName(CityName).RulingFaction = BandName;
                        break;
                    }

                    if (result >= DieResult.MinorSuccess)
                    {
                        returnText.Add($"The band of marauders known as {BandName} has been been thwarted by the local population of {CityName}.");
                        maraudingComplete = true;
                    }
                }

                if (newPopulation < 10)
                {
                    game.RemoveCityByName(this.CityName);
                    returnText.Add($"The {BandName}'s rampage killed most of the people of {CityName}.  Whoever was left escaped and the city is no more.");
                }

                return string.Join(" ", returnText);
            });
        }

        public override List<QuestResolution> GetQuestResolutions(Game game)
        {
            return
            [
                new QuestResolution(Skill.Melee, $"The group of marauders called {BandName} was routed from the city of {CityName} thanks to the Orchid Cavalry.", $"Defender of {CityName}"),
                new QuestResolution(Skill.Target, $"The group of marauders called {BandName} was routed from the city of {CityName} thanks to the Orchid Cavalry.", $"Defender of {CityName}"),
                new QuestResolution(Skill.Influence, $"The group of marauders called {BandName} was thwarted.  Their leader, {BandLeaderName} has agreed to join the Orchid Cavalry.", $"Defender of {CityName}", extraAction:()=>game.AddNewCharacter(this.BandLeaderName))
            ];
        }
    }
}