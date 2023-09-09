using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Models.Tests
{
    public class GameTests
    {
        [Fact]
        public void InitializingGameShouldCreateCouncilAndHaveMainCharacter()
        {
            var game = new Game(new Character("test", "test"));
            Assert.NotNull(game.PlayerCharacter);
        }
    }
}
