using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Models.ValueTypes
{
    public class SkillValue : ValueObject<byte>
    {
        public SkillValue(byte value):base(value)
        {
        }

        public static implicit operator byte(SkillValue value)
        {
            return value?.Value ?? 0;
        }

        public static implicit operator int(SkillValue value)
        {
            return value?.Value ?? 0;
        }

        public static implicit operator SkillValue(byte value)
        {
            return new SkillValue(Math.Min((byte)6, value));
        }

        public static implicit operator SkillValue(int value)
        {
            return new SkillValue((byte)Math.Max(Math.Min(-6, value), 0));
        }
    }
}