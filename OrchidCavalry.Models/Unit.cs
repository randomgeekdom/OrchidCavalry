using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrchidCavalry.Models.Enumerations;

namespace OrchidCavalry.Models
{
    public class Unit : Entity
    {
        public List<Character> Characters { get; set; } = new List<Character>();
        public Character Leader { get; set; }
        public string Name { get; set; }

        public List<PercentageAttribute<Nation>> Reputation { get; } = new List<PercentageAttribute<Nation>>();

        public void UpdateReputation(Nation nation, int valueIncrease)
        {
            var rep = Reputation.FirstOrDefault(x => x.Attribute == nation);
            if (rep == null)
            {
                rep = new PercentageAttribute<Nation>(nation, valueIncrease);
                Reputation.Add(rep);
            }

            rep.IncreaseValue(valueIncrease);
        }
    }
}