using OrchidCavalry.Models;
using OrchidCavalry.Views;

namespace OrchidCavalry.Services
{
    /// <summary>
    /// Creates a modal that shows the user a Character's attributes
    /// </summary>
    public class GameViewPopupService : IGameViewPopupService
    {
        /// <summary>
        /// The method to show the popup
        /// </summary>
        /// <param name="model">The model object that contains the characters</param>
        /// <returns>an awaitable task</returns>
        public async Task ShowPopupAsync<T>(Game game, INavigation navigation)
            where T : Page, IGameView
        {
            var view = Activator.CreateInstance<T>();
            view.LoadGame(game);
            await navigation.PushAsync(view, true);
        }
    }
}