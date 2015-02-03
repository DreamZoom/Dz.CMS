using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Reflection;
namespace Dz.CMS.DAO
{
    public class DbHelper:IDisposable
    {

        
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        protected SqlConnection Conntection { get; set; }

        private DbHelper()
        {
        }

        public DbHelper(string ConnectionString)
        {
            Conntection = new SqlConnection(ConnectionString);
        }

        public void Dispose()
        {
            Conntection.Close();
        }

        #region  通用方法

        public DataTable Query(string SQLString)
        {
            //创建一个dataset
            DataSet ds = new DataSet();
            //打开数据库链接（如果未打开）
            if (Conntection.State == ConnectionState.Closed)
            {
                Conntection.Open();
            }
            //获取数据
            SqlDataAdapter command = new SqlDataAdapter(SQLString, Conntection);
            command.Fill(ds, "ds");
            if (ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public  DataTable Query(string SQLString, params SqlParameter[] cmdParms)
        {
         
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, Conntection, null, SQLString, cmdParms);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
                
            DataSet ds = new DataSet();
   
            da.Fill(ds, "ds");
            cmd.Parameters.Clear();
                   
            //
            if (ds.Tables.Count == 0) return null;
            return ds.Tables[0];
          
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString)
        {

            using (SqlCommand cmd = new SqlCommand(SQLString, Conntection))
            {
                if (Conntection.State == ConnectionState.Closed)
                {
                    Conntection.Open();
                }
                int rows = cmd.ExecuteNonQuery();
                return rows; 
            }
          
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public  int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
          
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    PrepareCommand(cmd, Conntection, null, SQLString, cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw e;
                }
            }
            
        }


        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {


                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        #endregion
    }
}
