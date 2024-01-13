using OrchidCavalry.Domain;
using OrchidCavalry.Popups;

namespace OrchidCavalry.Services
{
    public class ChoicePopupService : IChoicePopupService
    {
        private static Action choiceSelected;
        private readonly ChoiceView choiceView;

        public ChoicePopupService(ChoiceView choiceView)
        {
            this.choiceView = choiceView;
        }

        public async Task ShowAsync(Choice choice, Action<Guid> resultAction, INavigation navigation)
        {
            choiceView.LoadViewModel(choice,
                async (x) =>
                {
                    resultAction(x);
                    await navigation.PopModalAsync();
                },
                async () =>
                {
                    await navigation.PopModalAsync();
                });

            await navigation.PushModalAsync(this.choiceView, true);
        }
    }
}