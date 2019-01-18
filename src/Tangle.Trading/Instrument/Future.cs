using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTrader.Instrument
{
    /// <summary>
    /// 期货
    /// </summary>
    public class Future : InstrumentBase
    {
        /// <summary>
        /// 期货合约最低保证金率
        /// </summary>
        public decimal MarginRate { get; set; }

        /// <summary>
        /// 合约乘数，例如沪深300股指期货的乘数为300.0
        /// </summary>
        public decimal ContractMultiplier { get; set; }

        /// <summary>
        /// 合约距离到期天数
        /// </summary>
        public int DaysToExpire { get
            {
                if(null == ListedDate)
                    return -1;

                return (DeListedDate - DateTime.Now).Days;
            }
        }

        public Future()
        {
            //期货全部为1
            RoundLot = 1;
        }
    }
}
