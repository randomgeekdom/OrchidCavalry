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

        /// <summary>
        /// Evaluates the results of the quest
        /// </summary>
        /// <param name="game"></param>
        /// <param name="diceRoller"></param>
        /// <returns></returns>
        public override async Task<string?> EvaluateQuestAsync(Game game, IDiceRoller diceRoller)
        {
            return await Task.Run(() =>
            {
                var returnText = new List<string>();
                if (Expiration > 0)
                {
                    Expiration--;
                    return null;
                }
                else
                {
                    var monsterRampageComplete = false;
                    var deadCharacters = new List<string>();

                    while (!monsterRampageComplete && this.Characters.Any())
                    {
                        foreach (var character in this.Characters.OrderBy(x => x.GetRank()).ToArray())
                        {
                            var necessarySkills = new List<Skill> { Skill.Aura, Skill.Shoot, Skill.Melee };
                            var highestSkill = character.Skills
                                .Where(x => necessarySkills.Contains(x.Skill))
                                .OrderByDescending(x => x.Value)
                                .First();

                            var result = diceRoller.Roll(highestSkill.Value, highestSkill.IsDebilitated, highestSkill.IsEnhanced);

                            if (result >= DieResult.MinorSuccess)
                            {
                                monsterRampageComplete = true;

                                switch (highestSkill.Skill)
                                {
                                    case Skill.Aura:
                                        returnText.Add($"The {MonsterName} was routed from the city of {CityName} thanks to the Orchid Cavalry.");
                                        character.AddTitle($"{MonsterName} Whisperer");
                                        break;

                                    case Skill.Shoot:
                                    case Skill.Melee:
                                        returnText.Add($"The {MonsterName} has been slain in the city of {CityName} thanks to the Orchid Cavalry.");
                                        character.AddTitle($"{MonsterName} Slayer");
                                        break;
                                }

                                returnText.Add($"Special kudos to {character.GetNameAndRank()}");
                            }

                            if (result == DieResult.Catastrophe || result <= DieResult.Fail && character.Skills.All(x => x.IsDebilitated))
                            {
                                game.KillCharacter(character);
                                deadCharacters.Add(character.GetNameAndRank());
                            }
                            else
                            {
                                if (result == DieResult.Fail)
                                {
                                    character.UpdateCharacterSkillValue(highestSkill.Skill, 1);
                                    character.DebilitateRandomCharacterSkill();
                                }

                                if (result <= DieResult.MinorSuccess)
                                {
                                    character.DebilitateRandomCharacterSkill();
                                }

                                if (result == DieResult.PerfectSuccess)
                                {
                                    character.EnhanceRandomCharacterSkill();
                                }
                            }
                        }
                    }

                    var city = game.GetCityByName(this.CityName);
                    var currentPopulation = city.Population;
                    var newPopulation = currentPopulation;

                    while (newPopulation > 0 && !monsterRampageComplete)
                    {
                        var result = diceRoller.Roll(0, isDebilitated: true);

                        if (result < Enumerations.DieResult.Success)
                        {
                            newPopulation--;
                        }

                        if (result == Enumerations.DieResult.Catastrophe)
                        {
                            newPopulation /= 2;
                        }

                        if (result >= Enumerations.DieResult.MinorSuccess)
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
                }
                game.RemoveQuest(this);
                return string.Join(" ", returnText);
            });
        }
    }
}