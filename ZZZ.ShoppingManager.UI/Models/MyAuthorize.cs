using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZZZ.ShoppingManager.BLL;

namespace ZZZ.ShoppingManager.UI.Models
{
    public class MyAuthorize:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string UserName = httpContext.User.Identity.Name;
            string role = GetRole(UserName);
            if(Roles.Contains(role))
            {
                return true;
            }
            return base.AuthorizeCore(httpContext);
        }
        public string GetRole(string UserName)
        {
            string Role = new UserBll().SelectUserName(UserName);
            return Role;
            
        }
    }
}