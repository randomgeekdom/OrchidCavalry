namespace OrchidCavalry.Services
{
    public class Randomizer : IRandomizer
    {
        private readonly Random random;
        public bool GetBool(int percent = 50)
        {
            return this.random.Next(0,101) <= percent;
        }
    }
}
