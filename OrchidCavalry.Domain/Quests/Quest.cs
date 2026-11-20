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
            var questSucceeded = false;
            if (Expiration > 0)
            {
                Expiration--;
                return null;
            }
            else
            {
                var monsterRampageComplete = false;

                if (this.EvaluatePreliminaryStages(game, diceRoller, returnText))
                {
                    var success = this.RollUntilSuccess(game, diceRoller, returnText, () => this.GetQuestResolutions(game).Select(x => x.Skill).ToList());

                    if (success != null)
                    {
                        monsterRampageComplete = true;
                        questSucceeded = true;

                        var (character, skill) = success.Value;
                        var resolution = this.GetQuestResolutions(game).Single(x => x.Skill == skill);
                        returnText.Add(resolution.SuccessText);
                        character.AddTitle(resolution.VictoryTitle);
                        character.AddVictory();
                        resolution.ExtraAction?.Invoke();
                        game.AddToFactionReputation(city.RulingFactionId, resolution.CityReputationModifier);

                        returnText.Add($"Special kudos to {character.GetNameAndRank()}");
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
            game.RemoveQuest(this, questSucceeded);
            return string.Join(" ", returnText);
        }

        /// <summary>
        /// Stages that must succeed before the quest's resolutions are rolled.  Returns false if the quest failed during them.
        /// </summary>
        protected virtual bool EvaluatePreliminaryStages(Game game, IDiceRoller diceRoller, List<string> returnText) => true;

        /// <summary>
        /// Assigned characters roll, lowest rank first, until one succeeds or none remain.  Applies death, debilitation and enhancement.
        /// Returns the successful character and the skill they used, or null if the party was wiped out.
        /// </summary>
        protected (Character Character, Skill Skill)? RollUntilSuccess(Game game, IDiceRoller diceRoller, List<string> returnText, Func<List<Skill>> getSkills)
        {
            while (this.GetFilledCharacterSlots().Any())
            {
                foreach (var characterSlot in this.GetFilledCharacterSlots().OrderBy(x => x.Character?.GetRank()).ToArray())
                {
                    var character = characterSlot.Character!;

                    var skills = getSkills();

                    var highestSkill = character.Skills
                        .Where(x => skills.Contains(x.Skill))
                        .OrderByDescending(x => x.Value)
                        .FirstOrDefault();

                    if (highestSkill == null)
                    {
                        var randomSkill = skills.OrderBy(x => Guid.NewGuid()).First();
                        highestSkill = character.UpdateCharacterSkillValue(randomSkill, 0);
                    }

                    var result = diceRoller.Roll(highestSkill.Value, highestSkill.IsDebilitated, highestSkill.IsEnhanced);

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

                    if (result >= DieResult.MinorSuccess)
                    {
                        return (character, highestSkill.Skill);
                    }
                }
            }

            return null;
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