using MonoTrader.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTrader.Stock
{
    public class Account:AccountBase
    {
        public Dictionary<string, Position> Positions { get; set; }
    }
}
