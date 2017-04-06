using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZZZ.ShoppingManager.BLL;
using ZZZ.ShoppingManager.Model;

namespace ZZZ.ShoppingManager.UI.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Logon(string returnUrl,string content)
        {
            if(string.IsNullOrEmpty(content))
            {
                 ViewData["isShow"]= "false";
            }
            else
            {
                ViewData["isShow"] = content;
            }
            TempData["url"] = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Logon(UserInfo user)
        {
            string url;
            if (TempData["url"] == null)
            {
                url = "/Product/ProductList";
            }
            else
            {
                url = TempData["url"] as string;
            }
            if (ModelState.IsValidField("UName") == false)
            {
                ModelState.AddModelError("UName", "账号必填");
            }
            if (ModelState.IsValidField("UPwd") == false)
            {
                ModelState.AddModelError("UPwd", "密码必填");
            }
            if (ModelState.IsValid)
            {
                int result = new UserBll().Login(user);
                user.UserID = new UserBll().SelectUserId(user);
                Session["UserID"] = user.UserID;
                string Roles = new UserBll().RolesByNameAndPwd(user);
                if (result>0)
                {
                    if (Roles == "Guess")
                    {
                        FormsAuthentication.SetAuthCookie(Roles, false);
                        return Redirect(url);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(Roles, false);
                        return RedirectToAction("ProductManager", "Product");
                    }
                }
                else
                {
                    string results = "true";
                    return RedirectToAction("LogOn", new { content = results });
                }
            }
            return View("Logon");
        }
        public ActionResult Regin(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                ViewData["isShow"] = "false";
            }
            else
            {
                ViewData["isShow"] = "true";
            }
            ViewBag.Roles = new UserBll().GetRoles();
            return View();
        }
        [HttpPost]
        public ActionResult Regin(UserInfo info)
        {
            if (ModelState.IsValidField("UName") == false)
            {
                ModelState.AddModelError("UName", "账号必填");
            }
            if (ModelState.IsValidField("UPwd") == false)
            {
                ModelState.AddModelError("UPwd", "密码必填");
            }
            if (ModelState.IsValidField("UPwdAgain") == false)
            {
                ModelState.AddModelError("UPwdAgain", "确认密码必填");
            }
            if (ModelState.IsValid)
            {
                int count = new UserBll().SearchByUName(info);
                if (count > 0)
                {
                    string result = "true";
                    return RedirectToAction("Regin", new { content=result});
                }
                else
                {
                    int result = new UserBll().Regin(info);
                    if (result > 0)
                    {
                        return View("Logon");
                    }

                }

            }
            return RedirectToAction("Regin");
        }
    }
}
