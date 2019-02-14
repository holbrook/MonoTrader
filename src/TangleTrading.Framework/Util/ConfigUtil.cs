using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TangleTrading.Framework.Util
{
    public class ConfigUtil
    {
        public static dynamic Load(string filename, Type t)
        {
            try
            {
                if (!System.IO.File.Exists(filename))
                {
                    //return default(t);
                    return null;
                }

                using (System.IO.StreamReader reader = new System.IO.StreamReader(filename))
                {
                    XmlSerializer xs = new XmlSerializer(t);
                    return xs.Deserialize(reader);
                }

            }
            catch (Exception ex)
            {
                //	return default(UnitConfig);
                //return default(T);
                return null;
            }
        }


        public static T Load<T>(string filename)
            where T : new()
        {
            try
            {
                if (!System.IO.File.Exists(filename))
                {
                    return default(T);
                }



                using (System.IO.StreamReader reader = new System.IO.StreamReader(filename))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    T config = (T)xs.Deserialize(reader);
                    return config;
                }

            }
            catch (Exception ex)
            {
                //	return default(UnitConfig);
                return default(T);
            }
        }



        public static void Save(object config, string filename)
        {
            FileInfo fi = new FileInfo(filename);

            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }

            try
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filename))
                {
                    XmlSerializer xs = new XmlSerializer(config.GetType());
                    xs.Serialize(writer, config);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
