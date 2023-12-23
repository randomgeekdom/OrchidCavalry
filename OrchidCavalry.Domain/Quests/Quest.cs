using OrchidCavalry.Domain.Services;
using OrchidCavalry.Models;

namespace OrchidCavalry.Domain.Quests
{
    public abstract class Quest : Entity
    {
        public Quest(string title, string description, int expiration, int requiredNumberOfCharacters, int maxNumberOfCharacters, string cityName)
        {
            Title = title;
            Description = description;
            Expiration = expiration;
            RequiredNumberOfCharacters = requiredNumberOfCharacters;
            this.CityName = cityName;
        }

        public List<Character> Characters { get; set; } = new List<Character>();
        public required string Description { get; set; }
        public int Expiration { get; set; }
        public bool IsActive { get; set; }
        public int RequiredNumberOfCharacters { get; set; }
        public int MaxNumberOfCharacters { get; set; }
        public required string Title { get; set; }
        public string CityName { get; set; }

        public void AddCharacter(Character character)
        {
            if (Characters.Count >= RequiredNumberOfCharacters)
            {
                return;
            }

            Characters.Add(character);
            character.IsDeployed = true;
        }

        public abstract string EvaluateQuest(Game game, IDiceRoller diceRoller);

        public void RemoveCharacter(Character character)
        {
            Characters.Remove(character);
            character.IsDeployed = false;
        }
    }
}