using OrchidCavalry.Domain;
using OrchidCavalry.Domain.Quests;

namespace OrchidCavalry.Models
{
    public class Game : Entity
    {
        /// <summary>
        /// Public constructor for loading JSON
        /// </summary>
        public Game()
        { }

        /// <summary>
        /// Constructor used for beginning of the game
        /// </summary>
        /// <param name="commander">Who the orchid commander is</param>
        public Game(Character commander)
        {
            Commander = commander;
            this.Characters.Add(commander);

            commander.MakeCommander();
        }

        /// <summary>
        /// The Alerts that get displayed after a turn is taken
        /// </summary>
        public List<Alert> Alerts { get; set; } = [];

        /// <summary>
        /// The members of the Orchid Cavalry
        /// </summary>
        public List<Character> Characters { get; set; } = [];

        /// <summary>
        /// The cities in the world
        /// </summary>
        public List<City> Cities { get; set; } = [];

        /// <summary>
        /// The leader of the Orchid Cavalry
        /// </summary>
        public Character? Commander { get; set; }

        public List<Guid> CompletedQuestIds { get; set; } = [];

        /// <summary>
        /// The quests that the cavalry can undertake or are currently undertaking
        /// </summary>
        public List<Quest> Quests { get; set; } = [];

        public List<Faction> Factions { get; set; } = [];

        /// <summary>
        /// Add an alert to display to the user before they take their next turn
        /// </summary>
        /// <param name="header">The header of the alert</param>
        /// <param name="message">The body of the alert</param>
        public void AddAlert(string header, string message)
        {
            Alerts.Add(new Alert(header, message));
        }

        public void AddCharacter(Character character)
        {
            this.Characters.Add(character);
        }

        public void AddNewCharacter(string fullName)
        {
            var splitName = fullName.Split(" ");
            var character = new Character(splitName[0], splitName[1]);
            this.Characters.Add(character);
        }

        public City AddNewCity(string name, Guid rulingFactionId)
        {
            var city = new City(name, rulingFactionId);
            this.Cities.Add(city);
            return city;
        }

        public void AddQuest(Quest quest)
        {
            this.Quests.Add(quest);
        }

        public City GetCityByName(string name)
        {
            return this.Cities.Single(x => x.Name == name);
        }

        public int GetNumberOfAvailableQuestSlots()
        {
            return this.Quests.Sum(x => x.GetRequiredNumberOfCharacters());
        }

        public int GetNumberOfUndeployedCharacters()
        {
            return this.Characters.Count(x => !x.IsDeployed);
        }

        public void KillCharacter(Character character)
        {
            this.Characters.Remove(character);
        }

        public void RemoveCityByName(string cityName)
        {
            this.Cities.Remove(GetCityByName(cityName));
        }

        public void RemoveQuest(Quest quest)
        {
            this.CompletedQuestIds.Add(quest.Id);
            this.Quests.Remove(quest);
        }

        /// <summary>
        /// Replace the leader.  Takes the highest ranking highest skilled member
        /// </summary>
        /// <returns></returns>
        public Character? ReplaceLeader() => this.Characters.OrderByDescending(x => x.GetRank()).ThenByDescending(x => x.Skills.Sum(y => y.Value.Value)).FirstOrDefault();

        public void AddToFactionReputation(Faction faction, int amount)
        {
            faction.OrchidCavalryReputation += amount;
        }

        public void AddToFactionReputation(Guid factionId, int amount)
        {
            var faction = this.Factions.First(x=>x.Id == factionId);
            this.AddToFactionReputation(faction, amount);
        }

        public Guid GetFactionByName(string bandName)
        {
            return this.Factions.First(x => x.Name == bandName).Id;
        }
    }
}