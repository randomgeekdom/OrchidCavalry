using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Domain.Services;
using OrchidCavalry.Models;

namespace OrchidCavalry.Domain.Quests
{
    public abstract class Quest(string title, string description, int expiration, int requiredNumberOfCharacters, int maxNumberOfCharacters, string cityName) : Entity
    {
        public List<Character> Characters { get; set; } = [];
        public string CityName { get; set; } = cityName;
        public string Description { get; set; } = description;
        public int Expiration { get; set; } = expiration;
        public bool IsActive { get; set; }
        public int MaxNumberOfCharacters { get; set; } = maxNumberOfCharacters;
        public int RequiredNumberOfCharacters { get; set; } = requiredNumberOfCharacters;
        public string Title { get; set; } = title;

        public void AddCharacter(Character character)
        {
            if (Characters.Count >= RequiredNumberOfCharacters)
            {
                return;
            }

            Characters.Add(character);
            character.IsDeployed = true;
        }

        public abstract Task<string?> EvaluateFailStateAsync(Game game, IDiceRoller diceRoller);

        public async Task<string?> EvaluateQuestAsync(Game game, IDiceRoller diceRoller)
        {
            var city = game.GetCityByName(CityName);
            var returnText = new List<string>();
            if (Expiration > 0)
            {
                Expiration--;
                return null;
            }
            else
            {
                var monsterRampageComplete = false;

                while (!monsterRampageComplete && this.Characters.Any())
                {
                    foreach (var character in this.Characters.OrderBy(x => x.GetRank()).ToArray())
                    {
                        var questResolutions = this.GetQuestResolutions(game);

                        var highestSkill = character.Skills
                            .Where(x => questResolutions.Select(x => x.Skill).Contains(x.Skill))
                            .OrderByDescending(x => x.Value)
                            .FirstOrDefault();

                        if (highestSkill == null)
                        {
                            var randomResolutionSkill = questResolutions.OrderBy(x => Guid.NewGuid()).First().Skill;
                            highestSkill = character.UpdateCharacterSkillValue(randomResolutionSkill, 0);
                        }

                        var result = diceRoller.Roll(highestSkill.Value, highestSkill.IsDebilitated, highestSkill.IsEnhanced);

                        if (result >= DieResult.MinorSuccess)
                        {
                            monsterRampageComplete = true;

                            var resolution = questResolutions.Single(x => x.Skill == highestSkill.Skill);
                            returnText.Add(resolution.SuccessText);
                            character.AddTitle(resolution.VictoryTitle);
                            resolution.ExtraAction?.Invoke();
                            city.OrchidCavalryReputation += resolution.CityReputationModifier;

                            returnText.Add($"Special kudos to {character.GetNameAndRank()}");
                        }

                        if (result == DieResult.Catastrophe || result <= DieResult.Fail && character.Skills.All(x => x.IsDebilitated))
                        {
                            game.KillCharacter(character);

                            returnText.Add($"{character.GetNameAndRank()} has been killed in action.");
                            this.Characters.Remove(character);
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

                // If not resolved, figure out what happens
                if (!monsterRampageComplete)
                {
                    var result = await this.EvaluateFailStateAsync(game, diceRoller);
                    if(result != null)
                    {
                        returnText.Add(result);
                    }
                }
            }
            game.RemoveQuest(this);
            return string.Join(" ", returnText);
        }

        public abstract List<QuestResolution> GetQuestResolutions(Game game);

        public void RemoveCharacter(Character character)
        {
            Characters.Remove(character);
            character.IsDeployed = false;
        }
    }
}