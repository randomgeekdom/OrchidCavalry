using OrchidCavalry.Domain.Services;
using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    /// <summary>
    /// The Gameplay service is the primary driver of Orchid Cavalary.  It determines all the steps that occur when a turn is taken.
    /// </summary>
    /// <param name="characterService">The character service for generating new characters</param>
    /// <param name="gameSaver">The service that saves and loads the game from the disk</param>
    /// <param name="diceRoller">Dice roller for skill checks</param>
    /// <param name="questService">The service for generating quests</param>
    public class GameplayService(ICharacterService characterService, IGameSaver gameSaver, IDiceRoller diceRoller, IQuestService questService) : IGameplayService
    {
        private readonly ICharacterService characterService = characterService;

        private readonly IDiceRoller diceRoller = diceRoller;
        private readonly IGameSaver gameSaver = gameSaver;
        private readonly IQuestService questService = questService;
        private readonly Random random = new();

        /// <summary>
        /// What occurs when the player hits the "Next Turn" button.
        /// </summary>
        /// <param name="game">The game state</param>
        /// <returns></returns>
        public async Task NextTurnAsync(Game game)
        {
            foreach (var quest in game.Quests.ToList())
            {
                var alert = await quest.EvaluateQuestAsync(game, this.diceRoller);
                if (alert != null)
                {
                    game.AddAlert("Quest Ended", alert);
                }
            }

            await this.questService.GenerateQuestsAsync(game);

            await RecruitConscriptsIfNecessaryAsync(game);
            await ReplaceCommanderIfNecessaryAsync(game);

            await this.gameSaver.SaveGameAsync(game);
        }

        private async Task RecruitConscriptsIfNecessaryAsync(Game game)
        {
            await Task.Run(() =>
            {
                if (game.Characters.Count < 8)
                {
                    var numberOfConscripts = this.random.Next(2, 4);
                    var conscripts = new List<Character>();
                    for (int i = 0; i < numberOfConscripts; i++)
                    {
                        var character = this.characterService.GenerateCharacter();
                        conscripts.Add(character);
                        game.Characters.Add(character);
                    }

                    var alertText = $"Due to the lack of members, you have conscripted {conscripts.Count} people into the Orchid Cavalry from the civilian population of Orchid Island: {string.Join(", ", conscripts.Select(x => x.GetName()))}";
                    game.Alerts.Add(new Alert("Civilians Conscripted", alertText));
                }
            });
        }

        private async Task ReplaceCommanderIfNecessaryAsync(Game game)
        {
            await Task.Run(() =>
            {
                if (!game.Characters.Contains(game.Commander))
                {
                    var previousCommander = game.Commander;
                    game.ReplaceLeader();
                    game.Alerts.Add(new Alert("A New Commander", $"Having lost Commander {previousCommander.GetName()}, a new commander has taking leadership of the Orchid Cavalry: {game.Commander.GetNameAndRank()}"));
                }
            });
        }
    }
}