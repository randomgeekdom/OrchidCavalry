using OrchidCavalry.Models;
using OrchidCavalry.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Services
{
    public class CharacterPopupService : ICharacterPopupService
    {
        private readonly CharacterView characterView;

        public CharacterPopupService(CharacterView characterView)
        {
            this.characterView = characterView;
        }

        public async Task ShowCharacterAsync(CharacterPopupModel model)
        {
            characterView.LoadViewModel(model.Character);
            await model.Navigation.PushAsync(characterView, true);
        }
    }
}
