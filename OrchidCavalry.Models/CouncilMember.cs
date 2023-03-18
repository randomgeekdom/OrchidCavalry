namespace OrchidCavalry.Models
{
    public record CouncilMember
    {
        public CouncilMember(Character character, CouncilPosition councilPosition)
        {
            this.Character = character;
            this.CouncilPosition = councilPosition;
        }
        public CouncilPosition CouncilPosition { get; }
        public Character Character { get; }
    }
}