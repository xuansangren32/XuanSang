using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        [HttpGet]
     
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = objWedBanHangEntities.Users.FirstOrDefault(s => s.Email==_user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objWedBanHangEntities.Configuration.ValidateOnSaveEnabled = false;
                    objWedBanHangEntities.Users.Add(_user);
                    objWedBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exitsts";
                    return View();
                }
            }
            return View("Index");
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
           
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] fromData = Encoding.UTF8.GetBytes(str);
                byte[] targetData = md5.ComputeHash(fromData);
                string byte2String = null;

                for (int i = 0; i < targetData.Length; i++)
                {
                    byte2String += targetData[i].ToString("x2");

                }
                return byte2String;
            }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = objWedBanHangEntities.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
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