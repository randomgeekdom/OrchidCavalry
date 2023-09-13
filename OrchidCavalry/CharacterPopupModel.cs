using OrchidCavalry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry
{
    public class CharacterPopupModel
    {
        public INavigation Navigation { get; set; }
        public Character Character { get; set; }
    }
}
