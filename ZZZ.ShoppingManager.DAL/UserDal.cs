using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZZZ.ShoppingManager.Model;

namespace ZZZ.ShoppingManager.DAL
{
    public class UserDal
    {
        public int Login(UserInfo info)
        {
            string sql = "select count(*) from UserInfo where UName=@UName and UPwd=@UPwd";
            SqlParameter[] p ={
                                  new SqlParameter("@UName",info.UName),
                                  new SqlParameter("@UPwd",info.UPwd)
                              };
            return (int)DBHelper.ExecuteScalar(sql, p);
        }
        public int SelectUserId(UserInfo info)
        {
            string sql = "select UserID from UserInfo where UName=@UName and UPwd=@UPwd";
            SqlParameter[] p ={
                                  new SqlParameter("@UName",info.UName),
                                  new SqlParameter("@UPwd",info.UPwd)
                              };
            object result = DBHelper.ExecuteScalar(sql, p);
            return result == null?0:(int)result ;
        }
        public string SelectUserName(string UserName)
        {
            string sql = "select r.UserName from UserInfo u inner join Roles r on u.UserID=r.UserID  where UName=@UName";
            SqlParameter[] p ={
                                  new SqlParameter("@UName",UserName)
                              };
            return (string)DBHelper.ExecuteScalar(sql, p);
        }
        public string RolesByNameAndPwd(UserInfo info)
        {
            string sql = "select r.RName from UserInfo u inner join Roles r on u.RId=r.RId where UName=@UName and UPwd=@UPwd";
            SqlParameter[] p ={
                                  new SqlParameter("@UName",info.UName),
                                  new SqlParameter("@UPwd",info.UPwd)
                              };
            return (string)DBHelper.ExecuteScalar(sql, p);
        }
        public int Regin(UserInfo info)
        {
            string sql="insert into UserInfo values(@UName,@UPwd,@RId)";
            SqlParameter[] p ={
                                  new SqlParameter("@UName",info.UName),
                                  new SqlParameter("@UPwd",info.UPwd),
                                  new SqlParameter("@RId",info.RId)
                              };
            return DBHelper.ExecuteNonQuery(sql, p);
        }
        public List<Roles> GetRoles()
        {
            string sql = "select * from Roles";
            return DBHelper.Query(sql, dr =>
            {
                Roles Role = new Roles
                {
                    RId = Convert.ToInt32(dr["RId"]),
                    RName = dr["RName"].ToString()
                };
                return Role;
            });
        }
        public int SearchByUName(UserInfo info)
        {
            string sql = "select count(*) from UserInfo where UName=@UName";
            SqlParameter p = new SqlParameter("@UName", info.UName);
            return (int)DBHelper.ExecuteScalar(sql, p);
        }
    }
}
