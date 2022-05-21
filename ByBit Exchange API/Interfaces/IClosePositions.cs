using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByBit_Exchange_API.Interfaces
{
    public interface IClosePositions
    {
        public Task<bool> closePosition(string symbol);
    }
}
