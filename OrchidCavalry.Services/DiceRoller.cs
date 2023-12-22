﻿using OrchidCavalry.Domain.Enumerations;
using OrchidCavalry.Domain.Interfaces;


namespace OrchidCavalry.Services
{
    public class DiceRoller : IDiceRoller
    {
        private readonly Random random = new();

        public DieResult Roll(int modifier, bool isDebilitated = false, bool isEnhanced = false)
        {
            var result = this.random.Next(1, 21);
            if (isEnhanced ^ isDebilitated)
            {
                var secondResult = this.random.Next(1, 21);
                if (isEnhanced)
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