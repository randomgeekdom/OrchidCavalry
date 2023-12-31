using OrchidCavalry.Domain;
using OrchidCavalry.Popups;

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

        public async Task ShowAsync(Choice choice, Action<Guid> resultAction, INavigation navigation)
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