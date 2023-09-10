//using Newtonsoft.Json;

//namespace OrchidCavalry.Models
//{
//    public class Skill
//    {
//        public Skill(string name)
//        {
//            this.Name = name;

//            var staticSkill = GetAll().First(x => x.Name == name);
//            this.BelowZeroTitle = staticSkill.BelowZeroTitle;
//            this.Titles = staticSkill.Titles;
//        }

//        private Skill(string name, string belowZeroTitle, params string[] titles)
//        {
//            this.Name = name;
//            this.BelowZeroTitle = belowZeroTitle;
//            this.Titles = titles;
//        }

//        public static Skill Academics { get; } = new Skill(nameof(Academics), "Dunce", "Student", "Tutor", "Teacher", "Professor", "Intellectual", "Technician", "Researcher", "Scientist", "Philosopher");

//        public static Skill Combat { get; } = new Skill(nameof(Combat), "Coward", "Scrapper", "Brawler", "Soldier", "Knight", "Marshal", "Captain");

//        public static Skill Commerce { get; } = new Skill(nameof(Skill.Commerce), "Vagrant", "Merchandiser", "Merchant", "Entrepreneur", "Investor", "Capitalist");

//        public static Skill Exploration { get; } = new Skill(nameof(Exploration), "Vanished", "Wanderer", "Scout", "Explorer", "Ranger", "Navigator", "Captain", "Commodore", "Admiral");

//        public static Skill Justice { get; } = new Skill(nameof(Justice), "Criminal", "Citizen", "Guard", "Warden", "Detective", "Inspector", "Prosecutor", "Defender", "Magistrate");

//        public static Skill Mechanics { get; } = new Skill(nameof(Mechanics), "Breaker", "Apprentice", "Fixer", "Crafter", "Builder", "Machinist", "Engineer", "Industrialist");

//        public static Skill Mysticism { get; } = new Skill(nameof(Mysticism), "Secularist", "Acolyte", "Monk", "Abbot", "Cleric", "Occultist", "Shaman", "Magus", "Spellbinder", "Magister");

//        public static Skill Nature { get; } = new Skill(nameof(Nature), "Artificialist", "Hiker", "Camper", "Gatherer", "Hunter", "Tamer", "Naturalist", "Druid");

//        public static Skill Politics { get; } = new Skill(nameof(Skill.Politics), "Introvert", "Extrovert", "Orator", "Debater", "Politician", "Representative", "Leader", "Minister", "Envoy", "Diplomat");

//        public static Skill Subterfuge { get; } = new Skill(nameof(Skill.Subterfuge), "Fugitive", "Rebel", "Rogue", "Renegade", "Agent", "Spy", "Spymaster");

//        [JsonIgnore]
//        public string BelowZeroTitle { get; }

//        public string Name { get; }

//        [JsonIgnore]
//        public string[] Titles { get; }

//        public static IEnumerable<Skill> GetAll()
//        {
//            return new List<Skill> {
//                Combat,
//                Commerce,
//                Subterfuge,
//                Politics,
//                Mechanics,
//                Exploration,
//                Mysticism,
//                Academics,
//                Justice,
//                Nature
//            };
//        }

//        public override bool Equals(object obj)
//        {
//            return this.Name == (obj as Skill)?.Name;
//        }

//        public override int GetHashCode()
//        {
//            return this.Name.GetHashCode();
//        }

//        public string GetTitle(int value)
//        {
//            var index = value / (100 / Titles.Length);

//            if (index < 0)
//            {
//                return this.BelowZeroTitle;
//            }
//            else if (index >= Titles.Length)
//            {
//                return Titles.Last();
//            }

//            return Titles[index];
//        }
//    }
//}