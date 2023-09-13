using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Models.ValueTypes
{
    public class TraitValue : ValueObject<byte>
    {
        public TraitValue(byte value):base(value)
        {
        }

        public static implicit operator byte(TraitValue value)
        {
            return value?.Value ?? 0;
        }

        public static implicit operator int(TraitValue value)
        {
            return value?.Value ?? 0;
        }

        public static implicit operator TraitValue(byte value)
        {
            return new TraitValue(Math.Min((byte)6, value));
        }

        public static implicit operator TraitValue(int value)
        {
            return new TraitValue((byte)Math.Max(Math.Min(-6, value), 0));
        }
    }
}