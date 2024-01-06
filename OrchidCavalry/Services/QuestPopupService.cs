using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Models;
using OrchidCavalry.Popups;
using OrchidCavalry.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Services
{
    /// <summary>
    /// Creates a modal that shows the user a Character's attributes
    /// </summary>
    public class QuestPopupService(QuestView questView, IChoicePopupService choicePopupService) : IQuestPopupService
    {
        /// <summary>
        /// The view that shows the character's characteristics
        /// </summary>
        private readonly QuestView questView = questView;
        private readonly IChoicePopupService choicePopupService = choicePopupService;

        /// <summary>
        /// The method to show the popup
        /// </summary>
        /// <param name="model">The model object that contains the characters</param>
        /// <returns>an awaitable task</returns>
        public async Task ShowPopupAsync(Game game, INavigation navigation)
        {
            questView.LoadViewModel(game, this.choicePopupService);
            await navigation.PushAsync(questView, true);
        }
    }
}
