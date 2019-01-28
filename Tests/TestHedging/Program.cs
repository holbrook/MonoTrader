using System;

namespace TestHedging
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public void LoadParts()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly()); //创建一个程序集目录，用于从一个程序集获取所有的组件定义   
            var container = new CompositionContainer(catalog); //创建一个组合容器   
            //var composablePart = new MyComponent();
            //container.ComposeParts(composablePart); //执行组合，从容器中获取ExportDefinition并创建实例组合在一起   
            // composablePart组合完成以供使用 
        }
    }
}
