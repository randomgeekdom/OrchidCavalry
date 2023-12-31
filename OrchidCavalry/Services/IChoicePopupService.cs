using OrchidCavalry.Domain;

namespace OrchidCavalry.Services
{
    public interface IChoicePopupService
    {
        Task ShowAsync(Choice choice, Action<Guid> resultAction, INavigation navigation);
        Action ChoiceSelected { get; set; }
    }
}