using OrchidCavalry.Domain.Services;
using OrchidCavalry.Models;

namespace OrchidCavalry.Domain.Quests
{
    public abstract class Quest(string title, string description, int expiration, int requiredNumberOfCharacters, string cityName) : Entity
    {
        public List<Character> Characters { get; set; } = new List<Character>();
        public string Description { get; set; } = description;
        public int Expiration { get; set; } = expiration;
        public bool IsActive { get; set; }
        public int RequiredNumberOfCharacters { get; set; } = requiredNumberOfCharacters;
        public int MaxNumberOfCharacters { get; set; }
        public string Title { get; set; } = title;
        public string CityName { get; set; } = cityName;

        public void AddCharacter(Character character)
        {
            if (Characters.Count >= RequiredNumberOfCharacters)
            {
                return;
            }

            Characters.Add(character);
            character.IsDeployed = true;
        }

        public abstract Task<string?> EvaluateQuestAsync(Game game, IDiceRoller diceRoller);

        public void RemoveCharacter(Character character)
        {
            Characters.Remove(character);
            character.IsDeployed = false;
        }
    }
}