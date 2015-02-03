using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dz.Log;
namespace System
{
    public static class LogExtend
    {
        public static Logger Log(this object o)
        {
            return Logger.Instance;
        }
    }
}
