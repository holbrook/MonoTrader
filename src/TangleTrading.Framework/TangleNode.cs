using Akka.Actor;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using Tangle.PluginModel;

namespace TangleTrading.Framework
{
    /// <summary>
    /// 
    /// MonoFramework 集群中的节点，负责以下职责：
    /// 
    /// 1. 插件容器。
    ///    可以自动发现插件，可以组装(MEF）
    ///    可以手工注入插件( AddExportValue)
    ///    可以获取插件实例
    /// 
    /// 2. 消息系统
    ///    管理 System, 所有的Actor, 消息通道的注册和管理等
    ///    从插件系统获取所有的 IPart, 根据配置包装成Actor  
    ///  
    ///    
    /// 3. 集群管理
    /// 
    /// 通过MEF加载插件，同时对外提供所有IQueries的引用，以便在展现层中注入
    /// </summary>
    public class TangleNode
    {
        public static ILog logger = LogManager.GetLogger("TangleNode");

        private AggregateCatalog _aggregate;
        public CompositionContainer Container;
        public ActorSystem System { get; private set; }
        private IActorRef cepActor;

        

        private Dictionary<IPart, IPartAttribute> _parts = new Dictionary<IPart, IPartAttribute>();
        //protected List<IPart> parts = new List<IPart>();

       // [Export]
        //private static SQLiteConnection db = new SQLiteConnection("MonoNode.db");
        //private static IOdb db = OdbFactory.Open("TangleNode.ndb");

       
        //public SQLiteConnection Conn { get { return db; } }

        public TangleNode(string nodeName = null, string pluginsPath = null)
        {
            //参数默认值
            if (null == nodeName)
                nodeName = "TangleNode";
            if (null == pluginsPath)
                pluginsPath = "Plugins";


            //初始化IOC容器
            _aggregate = new AggregateCatalog();
            Container = new CompositionContainer(_aggregate);

            _aggregate.Catalogs.Add(new AssemblyCatalog(this.GetType().Assembly));

            //从当前目录加载组件
            string path = Environment.CurrentDirectory;
            logger.Info(string.Format("从{0}加载组件...", path));

             _aggregate.Catalogs.Add(new DirectoryCatalog(path));

            //创建 actor system
            logger.Info(string.Format("创建MonoFramework节点 【{0}】", nodeName));
 
            //system = ActorSystem.Create(nodeName, config);
            //从App.config中读取配置
            System = ActorSystem.Create(nodeName);
            cepActor = System.ActorOf(Props.Create(() => new CEPActor()));

            //var inbox = Inbox.Create(System);
            //inbox.Watch(System.Mailboxes);



            List<Type> types = new List<Type>();
            //从目录加载组件
            logger.Info(string.Format("从{0}加载插件...", pluginsPath));
            var catalog = new DirectoryCatalog(Environment.CurrentDirectory + "/" + pluginsPath);
            _aggregate.Catalogs.Add(catalog);
            Container.ComposeParts(this);

            // Container.GetExportedValues<IPart>()
            var parts = Container.GetExports<IPart,IPartAttribute>();
            foreach (var kv in parts)
            {
                try
                {
                    _parts.Add(kv.Value, kv.Metadata);
                    logger.Info(string.Format("发现插件: {0}({1})", kv.Metadata.Name, kv.Value.GetType().FullName));
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                //通过反射，将所有监听的消息注册到NEsper
				//TODO: ICommand
                var part = kv.Value;
                var interfaces = part.GetType().GetInterfaces()
               .Where(x => typeof(IHandle).IsAssignableFrom(x) && x.IsGenericType);

                foreach (var @interface in interfaces)
                {
                    var type = @interface.GetGenericArguments()[0];
                    logger.Info(string.Format("    监听消息：{0}", type.FullName));

                    cepActor.Tell(type);

                    //所有消息都发送到CEPActor
                    System.EventStream.Subscribe(cepActor, type);

                    types.Add(type);

                }                
            }
            
            
        }




        public void AddAssemblies(IEnumerable<Assembly> assemblies)
        {
            foreach (var x in assemblies)
            {
                _aggregate.Catalogs.Add(new AssemblyCatalog(x));
            }

            //var batch = new CompositionBatch();
            //Container.Compose(batch);

            Container.ComposeParts(this);
        }



        

        //手工添加组件
        public void AddExportedValue<T>(T part)
        {
            var batch = new CompositionBatch();
            //batch.AddExportedValue(container);
            batch.AddExportedValue<T>(part);
            Container.Compose(batch);
        }



        //获取导出类型
        public Dictionary<TMeta, TPart> GetExports<TMeta, TPart>()
        {
            //必须定义Metadata的接口，不能直接使用类
            var imports = Container.GetExports<TPart, TMeta>();

            var ret = new Dictionary<TMeta, TPart>();

            foreach (var import in imports)
            {
                //Container.ComposeParts(import.Value);
                ret.Add(import.Metadata, import.Value);
            }

            return ret;
        }

        public T GetExportedValue<T>(string key = null)
        {
            var ret = Container.GetExportedValue<T>(key);
            Container.ComposeParts(ret);
            return ret;
        }

        public IEnumerable<T> GetExportedValues<T>(string key)
        {
            return Container.GetExportedValues<T>(key);
        }

        //创建Actor
        private void AddPart(IPart part, string name = null)
        {
            logger.Info("      添加部件");
            //parts.Add(part);

            //BridgeActor = system.ActorOf(Props.Create(() => new PartActor(part)), name);//.WithDispatcher("akka.actor.synchronized-dispatcher"));
        }


        
        //初始化
        public void Initialize()
        {

            
            foreach (var part in Container.GetExportedValues<IPart>())
            {
                Console.WriteLine("创建Actor:" + part.GetType().FullName);
                System.ActorOf(Props.Create(() => new PartActor(part)));//, part.GetType().FullName);
                part.Initialize(null);
                part.Start();
            }
        }

        //装配
        // import的，已经装配好了？


        //private void Composite()
        //{
        //    var batch = new CompositionBatch();
        //    //batch.AddExportedValue<IWindowManager>(new WindowManager());
        //    //batch.AddPart(this);

        //    var container = new CompositionContainer(_aggregate);
        //    container.Compose(batch);
        //}




    }


    /*
           var batch = new CompositionBatch();
           batch.AddExportedValue<IWindowManager>(windowManager);//將指定的导出加入至 CompositionBatch 物件  
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue<ILayout>(dockScreenManager);  
            batch.AddExportedValue(container);  
            batch.AddExportedValue(catalog);  
            container.Compose(batch);//在容器上执行组合，包括指定的 CompositionBatch 中的更改  
            container.ComposeParts(container.GetExportedValue<ILayout>());//由于DockScreenManager里有标记为Import的字段，所以要在MEF容器里组装把指定的部件导入  

   */



}
