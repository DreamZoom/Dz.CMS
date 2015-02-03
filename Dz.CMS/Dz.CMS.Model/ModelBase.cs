using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;


namespace Dz.CMS.Model
{
    public class ModelBase : IModel,ICloneable
    {
        /// <summary>
        /// 获取模型主键
        /// </summary>
        /// <returns></returns>
        public virtual string getPrimaryKey()
        {
            return "ID";
        }

        /// <summary>
        /// 获取模型标示键
        /// </summary>
        /// <returns></returns>
        public virtual string getIdentity()
        {
            return "ID";
        }


        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

       

        
    }
}
