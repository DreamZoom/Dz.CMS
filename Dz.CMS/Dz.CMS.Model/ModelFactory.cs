using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.CMS.Model
{
    public sealed class ModelFactory
    {
        public static ModelBase Create(string ModelName)
        {
            List<string> ls = new List<string>();
            foreach (System.Reflection.Assembly assembly in System.AppDomain.CurrentDomain.GetAssemblies())
            {

                if (assembly.GetName().Name == "Anonymously Hosted DynamicMethods Assembly") continue;
                
                Type serviceType = assembly.ExportedTypes.FirstOrDefault(m => m.Name == (ModelName));
                if (serviceType != null)
                {
                    return assembly.CreateInstance(serviceType.FullName) as ModelBase;
                }

            }
            return null;
        }
    }
}
