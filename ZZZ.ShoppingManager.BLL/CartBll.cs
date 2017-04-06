using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZZZ.ShoppingManager.DAL;
using ZZZ.ShoppingManager.Model;

namespace ZZZ.ShoppingManager.BLL
{
    public class CartBll
    {
        public List<Cart> GetCartByUId(int UserId)
        {
            return new CartDal().GetCartByUId(UserId);
        }
        public int UpdateQty(int Quantity, int PID, int UserID)
        {
            return new CartDal().UpdateQty(Quantity, PID, UserID);
        }
        public int InsertCart(int Quantity, int PID, int UserID)
        {
            return new CartDal().InsertCart(Quantity, PID, UserID);
        }
        public int GetProductInCartByUIdAndPid(int UserId, int Pid)
        {
            return new CartDal().GetProductInCartByUIdAndPid(UserId, Pid);
        }
        public int GetQuantityByUserIdAndPId(int UserId, int PId)
        {
            return new CartDal().GetQuantityByUserIdAndPId(UserId, PId);
        }
        public int DeleteCart(int ID)
        {
            return new CartDal().DeleteCart(ID);
        }
        public int DeleteCartByPIDAndUserId(int PID, int UserId)
        {
            return new CartDal().DeleteCartByPIDAndUserId(PID, UserId);
        }
    }
}
