using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WedsiteBanHang.Context;

namespace WedsiteBanHang.Controllers
{
    public class CategoryController : Controller
    {
        WedBanHangEntities1 objWedBanHangEntities = new WedBanHangEntities1();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory=objWedBanHangEntities.Categories.ToList();
            return View(lstCategory);
        }

        public ActionResult ProductCategory(int Id)
        {
            var lstProduct = objWedBanHangEntities.Products.Where(n=>n.Category == Id).ToList();
            return View(lstProduct);
        }
    }
}