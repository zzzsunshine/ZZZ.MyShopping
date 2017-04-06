using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZZZ.ShoppingManager.Model;

namespace ZZZ.ShoppingManager.UI.Models
{
    public class MyShopping
    {
        List<Shopping> Products = new List<Shopping>();
        public void AddCart(Shopping sp)
        {
            Shopping shopping = IsCart(sp);
            if(shopping==null)
            {
                Products.Add(sp);
            }
            else
            {
                shopping.Qty++;
            }

        }
        public Shopping IsCart(Shopping sp)
        {
            return Products.Find(p => p.product.PID == sp.product.PID);
        }
    }
}