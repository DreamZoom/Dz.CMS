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


        #region 分页处理方法
        /// <summary>
        /// 获取分页SQL语句，排序字段需要构成唯一记录
        /// </summary>
        /// <param name="_recordCount">记录总数</param>
        /// <param name="_pageSize">每页记录数</param>
        /// <param name="_pageIndex">当前页数</param>
        /// <param name="_safeSql">SQL查询语句</param>
        /// <param name="_orderField">排序字段，多个则用“,”隔开</param>
        /// <returns>分页SQL语句</returns>
        public static string CreatePagingSql(int _recordCount, int _pageSize, int _pageIndex, string _safeSql, string _orderField)
        {
            //重新组合排序字段，防止有错误
            string[] arrStrOrders = _orderField.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sbOriginalOrder = new StringBuilder(); //原排序字段
            StringBuilder sbReverseOrder = new StringBuilder(); //与原排序字段相反，用于分页
            for (int i = 0; i < arrStrOrders.Length; i++)
            {
                arrStrOrders[i] = arrStrOrders[i].Trim();  //去除前后空格
                if (i != 0)
                {
                    sbOriginalOrder.Append(", ");
                    sbReverseOrder.Append(", ");
                }
                sbOriginalOrder.Append(arrStrOrders[i]);

                int index = arrStrOrders[i].IndexOf(" "); //判断是否有升降标识
                if (index > 0)
                {
                    //替换升降标识，分页所需
                    bool flag = arrStrOrders[i].IndexOf(" DESC", StringComparison.OrdinalIgnoreCase) != -1;
                    sbReverseOrder.AppendFormat("{0} {1}", arrStrOrders[i].Remove(index), flag ? "ASC" : "DESC");
                }
                else
                {
                    sbReverseOrder.AppendFormat("{0} DESC", arrStrOrders[i]);
                }
            }

            //计算总页数
            _pageSize = _pageSize == 0 ? _recordCount : _pageSize;
            int pageCount = (_recordCount + _pageSize - 1) / _pageSize;

            //检查当前页数
            if (_pageIndex < 1)
            {
                _pageIndex = 1;
            }


            StringBuilder sbSql = new StringBuilder();
            //第一页时，直接使用TOP n，而不进行分页查询
            if (_pageIndex == 1)
            {
                sbSql.AppendFormat(" SELECT TOP {0} * ", _pageSize);
                sbSql.AppendFormat(" FROM ({0}) AS T ", _safeSql);
                sbSql.AppendFormat(" ORDER BY {0} ", sbOriginalOrder.ToString());
            }
            //最后一页时，减少一个TOP
            else if (_pageIndex == pageCount)
            {
                sbSql.Append(" SELECT * FROM ");
                sbSql.Append(" ( ");
                sbSql.AppendFormat(" SELECT TOP {0} * ", _recordCount - _pageSize * (_pageIndex - 1));
                sbSql.AppendFormat(" FROM ({0}) AS T ", _safeSql);
                sbSql.AppendFormat(" ORDER BY {0} ", sbReverseOrder.ToString());
                sbSql.Append(" ) AS T ");
                sbSql.AppendFormat(" ORDER BY {0} ", sbOriginalOrder.ToString());
            }
            //前半页数时的分页
            else if (_pageIndex <= (pageCount / 2 + pageCount % 2) && _pageIndex > 0)
            {
                sbSql.Append(" SELECT * FROM ");
                sbSql.Append(" ( ");
                sbSql.AppendFormat(" SELECT TOP {0} * FROM ", _pageSize);
                sbSql.Append(" ( ");
                sbSql.AppendFormat(" SELECT TOP {0} * ", _pageSize * _pageIndex);
                sbSql.AppendFormat(" FROM ({0}) AS T ", _safeSql);
                sbSql.AppendFormat(" ORDER BY {0} ", sbOriginalOrder.ToString());
                sbSql.Append(" ) AS T ");
                sbSql.AppendFormat(" ORDER BY {0} ", sbReverseOrder.ToString());
                sbSql.Append(" ) AS T ");
                sbSql.AppendFormat(" ORDER BY {0} ", sbOriginalOrder.ToString());
            }
            //后半页数时的分页
            else if (_pageIndex > (pageCount / 2 + pageCount % 2) && _pageIndex <= pageCount)
            {
                sbSql.AppendFormat(" SELECT TOP {0} * FROM ", _pageSize);
                sbSql.Append(" ( ");
                sbSql.AppendFormat(" SELECT TOP {0} * ", ((_recordCount % _pageSize) + _pageSize * (pageCount - _pageIndex) ));
                sbSql.AppendFormat(" FROM ({0}) AS T ", _safeSql);
                sbSql.AppendFormat(" ORDER BY {0} ", sbReverseOrder.ToString());
                sbSql.Append(" ) AS T ");
                sbSql.AppendFormat(" ORDER BY {0} ", sbOriginalOrder.ToString());
            }
            //返回空数据
            else
            {
                sbSql.AppendFormat(" SELECT TOP {0} * ", 0);
                sbSql.AppendFormat(" FROM ({0}) AS T ", _safeSql);
                sbSql.AppendFormat(" ORDER BY {0} ", sbOriginalOrder.ToString());
            }
            return sbSql.ToString();
        }

        /// <summary>
        /// 获取记录总数SQL语句
        /// </summary>
        /// <param name="_n">限定记录数</param>
        /// <param name="_safeSql">SQL查询语句</param>
        /// <returns>记录总数SQL语句</returns>
        public static string CreateTopnSql(int _n, string _safeSql)
        {
            return string.Format(" SELECT TOP {0} * FROM ({1}) AS T ", _n, _safeSql);
        }

        /// <summary>
        /// 获取记录总数SQL语句
        /// </summary>
        /// <param name="_safeSql">SQL查询语句</param>
        /// <returns>记录总数SQL语句</returns>
        public static string CreateCountingSql(string _safeSql)
        {
            return string.Format(" SELECT COUNT(1) AS RecordCount FROM ({0}) AS T ", _safeSql);
        }
        #endregion


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
