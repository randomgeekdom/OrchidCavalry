using OrchidCavalry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Services
{
    public interface ICharacterPopupService
    {
        Task ShowCharacterAsync(Character character, INavigation navigation);
    }
}