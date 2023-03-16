using Newtonsoft.Json;

namespace OrchidCavalry.Models.Tests
{
    public class CouncilPositionTests
    {
        [Fact]
        public void CouncilPositionIsValueObject()
        {
            var skill = Skill.Diplomacy;
            var title = "Ambassador";

            var copy = new CouncilPosition(skill, title);
            Assert.Equal(CouncilPosition.Get(skill), copy);
        }

        [Fact]
        public void CanSerializeAndDeserializeCouncilPosition()
        {
            var councilPosition = CouncilPosition.GetAll().First();

            var serialized = JsonConvert.SerializeObject(councilPosition);
            var deserializedObject = JsonConvert.DeserializeObject<CouncilPosition>(serialized);

            Assert.Equal(councilPosition, deserializedObject);
        }
    }
}