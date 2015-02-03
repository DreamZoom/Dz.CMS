using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dz.Log
{
    public class Logger : ILogger
    {

        private static readonly object lockobject = new object();

        private static Logger _instance = null;
        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    //lock (_instance)
                    {
                        _instance = new Logger();
                    }
                }
                return _instance;
            }
        }

        public void Debug(object o)
        {

            this.Write(Log(o), "Debug");
        }

        public void Info(object o)
        {
            this.Write(Log(o), "Info");
        }

        public void Error(object o)
        {
            this.Write(Log(o), "Error");
        }

        public void Warn(object o)
        {
            this.Write(Log(o), "Warn");
        }

        public string Log(object o)
        {
            StringBuilder sb = new StringBuilder();
            if (o.GetType().IsClass)
            {
                var propertys = o.GetType().GetProperties();
                foreach (var p in propertys)
                {
                    var a = p.GetValue(o);
                    sb.Append(Log(o));
                }
            }
            else
            {
                sb.Append(o.GetType().Name+":"+o);
            }
            return sb.ToString();
        }

        public void Write(object o, string path)
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";
            path = System.Environment.CurrentDirectory + "/" + path + "/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.WriteAllText(path + fileName, o.ToString());
        }


    }
}
