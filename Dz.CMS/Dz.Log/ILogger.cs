using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.Log
{
    public interface ILogger
    {
         void Debug(object o);
         void Info(object o);
         void Error(object o);
         void Warn(object o);
    }
}
