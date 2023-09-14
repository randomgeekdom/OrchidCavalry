using OrchidCavalry.Models.ValueTypes;

namespace OrchidCavalry.Domain
{
    public class Faction : ValueObject<string>
    {
        public static readonly Faction Akaden = new("The MachiNation", "A faction dedicated to stopping the spread of mysticism because they feel it hinders the progress of technology.");
        public static readonly Faction AngelBlessed = new("The Angel-Blessed", "A faction that believes they are the incarnations of angels and must spread the word of the Angelic Texts.");
        public static readonly Faction Barbarous = new("The Barbarous", "A faction that believes in selfish desires and that only the strong survive.");
        public static readonly Faction Magus = new("The Magus Empire", "A faction that feels that technology is blasphemy and unnatural to the ways of the world.");

        public Faction(string name, string description) : base(name)
        {
            this.Name = name;
            this.Description = description;
        }

        public string Description { get; }
        public string Name { get; }
    }
}