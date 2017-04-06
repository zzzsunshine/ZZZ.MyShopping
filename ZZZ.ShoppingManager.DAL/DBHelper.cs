using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace ZZZ.ShoppingManager.DAL
{
    public static class DBHelper
    {
        //连接字符串
        static string connStr = "server=.;database=Shopping;uid=sa;pwd=zzz19970601";
        //连接对象
        static SqlConnection conn = new SqlConnection(connStr);
        /// <summary>
        /// 通过语句执行查询数据的操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">执行的SQL语句</param>
        /// <param name="fun"></param>
        /// <param name="p">参数</param>
        /// <returns></returns>
        public static List<T> Query<T>(string sql, Func<SqlDataReader, T> fun, params SqlParameter[] p)
        {
            List<T> result = new List<T>();
            try
            {//异常处理
                conn.Open();//打开数据连接
                SqlCommand cmd = new SqlCommand(sql, conn);//创建命令对象用于操作数据表
                cmd.Parameters.AddRange(p);
                using(SqlDataReader dr=cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        T emp = fun(dr);
                        result.Add(emp);
                    }
                }
            }
            finally
            {
                conn.Close();//关闭数据库连接
            }
            return result;
        }
        /// <summary>
        /// 通过语句执行修改数据的操作(增、删、改)
        /// </summary>
        /// <param name="sql">执行的SQL语句</param>
        /// <param name="p">参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] p)
        {
            int i = -1;
            try
            {//异常处理
                conn.Open();//打开数据库连接
                SqlCommand cmd = new SqlCommand(sql, conn);//创建命令对象用于操作数据表
                cmd.Parameters.AddRange(p);
                //执行sql命令，并返回执行成功的记录数
                i = cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
            return i;
        }
        /// <summary>
        /// 返回查询到的首行首列的值
        /// </summary>
        /// <param name="sql">执行的SQL语句</param>
        /// <param name="p">参数</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] p)
        {
            object result = null;
            try
            {//异常处理
                conn.Open();//打开数据库连接
                SqlCommand cmd = new SqlCommand(sql, conn);//创建命令对象用于操作数据表
                cmd.Parameters.AddRange(p);
                //执行sql命令，并返回执行成功的记录数
                result = cmd.ExecuteScalar();
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}
