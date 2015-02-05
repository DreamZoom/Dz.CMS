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
            foreach (System.Reflection.Assembly assembly in System.AppDomain.CurrentDomain.GetAssemblies())
            {
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
