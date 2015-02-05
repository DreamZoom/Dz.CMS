using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.CMS.Services
{
    public sealed class ServiceFactory
    {
        public static ServiceBase Create(string ServiceName)
        {
            foreach (System.Reflection.Assembly assembly in System.AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.GetName().Name == "Anonymously Hosted DynamicMethods Assembly") continue;

                Type serviceType = assembly.ExportedTypes.FirstOrDefault(m => m.Name == (ServiceName + "Service"));
                if (serviceType != null)
                {
                    return assembly.CreateInstance(serviceType.FullName) as ServiceBase;
                }
                
            }
            return null;
        }
    }
}
