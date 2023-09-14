using OrchidCavalry.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Services
{
    public class DiceRoller : IDiceRoller
    {
        private readonly Random random = new Random();

        public DieResult Roll(int modifier, bool? epicRoll = null)
        {
            var result = this.random.Next(1, 21);
            if (epicRoll != null)
            {
                var secondResult = this.random.Next(1, 21);
                if (epicRoll.Value)
                {
                    result = Math.Max(result, secondResult);
                }
                else
                {
                    result = Math.Min(result, secondResult);
                }
            }

            if (result == 20)
            {
                return DieResult.PerfectSuccess;
            }

            if (result == 1)
            {
                return DieResult.Catastrophe;
            }

            var totalResult = result + modifier;
            if (result >= 18)
            {
                return DieResult.Success;
            }
            else if (totalResult >= 10)
            {
                return DieResult.MinorSuccess;
            }
            else
            {
                return DieResult.Fail;
            }
        }
    }
}