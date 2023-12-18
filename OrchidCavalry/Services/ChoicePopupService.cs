using OrchidCavalry.Domain;
using OrchidCavalry.Popups;
using OrchidCavalry.ViewModels;
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
        private static Action choiceSelected;

        public ChoicePopupService(ChoiceView choiceView)
        {
            this.choiceView = choiceView;
        }

        public Action ChoiceSelected { get => choiceSelected; set => choiceSelected = value; }

        public async Task ShowAsync(Choice choice, Action<int> resultAction, INavigation navigation)
        {
            choiceView.LoadViewModel(choice, async (x) =>
            {
                resultAction(x);
                await navigation.PopModalAsync();
                this.ChoiceSelected();
            });
            await navigation.PushModalAsync(this.choiceView, true);
        }
    }
}