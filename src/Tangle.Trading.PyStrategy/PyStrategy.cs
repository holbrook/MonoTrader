using System;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using MonoTrader.Market;
using Tangle.Trading.Base;
using Tangle.Trading.Stock;

namespace Tangle.Trading.PyStrategy
{
    /// <summary>
    /// 执行Python脚本编写的策略。
    /// 兼容RiceQuant()
    /// </summary>
    public class PyStrategy :IStrategy
    {
        //ScriptRuntime pyRuntime = Python.CreateRuntime();
        //dynamic py = pyRuntime.UseFile(@"E:\Test\test.py");

        ScriptEngine engine;
        ScriptScope scope;
        ScriptSource source;

        public PyStrategy()
        {
            engine = Python.CreateEngine();
            scope = engine.CreateScope();

            //传入函数
            scope.SetVariable("order_shares", (Func<string, int, OrderType,Order>)this.OrderShares);


        }

        public void HandleMatch(Match match)
        {

        }

        public void HandleTick(Tick tick)
        {

        }

        public void Initialize(dynamic config)
        {
            // 加载外部 python 脚本文件
            //obj = pyRunTime.UseFile(config.PythonFile);

            source = engine.CreateScriptSourceFromFile(config.PythonFile);
            source.Execute(scope);




            var welcome = scope.GetVariable<Func<object,string>>("welcome");
            // 简单调用脚本文件中的方法.
            Console.WriteLine(welcome("测试中文看看是否正常！"));

            //测试函数调用
            var testFunc = scope.GetVariable<Action<string,float>>("testFunc");
            testFunc("838006", 1000);
            //    var say_hello = scope.GetVariable<Func<object>>("say_hello");
        }

        private void AddImport()
        {
            /*
            可以这样一次性注入数据类型？ 使用 clr
            
            string importScript = "import sys" + Environment.NewLine +
                      "sys.path.append( r\"{0}\" )" + Environment.NewLine +
                      "from {1} import *";

            // python script to load
            string fullPath = @"c:\path\mymodule.py";

            var engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();

            // import the module
            string scriptStr = string.Format(importScript,
                                             Path.GetDirectoryName(fullPath),
                                             Path.GetFileNameWithoutExtension(fullPath));
            var importSrc = engine.CreateScriptSourceFromString(scriptStr, Microsoft.Scripting.SourceCodeKind.File);
            importSrc.Execute(scope);

            // now you ca execute one-line expressions on the scope e.g.
            string expr = "functionOfMyModule()";
            var result = engine.Execute(expr, scope);
            */           
        }


    }
}
