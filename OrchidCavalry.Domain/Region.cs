using OrchidCavalry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Domain
{
    public class Region : Entity
    {
        public Region(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
        public Faction? RulingFaction { get; set; }
    }
}