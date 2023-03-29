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
        WedBanHangEntities objWedBanHangEntities = new WedBanHangEntities();
        public ActionResult Index()
        {
            var lstProduct = objWedBanHangEntities.Products.ToList();
            return View(lstProduct);
        }
        [HttpGet]
        public ActionResult Create()
        {
            Commom objcommom=new Commom();
            // lấy dữ liệu danh mục dưới Db
            var lstCat=objWedBanHangEntities.Categories.ToList();
            //Convert sang select list dang value,text
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objcommom.ToSelectList(dtCategory, "Id", "Name");
            
            //lấy dữ liêuj thương hiệu dưới Db
            var lstBrand=objWedBanHangEntities.Brands.ToList();
            DataTable dtBrand=converter.ToDataTable(lstBrand);
            //convert sang select list dang value,text
            ViewBag.ListBrand= objcommom.ToSelectList(dtBrand, "Id", "Name");
            return View();
        }


        [ValidateInput(false)]      
        
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                    }
                    objWedBanHangEntities.Products.Add(objProduct);
                    objWedBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
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

        [HttpPost]
        public ActionResult Edit(int id,Product objPro)
        {
            if (objPro.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objPro.ImageUpload.FileName);
                string extension = Path.GetExtension(objPro.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objPro.Avatar = fileName;
                objPro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
            }
            objWedBanHangEntities.Entry(objPro).State=EntityState.Modified;
            objWedBanHangEntities.SaveChanges();
            return View(objPro);
        }

    }
   
}