using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WedsiteBanHang.Context;

namespace WedsiteBanHang.Controllers
{
    public class DetailProductController : Controller
    {
        WedBanHangEntities1 objWedBanHangEntities = new WedBanHangEntities1();
        // GET: DetailProduct
        public ActionResult DetailProduct(int Id)
        {
            Product objProduct = objWedBanHangEntities.Products.Where(n => n.Id== Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}