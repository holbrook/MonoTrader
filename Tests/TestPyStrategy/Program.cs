using System;
using System.Dynamic;
using Tangle.Trading.PyStrategy;

namespace TestPyStrategy
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            PyStrategy stgy = new PyStrategy();
            dynamic config = new ExpandoObject();
            config.PythonFile = "hello.py";
            stgy.Initialize(config);
        }
    }
}
