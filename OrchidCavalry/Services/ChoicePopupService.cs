using OrchidCavalry.Domain;
using OrchidCavalry.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Services
{
    public class ChoicePopupService : IChoicePopupService
    {
        private readonly ChoiceView choiceView;

        public ChoicePopupService(ChoiceView choiceView)
        {
            this.choiceView = choiceView;
        }

        public async Task ShowAsync(Choice choice, INavigation navigation)
        {
            choiceView.LoadViewModel(choice);
            await navigation.PushModalAsync(this.choiceView, true);
        }
    }
}
