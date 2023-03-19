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
        WedBanHangEntities objWedBanHangEntities = new WedBanHangEntities();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory=objWedBanHangEntities.Categories.ToList();
            return View(lstCategory);
        }
    }
}