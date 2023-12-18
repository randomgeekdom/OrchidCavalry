using OrchidCavalry.Domain;

namespace OrchidCavalry.Services
{
    public interface IChoicePopupService
    {
        Task ShowAsync(Choice choice, Action<int> resultAction, INavigation navigation);
        Action ChoiceSelected { get; set; }
    }
}