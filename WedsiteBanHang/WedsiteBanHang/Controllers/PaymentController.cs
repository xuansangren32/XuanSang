using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WedsiteBanHang.Context;
using WedsiteBanHang.Models;

namespace WedsiteBanHang.Controllers
{
    public class PaymentController : Controller
    {
        WedBanHangEntities1 objWedBanHangEntities = new WedBanHangEntities1();
        // GET: Payment
        public ActionResult Index()
        {

            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                Order objOrder = new Order();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objWedBanHangEntities.Orders.Add(objOrder);
                objWedBanHangEntities.SaveChanges();
                int intOrderId = objOrder.Id;

                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                foreach (var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.OrderId = intOrderId;
                    obj.Quantity = item.Quantity;
                    obj.Productid = item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objWedBanHangEntities.OrderDetails.AddRange(lstOrderDetail);
                objWedBanHangEntities.SaveChanges();
            }
            return View();
        }
    }
}