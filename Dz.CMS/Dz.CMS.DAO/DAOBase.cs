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
    public class DAOBase : IDAO
    {
        DAOHelper Helper = new DAOHelper(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

        #region 基本方法
        public DbHelper DbHelper()
        {
            return new DbHelper(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        public ModelHelper ModelHelper()
        {
            return new ModelHelper();
        }
        #endregion


        public bool AddModel(ModelBase model)
        {
            string SQLString = ModelHelper().InsertSQL(model);
            if (DbHelper().ExecuteSql(SQLString, ModelHelper().getParams(model)) > 0)
            {
                return true;
            }
            return false;
        }

        public bool UpdateModel(ModelBase model)
        {
            string SQLString = ModelHelper().UpdateSQL(model);
            if (DbHelper().ExecuteSql(SQLString, ModelHelper().getParams(model)) > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteModel(ModelBase model)
        {
            string SQLString = ModelHelper().DeleteSQL(model);
            if (DbHelper().ExecuteSql(SQLString, ModelHelper().getParams(model)) > 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<ModelBase> GetList(ModelBase model, string where)
        {
            string SQLString = ModelHelper().SelectSQL(model);

            return ModelHelper().TableToModelList(DbHelper().Query(SQLString), model);
        }
    }
}
