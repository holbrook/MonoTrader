using Tangle.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TangleNode node = new TangleNode("Demo-Node");


            node.Initialize();
            Console.ReadLine();
        }
    }
}
