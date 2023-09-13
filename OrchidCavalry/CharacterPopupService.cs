using OrchidCavalry.Models;
using OrchidCavalry.Popups;
using OrchidCavalry.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry
{
    public class CharacterPopupService : ICharacterPopupService
    {
        private readonly CharacterView characterView;

        public CharacterPopupService(CharacterView characterView)
        {
            this.characterView = characterView;
        }

        public async Task ShowCharacterAsync(Character character, INavigation navigation)
        {
            characterView.LoadViewModel(character);
            await navigation.PushAsync(characterView, true);
        }
    }
}
