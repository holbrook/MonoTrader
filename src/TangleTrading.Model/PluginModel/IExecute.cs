using System;
using TangleTrading.Model;
using TangleTrading.Model.Command;

namespace Tangle.PluginModel
{
    public interface IExecute
    {
    }

    public interface IExecute<in TCmd> : IExecute
        where TCmd : ICommand
    {
        ExecuteStatus Execute(TCmd cmd);
    }

    public interface IExecute<in TCmd, TStatus> :IExecute
        where TCmd: ICommand<TStatus>
        where TStatus: ExecuteStatus
    {
        TStatus Execute(TCmd cmd);
    }
}
