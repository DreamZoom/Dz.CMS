using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Reflection;
using Dz.CMS.Model;


namespace Dz.CMS.DAO
{
    public class ModelHelper
    {
        public string InsertSQL(ModelBase o)
        {
            StringBuilder SQLBulider = new StringBuilder();

            ////反射获取属性列表
            var propertys = o.GetType().GetProperties();

            List<string> filedList = new List<string>();
            List<string> valueList = new List<string>();
            foreach(var p in propertys){
                if (IsFiliter(o, p)) continue;
                filedList.Add(string.Format("[{0}]", p.Name));
                valueList.Add(string.Format("@{0}",p.Name));
            }
            ////bulid sql
            SQLBulider.AppendFormat(" INSERT INTO [{0}]({1}) VALUES({2}) ", o.GetType().Name, string.Join(",", filedList.ToArray()), string.Join(",", valueList.ToArray()));
            return SQLBulider.ToString();
        }

        public string UpdateSQL(ModelBase o)
        {
            StringBuilder SQLBulider = new StringBuilder();

            ////反射获取属性列表
            var propertys = o.GetType().GetProperties();

            List<string> filedList = new List<string>();
            foreach (var p in propertys)
            {
                if (IsFiliter(o, p)) continue;
                filedList.Add(string.Format("[{0}]=@{0}", p.Name));
            }
            ////bulid update sql
            SQLBulider.AppendFormat(" UPDATE [{0}] SET {1} WHERE [{2}]=@{2}", o.GetType().Name, string.Join(",", filedList.ToArray()),o.getPrimaryKey());
            return SQLBulider.ToString();
        }

        public string DeleteSQL(ModelBase o)
        {
            StringBuilder SQLBulider = new StringBuilder();

            ////反射获取属性列表
            var propertys = o.GetType().GetProperties();
            ////bulid delete sql
            SQLBulider.AppendFormat(" DELETE FROM [{0}] WHERE [{1}]=@{1}", o.GetType().Name, o.getPrimaryKey());
            return SQLBulider.ToString();
        }

        public bool IsFiliter(ModelBase model, PropertyInfo property)
        {
            if (property.Name == model.getIdentity())
            {
                return true;
            }
            return false;
        }

        public string SelectSQL(ModelBase template)
        {
            StringBuilder SQLBulider = new StringBuilder();

            ////反射获取属性列表
            var propertys = template.GetType().GetProperties();
            ////bulid delete sql
            SQLBulider.AppendFormat(" SELECT * FROM [{0}] ", template.GetType().Name);
            return SQLBulider.ToString();
        }

        public string SelectSQL(ModelBase template, int top)
        {
            StringBuilder SQLBulider = new StringBuilder();

            ////反射获取属性列表
            var propertys = template.GetType().GetProperties();
            ////bulid delete sql
            SQLBulider.AppendFormat(" SELECT TOP {1} * FROM [{0}] ", template.GetType().Name,top);
            return SQLBulider.ToString();
        }

        public SqlParameter[] getParams(ModelBase o)
        {
            var propertys = o.GetType().GetProperties();
            SqlParameter[] parms = new SqlParameter[propertys.Length];
            int i = 0;
            foreach (var p in propertys)
            {
                parms[i] = new SqlParameter(p.Name,p.GetValue(o));
                i++;
            }
            return parms;
        }

        public ModelBase RowToModel(DataRow row,ModelBase template)
        {
            var propertys = template.GetType().GetProperties();
            ModelBase m = template.Clone() as ModelBase ;
            foreach (var p in propertys)
            {
                
                if (row[p.Name] != null)
                {
                    p.SetValue(m, Convert.ChangeType(row[p.Name].ToString(), p.PropertyType));
                }
            }

            return m;

        }


        public IEnumerable<ModelBase> TableToModelList(DataTable table, ModelBase template)
        {
            foreach( DataRow dr in table.Rows ){
                yield return RowToModel(dr,template);
            }
        }

    }
}
