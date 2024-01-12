using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Domain.Services;
using OrchidCavalry.Models;

namespace OrchidCavalry.Domain.Quests
{
    public abstract class Quest : Entity
    {
        public Quest(string title, string description, int expiration, int requiredNumberOfCharacters, int maxNumberOfCharacters, string cityName)
        {
            this.CityName = cityName;
            this.Description = description;
            this.Expiration = expiration;

            var optionalCharacters = maxNumberOfCharacters - requiredNumberOfCharacters;

            for (int i = 0; i < requiredNumberOfCharacters; i++)
            {
                this.CharacterSlots.Add(new QuestCharacterSlot(true));
            }

            for (int i = 0; i < optionalCharacters; i++)
            {
                this.CharacterSlots.Add(new QuestCharacterSlot(false));
            }

            this.Title = title;
        }

        public List<QuestCharacterSlot> CharacterSlots { get; set; } = [];
        public string CityName { get; set; }
        public string Description { get; set; }
        public int Expiration { get; set; }
        public List<Loot> Loot { get; set; } = [];
        public string Title { get; set; }

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

                while (!monsterRampageComplete && this.GetFilledCharacterSlots().Any())
                {
                    foreach (var characterSlot in this.GetFilledCharacterSlots().OrderBy(x => x.Character?.GetRank()).ToArray())
                    {
                        var character = characterSlot.Character!;

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
                            game.AddToFactionReputation(city.RulingFactionId, resolution.CityReputationModifier);

                            returnText.Add($"Special kudos to {character.GetNameAndRank()}");
                        }

                        if (result == DieResult.Catastrophe || result <= DieResult.Fail && character.Skills.All(x => x.IsDebilitated))
                        {
                            game.KillCharacter(character);

                            returnText.Add($"{character.GetNameAndRank()} has been killed in action.");
                            characterSlot.UnassignCharacter();
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
                    if (result != null)
                    {
                        returnText.Add(result);
                    }
                }
            }
            game.RemoveQuest(this);
            return string.Join(" ", returnText);
        }

        public IEnumerable<QuestCharacterSlot> GetFilledCharacterSlots() => CharacterSlots.Where(x => x.Character != null);

        public int GetMaxNumberOfCharacters() => this.CharacterSlots.Count;

        public abstract List<QuestResolution> GetQuestResolutions(Game game);

        public int GetRequiredNumberOfCharacters() => this.CharacterSlots.Where(x => x.IsMandatory).Count();

        public override string ToString()
        {
            return this.Title;
        }
    }
}