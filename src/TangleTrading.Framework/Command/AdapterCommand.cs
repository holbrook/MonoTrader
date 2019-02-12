using System;
namespace TangleTrading.Framework.Command
{
    public class AdapterCommand
    {
        public string Command { get; private set; }
        public AdapterCommand(string cmd)
        {
            Command = cmd;
        }
    }
}
