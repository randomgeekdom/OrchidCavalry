using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Services
{
    public interface IAlertService
    {
        Task DisplayAlertAsync(string message, string header = "");
    }
}
