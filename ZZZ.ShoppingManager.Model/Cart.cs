using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZZZ.ShoppingManager.Model
{
    public class Cart
    {
        public int ID { get; set; }
        public int PID { get; set; }//商品ID
        public int UserID { get; set; }//用户ID
        public int Quantity { get; set; }//数量
        public string PName { get; set; }//用户账号
        public decimal UnitPrice { get; set; }//单价
        public string ImageUrl { get; set; }//图片路径
        public string PDescription { get; set; }//商品描述
        public decimal subTotal//小计
        {
            get
            {
                return UnitPrice * Quantity;
            }
        }
        //描述超过15个字符后面的字用省略号代替
        public string PDescriptionText { get { 
             if(PDescription.Length>15)
            {
                return PDescription.Substring(0,15)+"...";
            }
             else
             {
                 return PDescription;
             }
        } }
    }
}
