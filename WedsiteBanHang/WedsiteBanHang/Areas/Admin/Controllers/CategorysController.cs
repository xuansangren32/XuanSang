using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WedsiteBanHang.Context;

namespace WedsiteBanHang.Areas.Admin.Controllers
{
    public class CategorysController : Controller
    {
        // GET: Admin/Category
      
            WedBanHangEntities1 objWedBanHangEntities = new WedBanHangEntities1();
            public ActionResult Index(string SearchString, string currentFiler, int? page)
            {
                var lstCategorys = new List<Category>();
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
                lstCategorys = objWedBanHangEntities.Categories.Where(n => n.Name.Contains(SearchString)).ToList();
                }
                else
                {
                lstCategorys = objWedBanHangEntities.Categories.ToList();
                }
                ViewBag.CurrentFilter = SearchString;
                int pageSize = 4;
                int pageNumber = (page ?? 1);

                lstCategorys = lstCategorys.OrderByDescending(n => n.Id).ToList();



                return View(lstCategorys.ToPagedList(pageNumber, pageSize));
            }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category objCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (objCategory.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                        string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objCategory.Avatar = fileName;
                        objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objWedBanHangEntities.Categories.Add(objCategory);
                    objWedBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(objCategory);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objCategory = objWedBanHangEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = objWedBanHangEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Delete(Category objCate)
        {
            var objCategory = objWedBanHangEntities.Categories.Where(n => n.Id == objCate.Id).FirstOrDefault();
            objWedBanHangEntities.Categories.Remove(objCategory);
            objWedBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objCategory = objWedBanHangEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Category objCategory)
        {

            if (objCategory.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objCategory.Avatar = fileName;
                objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objWedBanHangEntities.Entry(objCategory).State = EntityState.Modified;
            objWedBanHangEntities.SaveChanges();
            return View(objCategory);
        }

    }
    }
