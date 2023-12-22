using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Domain.Interfaces;
using OrchidCavalry.Models;
using OrchidCavalry.Models.ValueTypes;

namespace OrchidCavalry.Domain.Quests
{
    /// <summary>
    /// A quest of 1 to 4 characters to stop a monster from terrorizing a city
    /// </summary>
    public class MonsterHuntQuest : Quest
    {
        public MonsterHuntQuest(string title, string description, string monsterName, string cityName) : base(title, description, 5, 1, 4, cityName)
        {
            this.MonsterName = monsterName;
        }

        public string MonsterName { get; set; }

        /// <summary>
        /// Evaluates the results of the quest
        /// </summary>
        /// <param name="game"></param>
        /// <param name="diceRoller"></param>
        /// <returns></returns>
        public override string EvaluateQuest(Game game, IDiceRoller diceRoller)
        {
            var returnText = new List<string>();
            if (Expiration > 0)
            {
                Expiration--;
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
            return string.Join(" ", returnText);
        }
    }
}