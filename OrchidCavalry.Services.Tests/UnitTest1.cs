namespace OrchidCavalry.Services.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var generator = new Mudless.NameGenerator.NameGenerator();
            var x = generator.Generate();
            Console.WriteLine();
        }
    }
}