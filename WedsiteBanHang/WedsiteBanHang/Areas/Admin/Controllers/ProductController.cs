using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WedsiteBanHang.Context;
using static WedsiteBanHang.Commom;

namespace WedsiteBanHang.Areas.Admin.Controllers
{

    public class ProductController : Controller
    {
        // GET: Admin/Product
        WedBanHangEntities1 objWedBanHangEntities = new WedBanHangEntities1();
        public ActionResult Index(string SearchString,string currentFiler,int? page)
        {
           var lstProduct=new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFiler;
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
            int pageSize = 4;
            int pageNumber=(page ?? 1);

            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();



            return View(lstProduct.ToPagedList(pageNumber,pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                   
                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objWedBanHangEntities.Products.Add(objProduct);
                    objWedBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objWedBanHangEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objWedBanHangEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = objWedBanHangEntities.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objWedBanHangEntities.Products.Remove(objProduct);
            objWedBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
       
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = objWedBanHangEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Product objPro)
        {

            if (objPro.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objPro.ImageUpload.FileName);
                string extension = Path.GetExtension(objPro.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objPro.Avatar = fileName;
                objPro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objWedBanHangEntities.Entry(objPro).State = EntityState.Modified;
            objWedBanHangEntities.SaveChanges();
            return View(objPro);
        }


        void LoadData()
        {
            Commom objcommom = new Commom();
            // lấy dữ liệu danh mục dưới Db
            var lstCat = objWedBanHangEntities.Categories.ToList();
            //Convert sang select list dang value,text
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objcommom.ToSelectList(dtCategory, "Id", "Name");

            //lấy dữ liêuj thương hiệu dưới Db
            var lstBrand = objWedBanHangEntities.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            //convert sang select list dang value,text
            ViewBag.ListBrand = objcommom.ToSelectList(dtBrand, "Id", "Name");

            //loai san pham
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            //objProductType.Id = 01;
            objProductType.Name = "Giảm giá sốc";
            lstProductType.Add(objProductType);



            objProductType = new ProductType();
            //objProductType.Id = 02;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            //convert sang select list dang value,text
            ViewBag.ProductType = objcommom.ToSelectList(dtProductType,"Id", "Name");
        }

    }
}
    

   
