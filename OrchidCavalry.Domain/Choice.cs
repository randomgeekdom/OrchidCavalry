﻿using OrchidCavalry.Models.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Domain
{
    public class Choice : ValueObject<string>
    {
        public Choice(string value, Dictionary<Guid, string> options): base(value)
        {
            this.Options = options;
        }

        public Dictionary<Guid, string> Options { get; } = [];
    }
}
