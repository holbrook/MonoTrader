using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using TangleTrading.PyStrategy;
using TangleTrading.Strategy;

namespace TestPyStrategy
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            PyStrategy stgy = new PyStrategy();

            Context ctx = new Context();
            dynamic config = new ExpandoObject();
            config.PythonFile = "SpreadTrading.py";
            stgy.Initialize(ctx,config);

            //Save1();

        }


        static void Save1()
        {
            dynamic firstExpando = new ExpandoObject();
            firstExpando.Name = "Name";
            firstExpando.Age = 1;

            dynamic secondExpando = new ExpandoObject();
            secondExpando.Name = "SecondName";
            secondExpando.Age = 2;

            var expandoList = new List<ExpandoObject> { firstExpando, secondExpando };

            var list = expandoList.Select(expando => (IDictionary<string, object>)expando)
                                  .ToList();

            var dataContractSerializer = new DataContractSerializer(list.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                dataContractSerializer.WriteObject(memoryStream, list);
                string outputXml = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }
    }
}
