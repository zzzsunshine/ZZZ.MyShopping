using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZZZ.ShoppingManager.BLL;
using ZZZ.ShoppingManager.Model;
using ZZZ.ShoppingManager.UI.Models;

namespace ZZZ.ShoppingManager.UI.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        /// <summary>
        /// 显示所有商品
        /// </summary>
        /// <param name="CID">商品分类ID</param>
        /// <param name="PageNo">当前页</param>
        /// <param name="PageSize">每页记录数</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult ProductList(string Name, string info, int CID = 0, int PageNo = 1, int PageSize = 10)
        {
            Pagenation pagenation = null;
            List<Product> product = new ProductBll().GetProduct(PageNo, PageSize);
            #region oldmethod
            if (!string.IsNullOrEmpty(info))
            {
                Session["info"] = info;
                ViewBag.info = info;
                product = new ProductBll().GetProductBySearch(info, PageNo, PageSize);
            }
            if (string.IsNullOrEmpty(info) && Session["info"] != null)
            {
                info = Session["info"] as string;
                ViewBag.info = info;
                product = new ProductBll().GetProductBySearch(info, PageNo, PageSize);
            }
            if(CID==0)
            {
                if(string.IsNullOrEmpty(info))
                {
                    pagenation = new Pagenation(new ProductBll().GetRecondsCount(), PageSize);
                }
                else
                {
                    pagenation = new Pagenation(new ProductBll().GetRecondsCountByCIDAndPName(CID, info), PageSize);
                }
            }
            if (CID > 0)
            {
                ViewBag.info = "";

                Session["CID"] = CID;
                ViewBag.CID = CID;
                pagenation = new Pagenation(new ProductBll().GetRecondsCount(CID), PageSize);
                product = new ProductBll().GetProductByCID(CID, PageNo, PageSize);
            }
            else if (CID == -1)
            {
                ViewBag.info = "";

                Session["CID"] = CID;
                ViewBag.CID = CID;
                pagenation = new Pagenation(new ProductBll().GetRecondsCount(), PageSize);
                product = new ProductBll().GetProduct(PageNo, PageSize);
            }

            #endregion

            pagenation.PageNo = PageNo;

            PagenationText pagenationText = new PagenationText(pagenation);
            pagenationText.FirstAfter = 2;
            pagenationText.LastBefore = 2;
            pagenationText.CurrenntBefore = 2;
            pagenationText.CurrentAfter = 2;
            pagenationText.SkitText = "...";

            ViewBag.pagenationText = pagenationText;


            ViewBag.Category = new ProductBll().GetCategory();

            return View(product);
        }
        /// <summary>
        /// 管理员界面显示商品
        /// </summary>
        /// <param name="PageNo">当前页</param>
        /// <param name="PageSize">每页记录数</param>
        /// <returns></returns>
        public ActionResult ProductManager(int PageNo = 1, int PageSize = 10)
        {
            Pagenation pagenation = new Pagenation(new ProductBll().GetRecondsCount(), PageSize);
            pagenation.PageNo = PageNo;

            PagenationText pagenationText = new PagenationText(pagenation);
            pagenationText.FirstAfter = 2;
            pagenationText.LastBefore = 2;
            pagenationText.CurrenntBefore = 2;
            pagenationText.CurrentAfter = 2;
            pagenationText.SkitText = "...";

            ViewBag.pagenationText = pagenationText;
            ViewBag.Category = new ProductBll().GetCategory();
            List<Product> product = new ProductBll().GetProduct(PageNo, PageSize);
            return View(product);
        }
        /// <summary>
        /// 添加商品的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Insert()
        {
            ViewBag.Category = new ProductBll().GetCategory();
            return View();
        }
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="p">product类</param>
        /// <param name="ImageUrl">图片路径</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Insert(Product p, HttpPostedFileBase ImageUrl)
        {
            if (ModelState.IsValidField("PName") == false)
            {
                ModelState.AddModelError("PName", "商品名必填");
            }
            if (ModelState.IsValidField("UnitPrice") == false)
            {
                ModelState.AddModelError("UnitPrice", "商品单价必填");
            }
            if (ModelState.IsValidField("ImageUrl") == false)
            {
                ModelState.AddModelError("ImageUrl", "未选择文件");
            }
            if (ModelState.IsValid)
            {
                var fileName = Path.Combine(Request.MapPath("~/Image"), Path.GetFileName(ImageUrl.FileName));
                ImageUrl.SaveAs(fileName);
                p.ImageUrl = ImageUrl.FileName;
                int result = new ProductBll().InsertProduct(p);
                if (result > 0)
                {
                    return RedirectToAction("ProductManager");
                }
                else
                {
                    return RedirectToAction("Insert");
                }
            }
            return RedirectToAction("Insert");
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="p">产品类</param>
        /// <returns></returns>
        public ActionResult Delete(Product p)
        {
            int result = new ProductBll().DeleteProduct(p);
            return RedirectToAction("ProductManager");
        }
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult Edit(Product p)
        {

            Product product = null;

            List<Product> products = new ProductBll().GetProductByPID(p);
            product = new Product
            {
                PID = products[0].PID,
                ImageUrl = products[0].ImageUrl,
                CategoryName = products[0].CategoryName,
                CID = products[0].CID,
                PDescription = products[0].PDescription,
                PName = products[0].PName,
                UnitPrice = products[0].UnitPrice
            };
            ViewBag.Category = new ProductBll().GetCategory();


            return View(product);
        }
        /// <summary>
        /// 编辑后保存数据
        /// </summary>
        /// <param name="p">产品类</param>
        /// <param name="ImageUrl">图片路径</param>
        /// <returns></returns>
        public ActionResult Save(Product p, HttpPostedFileBase ImageUrl)
        {
            if (ModelState.IsValidField("PName") == false)
            {
                ModelState.AddModelError("PName", "商品名必填");
            }
            if (ModelState.IsValidField("UnitPrice") == false)
            {
                ModelState.AddModelError("UnitPrice", "商品单价必填");
            }
            if (ModelState.IsValid)
            {
                int resut = new ProductBll().UpdateProduct(p);
            }
            return RedirectToAction("ProductManager");

        }
        /// <summary>
        /// 添加商品到购物车
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult AddCart(Product p)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Logon", "Account");
            }
            int UserID = (int)Session["UserID"];
            List<Cart> carts = new CartBll().GetCartByUId(UserID);
            int result = new CartBll().GetProductInCartByUIdAndPid(UserID, p.PID);
            if (result > 0)
            {
                carts[0].Quantity++;
                new CartBll().UpdateQty(carts[0].Quantity, p.PID, UserID);
            }
            else
            {
                new CartBll().InsertCart(1, p.PID, UserID);
            }
            return RedirectToAction("ProductList");

        }
        /// <summary>
        /// 显示我的购物车
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowMyCart()
        {

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Logon", "Account");
            }
            int UserId = (int)Session["UserID"];
            List<Cart> Carts = new CartBll().GetCartByUId(UserId);
            return View(Carts);
        }
        /// <summary>
        /// 添加购物车里面商品的数量
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult AddQuantity(Product p)
        {
            int UserId = (int)Session["UserID"];
            Cart c = new Cart();
            c.Quantity = new CartBll().GetQuantityByUserIdAndPId(UserId, p.PID);
            int Quantity = c.Quantity + 1;
            new CartBll().UpdateQty(Quantity, p.PID, UserId);
            return RedirectToAction("ShowMyCart");
        }
        /// <summary>
        /// 减少购物车里面商品的数量
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult MinusQuantity(Product p)
        {
            int UserId = (int)Session["UserID"];
            Cart c = new Cart();
            c.Quantity = new CartBll().GetQuantityByUserIdAndPId(UserId, p.PID);
            int Quantity = c.Quantity - 1;
            if (Quantity <= 0)
            {
                new CartBll().DeleteCartByPIDAndUserId(p.PID, UserId);
            }
            else
            {
                new CartBll().UpdateQty(Quantity, p.PID, UserId);
            }
            return RedirectToAction("ShowMyCart");
        }
        /// <summary>
        /// 删除购物车里面的商品
        /// </summary>
        /// <param name="ID">商品ID</param>
        /// <returns></returns>
        public ActionResult DeleteCart(int ID)
        {
            new CartBll().DeleteCart(ID);
            return RedirectToAction("ShowMyCart");
        }
    }
}
