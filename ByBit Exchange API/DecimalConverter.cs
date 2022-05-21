using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByBit_Exchange_API
{
    public static class DecimalConverter
    {
        public static decimal WithDecimalDigitsOf(this decimal d, int numberOfDigits = 0)
        {
            int factor = (int)Math.Pow(10, numberOfDigits);
            d *= factor;
            d = Math.Truncate(d);
            d /= factor;
            return d;
        }
    }
}
