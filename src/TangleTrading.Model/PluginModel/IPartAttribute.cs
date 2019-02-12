using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangle.PluginModel
{
    /// <summary>
    /// <see cref="IPart"/>的属性定义。MonoFramework通过IMonoPart的属性生命发现和识别插件。
    /// </summary>
    public interface IPartAttribute
    {
        string Name { get; }
    }
}
