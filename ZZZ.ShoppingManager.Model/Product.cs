using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZZZ.ShoppingManager.Model
{
   public  class Product
    {
       public int PID { get; set; }//商品ID
       public string PName { get; set; }//商品名字
       public decimal UnitPrice { get; set; }//单价
       public string ImageUrl { get; set; }//图片路径
       public string PDescription { get; set; }//商品描述
       public int CID { get; set; }//商品分类ID
       public string CategoryName { get; set; }//商品分类名

       //描述超过15个字符后面的字用省略号代替
       public string PDescriptionText { get {
            if(PDescription.Length>15)
            {
                return PDescription.Substring(0, 15) + "...";
            }
            else
            {
                return PDescription;
            }
       } }
    }
}
