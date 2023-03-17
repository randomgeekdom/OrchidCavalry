namespace OrchidCavalry.Models
{
    public class Skill
    {
        public override bool Equals(object obj)
        {
            return this.Name == (obj as Skill)?.Name;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public Skill(string name, string belowZeroTitle, params string[] titles)
        {
            this.Name = name;
            this.BelowZeroTitle = belowZeroTitle;
            this.Titles = titles;
        }

        public static IEnumerable<Skill> GetAll()
        {
            return new List<Skill> { 
                Combat,
                Commercial,
                Criminal,
                Diplomacy,
                Engineering,
                Exploration,
                Mysticism
            };
        }

        public static Skill Combat { get; } = new Skill(nameof(Combat), "Coward", "Scrapper", "Brawler", "Guard", "Soldier", "Knight", "Marshal", "Captain");
        public static Skill Commercial { get; } = new Skill(nameof(Skill.Commercial), "Vagrant", "Merchandiser", "Merchant", "Entrepreneur", "Investor", "Capitalist");
        public static Skill Criminal { get; } = new Skill(nameof(Skill.Criminal), "Fugitive", "Pickpocket", "Burglar", "Thief", "Agent", "Spy");
        public static Skill Diplomacy { get; } = new Skill(nameof(Skill.Diplomacy), "Introvert", "Extrovert", "Orator", "Politician", "Representative", "Leader", "Minister", "Envoy", "Diplomat");
        public static Skill Engineering { get; } = new Skill(nameof(Skill.Engineering), "Breaker", "Apprentice", "Fixer", "Crafter", "Builder", "Machinist", "Engineer", "Industrialist");
        public static Skill Exploration { get; } = new Skill(nameof(Exploration), "Vanished", "Wanderer", "Scout", "Explorer", "Ranger", "Navigator", "Captain", "Commodore", "Admiral");
        public static Skill Mysticism { get; } = new Skill(nameof(Mysticism), "Secularist", "Acolyte", "Monk", "Abbot", "Cleric", "Occultist", "Shaman", "Magus", "Spellbinder", "Magister");
        
        public string BelowZeroTitle { get; }
        
        
        public string[] Titles { get; }
        public string Name { get; }

        public string GetTitle(int value)
        {
            var index = value / (100/Titles.Length);

            if (index < 0)
            {
                return this.BelowZeroTitle;
            }
            else if(index >= Titles.Length)
            {
                return Titles.Last();
            }

            return Titles[index];
        }
    }

}