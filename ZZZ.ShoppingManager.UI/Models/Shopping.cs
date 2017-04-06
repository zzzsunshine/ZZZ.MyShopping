using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZZZ.ShoppingManager.Model;

namespace ZZZ.ShoppingManager.UI.Models
{
    public class Shopping
    {
        public Product product { get; set; }
        public int Qty { get; set; }
        public decimal subTotal
        {
            get
            {
                return product.UnitPrice * Qty;
            }
        }
    }
}