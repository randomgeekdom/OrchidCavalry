using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Models.ValueTypes
{
    public class Percent(byte value) : ValueObject<byte>(value)
    {
        public static implicit operator byte(Percent value)
        {
            return value?.Value ?? 0;
        }

        public static implicit operator int(Percent value)
        {
            return value?.Value ?? 0;
        }

        public static implicit operator Percent(byte value)
        {
            return new Percent(Math.Min((byte)99, value));
        }

        public static implicit operator Percent(int value)
        {
            return new Percent((byte)Math.Max(Math.Min(99, value), 0));
        }
    }
}