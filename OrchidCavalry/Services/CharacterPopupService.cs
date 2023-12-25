using OrchidCavalry.Models;
using OrchidCavalry.Popups;
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
    public class CharacterPopupService(CharacterView characterView) : ICharacterPopupService
    {
        /// <summary>
        /// The view that shows the character's characteristics
        /// </summary>
        private readonly CharacterView characterView = characterView;

        /// <summary>
        /// The method to show the popup
        /// </summary>
        /// <param name="model">The model object that contains the characters</param>
        /// <returns>an awaitable task</returns>
        public async Task ShowCharacterAsync(CharacterPopupModel model)
        {
            characterView.LoadViewModel(model.Character);
            await model.Navigation.PushAsync(characterView, true);
        }
    }
}
