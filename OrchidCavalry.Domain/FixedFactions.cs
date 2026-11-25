namespace OrchidCavalry.Domain
{
    /// <summary>
    /// The factions that exist at the start of every new game
    /// </summary>
    public static class FixedFactions
    {
        public static readonly Guid OrderOfFireId = new("5d0b7a0e-3f5c-4c0b-9d55-2a6e8f1b7c34");

        public static List<Faction> Create()
        {
            return [
                new Faction("Order of Fire", 4) { Id = OrderOfFireId }
                ];
        }
    }
}
