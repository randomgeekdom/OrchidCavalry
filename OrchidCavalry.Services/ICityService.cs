using OrchidCavalry.Domain;

namespace OrchidCavalry.Services
{
    public interface ICityService
    {
        Task<City> GetUnthreatenedRandomCityAsync(Game game);
        Task IncreasePopulationAsync(IEnumerable<City> cities);
    }
}