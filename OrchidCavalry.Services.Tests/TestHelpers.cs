using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Domain.Services;
using OrchidCavalry.Models;
using OrchidCavalry.Services;

namespace OrchidCavalry.Services.Tests
{
    internal class FakeDiceRoller : IDiceRoller
    {
        private readonly Queue<DieResult> results;

        public FakeDiceRoller(params DieResult[] results)
        {
            this.results = new Queue<DieResult>(results);
        }

        public List<DieResult> Rolled { get; } = [];

        public DieResult Roll(int modifier = 0, bool isDebilitated = false, bool isEnhanced = false)
        {
            var result = results.Count > 0 ? results.Dequeue() : DieResult.Fail;
            Rolled.Add(result);
            return result;
        }
    }

    internal class NoOpQuestService : IQuestService
    {
        public Task GenerateQuestsAsync(Game game) => Task.CompletedTask;
    }

    internal class StubCharacterService : ICharacterService
    {
        public Character GenerateCharacter() => new Character("Conscript", "Test");
    }
}