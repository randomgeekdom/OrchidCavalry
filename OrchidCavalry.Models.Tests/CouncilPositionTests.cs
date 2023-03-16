using Newtonsoft.Json;

namespace OrchidCavalry.Models.Tests
{
    public class CouncilPositionTests
    {
        [Theory]
        [InlineData(Skill.Diplomacy, "Ambassador")]
        public void CouncilPositionIsValueObject(Skill skill, string title)
        {
            var copy = new CouncilPosition(skill, title);
            Assert.Equal(CouncilPosition.Get(skill), copy);
        }

        [Fact]
        public void CanSerializeAndDeserializeCouncilPosition()
        {
            var councilPosition = CouncilPosition.GetAll().First();

            var serialized = JsonConvert.SerializeObject(councilPosition);

            Assert.Equal(councilPosition, JsonConvert.DeserializeObject<CouncilPosition>(serialized));
        }
    }
}