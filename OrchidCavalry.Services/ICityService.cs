using OrchidCavalry.Domain;
using OrchidCavalry.Models;

namespace OrchidCavalry.Services
{
    public interface ICityService
    {
        City GetRandomCity(Game game);
    }
}