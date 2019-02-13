using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TangleTrading.Command
{
    public class SpeechCommand : ICommand
    {
        public int Volume {get;private set; }    //设置朗读音量 [范围 0 ~ 100] 
        public int Rate { get; private set; }      //设置朗读频率 [范围  -10 ~ 10] 
        public string Message { get; private set; }

        public SpeechCommand(string msg, int volume=100, int rate=0)
        {
            Message = msg.Trim();
            Volume = volume;
            if (Volume > 100)
                Volume = 100;
            if (Volume < 0)
                Volume = 0;
            Rate = rate;
            if (Rate > 10)
                Rate = 10;
            if (Rate < -10)
                Rate = -10;
        }
    }
}
