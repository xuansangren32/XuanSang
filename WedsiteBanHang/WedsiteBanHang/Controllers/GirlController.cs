using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WedsiteBanHang.Context;

namespace WedsiteBanHang.Controllers
{
    public class GirlController : Controller
    {
        // GET: Girl
        WedBanHangEntities1 objWedBanHangEntities = new WedBanHangEntities1();





        // GET: dmsp
        public ActionResult Index(string currentFilter, string SearchString, int? page)
            {

                var lstProduct = new List<Product>();
                if (SearchString != null)
                {
                    page = 1;
                }
                else
                {
                    SearchString = currentFilter;
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    lstProduct = objWedBanHangEntities.Products.Where(n => n.Name.Contains(SearchString)).ToList();
                }
                else
                {
                    lstProduct = objWedBanHangEntities.Products.ToList();
                }
                ViewBag.CurrentFilter = SearchString;
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
                return View(lstProduct.ToPagedList(pageNumber, pageSize));
            }
            public ActionResult Listing(int Id)
            {
                var listProduct = objWedBanHangEntities.Products.Where(n => n.Category == 1).ToList();
                return View(listProduct);
            }
        }
    }
