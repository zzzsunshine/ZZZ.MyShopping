using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZZZ.ShoppingManager.UI.Models
{
    public class ShoppingModelBinder:DefaultModelBinder
    {
        public static readonly string key = "MyShopping";
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            MyShopping ms = controllerContext.HttpContext.Session[key] as MyShopping;
            if(ms==null)
            {
                ms = new MyShopping();
                controllerContext.HttpContext.Session[key] = ms;
            }
            return ms;
        }
    }
}