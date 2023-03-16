namespace OrchidCavalry.Models
{
    //public enum Skill
    //{
    //    Combat = 1,
    //    Exploration = 2,
    //    Mysticism = 3,
    //    Criminal = 4,
    //    Engineering = 5,
    //    Commercial = 6,
    //    Entertainment = 7,
    //    Diplomacy = 8
    //}

    public class Skill
    {
        public Skill(string name, string belowZeroTitle, params string[] titles)
        {
            this.Name = name;
            this.BelowZeroTitle = belowZeroTitle;
            this.Titles = titles;
        }

        public static Skill Combat { get; } = new Skill(nameof(Combat), "Coward", "Scrapper", "Brawler", "Guard", "Soldier", "Knight", "Marshal", "Captain");
        public static Skill Commercial { get; } = new Skill(nameof(Skill.Commercial), "Vagrant", "Merchandiser", "Merchant", "Entrepreneur", "Investor", "Capitalist");
        public static Skill Criminal { get; } = new Skill(nameof(Skill.Criminal), "Fugitive", "Pickpocket", "Burglar", "Thief", "Agent", "Spy");
        public static Skill Diplomacy { get; } = new Skill(nameof(Skill.Diplomacy), "Introvert", "Extrovert", "Orator", "Politician", "Representative", "Leader", "Minister", "Envoy", "Diplomat");
        public static Skill Engineering { get; } = new Skill(nameof(Skill.Engineering), "Breaker", todo);
        public static Skill Entertainment { get; } = new Skill(nameof(Skill.Entertainment), "Dullard", todo);
        public static Skill Exploration { get; } = new Skill(nameof(Exploration), "Wanderer", todo);
        public static Skill Mysticism { get; } = new Skill(nameof(Mysticism), "Secularist", todo);
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