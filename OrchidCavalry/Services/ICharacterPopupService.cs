namespace OrchidCavalry.Services
{
    public interface ICharacterPopupService
    {
        Task ShowCharacterAsync(CharacterPopupModel model);
    }
}