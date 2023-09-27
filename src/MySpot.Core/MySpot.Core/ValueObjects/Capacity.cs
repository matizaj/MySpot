using MySpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.ValueObjects
{
    public sealed record Capacity
    {
        public int Value { get; set; }
        public Capacity(int value)
        {
            if(value < 0 && value > 4)
            {
                throw new InvalidCapacityException(value);
            }
            Value = value;
        }

        public static implicit operator Capacity(int value) {  return new Capacity(value); }
        public static implicit operator int(Capacity value) { return value.Value;}
    }
}
