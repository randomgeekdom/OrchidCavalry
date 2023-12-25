using OrchidCavalry.Domain;
using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    public interface ICityService
    {
        Task<City> GetUnthreatenedRandomCityAsync(Game game);
        Task IncreasePopulationAsync(IEnumerable<City> cities);
    }
}