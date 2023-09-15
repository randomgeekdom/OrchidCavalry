using OrchidCavalry.Domain;

namespace OrchidCavalry.Services
{
    public interface IChoicePopupService
    {
        Task ShowAsync(Choice choice, INavigation navigation);
    }
}