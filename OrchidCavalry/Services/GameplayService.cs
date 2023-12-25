﻿using OrchidCavalry.Domain;
using OrchidCavalry.Domain.Services;
using OrchidCavalry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Services
{
    public class GameplayService(ICharacterService characterService, IGameSaver gameSaver, IDiceRoller diceRoller, IQuestService questService) : IGameplayService
    {
        private readonly ICharacterService characterService = characterService;

        private readonly IGameSaver gameSaver = gameSaver;
        private readonly IDiceRoller diceRoller = diceRoller;
        private readonly IQuestService questService = questService;
        private readonly Random random = new();

        public async Task NextTurnAsync(Game game, INavigation navigation)
        {
            await Task.Run(() =>
            {
                foreach (var quest in game.Quests.ToList())
                {
                    quest.EvaluateQuest(game, this.diceRoller);
                }
            });

            await this.questService.GenerateQuestsAsync(game);

            RecruitConscriptsIfNecessary(game);
            ReplaceCommanderIfNecessary(game);

            await this.gameSaver.SaveGameAsync(game);
        }

        private void ReplaceCommanderIfNecessary(Game game)
        {
            if (!game.Characters.Contains(game.Commander))
            {
                var previousCommander = game.Commander;
                game.ReplaceLeader();
                game.Alerts.Add(new Alert("A New Commander", $"Having lost Commander {previousCommander.GetName()}, a new commander has taking leadership of the Orchid Cavalry: {game.Commander.GetNameAndRank()}"));
            }
        }

        private void RecruitConscriptsIfNecessary(Game game)
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
        }
    }
}