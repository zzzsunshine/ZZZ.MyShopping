using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZZZ.ShoppingManager.Model;
using System.Data.SqlClient;

namespace ZZZ.ShoppingManager.DAL
{
    public class CartDal
    {
        public List<Cart> GetCartByUId(int UserId)
        {
            string sql = "select * from Cart c inner join Product p on c.PID=p.PID where UserID=@UId";
            SqlParameter p = new SqlParameter("@UId", UserId);
            return DBHelper.Query(sql, dr =>
            {
                Cart c = new Cart();
                c.ID = Convert.ToInt32(dr["ID"]);
                c.PID = Convert.ToInt32(dr["PID"]);
                c.UserID = Convert.ToInt32(dr["UserID"]);
                c.Quantity = Convert.ToInt32(dr["Quantity"]);
                c.PName = dr["PName"].ToString();
                c.ImageUrl = dr["ImageUrl"].ToString();
                c.UnitPrice = Convert.ToDecimal(dr["UnitPrice"]);
                c.PDescription = dr["PDescription"].ToString();

                //Cart c = new Cart
                //{
                //    ID = Convert.ToInt32(dr["ID"]),
                //    UserID = Convert.ToInt32(dr["UserID"]),
                //    PID = Convert.ToInt32(dr["PID"]),
                //    Quantity = Convert.ToInt32(dr["Quantity"])
                //};
                return c;
            }, p);
        }
        public int GetQuantityByUserIdAndPId(int UserId,int PId)
        {
            string sql = "select Quantity from Cart where UserId=@UserId and PId=@PId";
            SqlParameter[] p ={
                                  new SqlParameter("@UserId",UserId),
                                  new SqlParameter("PId",PId)
                              };
            return (int)DBHelper.ExecuteScalar(sql, p);
        }
        public int GetProductInCartByUIdAndPid(int UserId,int Pid)
        {
            string sql = "select count(*) from Cart where UserID=@Uid and Pid=@Pid";
            SqlParameter[] p ={
                                 new SqlParameter("@Pid",Pid),
                                 new SqlParameter("@Uid",UserId)
                             };
            return (int)DBHelper.ExecuteScalar(sql, p);
        }
        public int UpdateQty(int Quantity,int PID,int UserID)
        {
            string sql = "Update Cart set Quantity=@Quantity where PID=@PID and UserID=@UserID";
            SqlParameter[] p ={
                                 new SqlParameter("@Quantity",Quantity),
                                 new SqlParameter("@PID",PID),
                                 new SqlParameter("@UserID",UserID)
                             };
            return DBHelper.ExecuteNonQuery(sql, p);
        }
        public int InsertCart(int Quantity, int PID, int UserID)
        {
            string sql = "insert into Cart values(@PID,@UserID,@Quantity)";
            SqlParameter[] p ={
                                 new SqlParameter("@Quantity",Quantity),
                                 new SqlParameter("@PID",PID),
                                 new SqlParameter("@UserID",UserID)
                              };
            return DBHelper.ExecuteNonQuery(sql, p);
        }
        public int DeleteCart(int ID)
        {
            string sql = "delete Cart where ID=@id";
            SqlParameter p = new SqlParameter("@id", ID);
            return DBHelper.ExecuteNonQuery(sql, p);
        }
        public int DeleteCartByPIDAndUserId(int PID,int UserId)
        {
            string sql = "delete Cart where PID=@pid and UserID=@UserId";
            SqlParameter[] p ={
                                  new SqlParameter("@pid",PID),
                                  new SqlParameter("@UserId",UserId)
                              };
            return DBHelper.ExecuteNonQuery(sql, p);
        }
    }
}
