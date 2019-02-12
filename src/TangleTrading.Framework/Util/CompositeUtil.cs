using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework
{
    public static class CompositeUtil
    {
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
        public static CompositionContainer CreateContainer(string[] paths)
        {
            var catalog = new AggregateCatalog();
            foreach (var path in paths)
            {
                //catalog.Catalogs.Add(new DirectoryCatalog(Environment.CurrentDirectory + path));
                var c = new DirectoryCatalog(path);
                catalog.Catalogs.Add(c);
            }

            var container = new CompositionContainer(catalog);
            return container;
        }
        public static Dictionary<TAttribute, TPlugin> FindPlugins<TPlugin, TAttribute>(CompositionContainer container)
        {
            Dictionary<TAttribute, TPlugin> dict = new Dictionary<TAttribute, TPlugin>();
            //必须定义Metadata的接口，不能直接使用类
            var imports = container.GetExports<TPlugin, TAttribute>();
            foreach (var import in imports)
            {
                dict.Add(import.Metadata, import.Value);
            }

            return dict;
        }

    }
}
