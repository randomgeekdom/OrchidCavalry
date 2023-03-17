namespace OrchidCavalry.Models
{
    public record CouncilPosition
    {
        private static readonly IEnumerable<CouncilPosition> councilPositions = new List<CouncilPosition>()
            {
                new CouncilPosition(Skill.Combat, "Commander"),
                new CouncilPosition(Skill.Commercial, "Treasurer"),
                new CouncilPosition(Skill.Criminal, "Spymaster"),
                new CouncilPosition(Skill.Diplomacy, "Ambassador"),
                new CouncilPosition(Skill.Engineering, "Architect"),
                new CouncilPosition(Skill.Exploration, "Admiral"),
                new CouncilPosition(Skill.Mysticism, "Oracle"),
            };

        public CouncilPosition(Skill primarySkill, string title)
        {
            this.PrimarySkill = primarySkill;
            this.Title = title;
        }

        public Skill PrimarySkill { get; }
        public string Title { get;  }

        public static IEnumerable<CouncilPosition> GetAll()
        {
            return councilPositions;
        }

        public static CouncilPosition Get(Skill skill)
        {
            return GetAll().FirstOrDefault(x => x.PrimarySkill == skill);
        }
    }
}