using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WedsiteBanHang.Context;
 
using WedsiteBanHang.Models;

namespace WedsiteBanHang.Controllers
{
    public class HomeController : Controller
    {
        WedBanHangEntities objWedBanHangEntities = new WedBanHangEntities();
        public ActionResult Index()
        {
            HomeModel objHomeModel=new HomeModel();
            objHomeModel.ListCategory = objWedBanHangEntities.Categories.ToList();

            objHomeModel.ListProduct = objWedBanHangEntities.Products.ToList();
            return View(objHomeModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}