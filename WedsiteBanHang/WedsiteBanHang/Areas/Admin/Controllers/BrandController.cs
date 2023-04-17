using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WedsiteBanHang.Context;
using static WedsiteBanHang.Commom;

namespace WedsiteBanHang.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        WedBanHangEntities1 objWedBanHangEntities = new WedBanHangEntities1();
        // GET: Admin/Brand
        public ActionResult Index(string SearchString, string currentFiler, int? page)
        {
            

            var lstBrand = new List<Brand>();
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
                lstBrand = objWedBanHangEntities.Brands.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstBrand = objWedBanHangEntities.Brands.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            lstBrand = lstBrand.OrderByDescending(n => n.Id).ToList();



            return View(lstBrand.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            //this.LoadData();

            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand objBra)
        {
            //this.LoadData();

            if (ModelState.IsValid)
            {
                try
                {

                    if (objBra.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objBra.ImageUpload.FileName);
                        string extension = Path.GetExtension(objBra.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objBra.Avatar = fileName;
                        objBra.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objWedBanHangEntities.Brands.Add(objBra);
                    objWedBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(objBra);
        }
        public ActionResult Details(int id)
        {
            var objBrand = objWedBanHangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = objWedBanHangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Delete(Brand objBran)
        {
            var objBrand = objWedBanHangEntities.Brands.Where(n => n.Id == objBran.Id).FirstOrDefault();
            objWedBanHangEntities.Brands.Remove(objBrand);
            objWedBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBrand = objWedBanHangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Brand objBra)
        {

            if (objBra.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBra.ImageUpload.FileName);
                string extension = Path.GetExtension(objBra.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objBra.Avatar = fileName;
                objBra.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objWedBanHangEntities.Entry(objBra).State = EntityState.Modified;
            objWedBanHangEntities.SaveChanges();
            return View(objBra);
        }
        //void LoadData()
        //{
        //    Commom objcommom = new Commom();
        //    // lấy dữ liệu danh mục dưới Db
        //    var lstCat = objWedBanHangEntities.Categories.ToList();
        //    //Convert sang select list dang value,text
        //    ListtoDataTableConverter converter = new ListtoDataTableConverter();
        //    DataTable dtCategory = converter.ToDataTable(lstCat);
        //    ViewBag.ListCategory = objcommom.ToSelectList(dtCategory, "Id", "Name");

        //    //lấy dữ liêuj thương hiệu dưới Db
        //    var lstBrand = objWedBanHangEntities.Brands.ToList();
        //    DataTable dtBrand = converter.ToDataTable(lstBrand);
        //    //convert sang select list dang value,text
        //    ViewBag.ListBrand = objcommom.ToSelectList(dtBrand, "Id", "Name");

        //    //loai san pham
        //    List<ProductType> lstProductType = new List<ProductType>();
        //    ProductType objProductType = new ProductType();
        //    //objProductType.Id = 01;
        //    objProductType.Name = "Giảm giá sốc";
        //    lstProductType.Add(objProductType);



        //    objProductType = new ProductType();
        //    //objProductType.Id = 02;
        //    objProductType.Name = "Đề xuất";
        //    lstProductType.Add(objProductType);

        //    DataTable dtProductType = converter.ToDataTable(lstProductType);
        //    //convert sang select list dang value,text
        //    ViewBag.ProductType = objcommom.ToSelectList(dtProductType, "Id", "Name");
        //}

    }
}