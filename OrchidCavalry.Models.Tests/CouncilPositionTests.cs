using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Text.Json;

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
    }
}