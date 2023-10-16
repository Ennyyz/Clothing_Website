using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN_14.Models;
namespace DOAN_14.Controllers
{
    public class LoginUserController : Controller
    {
        DBFourteenEntities db = new DBFourteenEntities();
        // GET: LoginUser
        // Phương thức tạo view cho Login
        public ActionResult Index(int chon)
        {
            Session["chon"] = chon;
            return View();
        }
        // Xử lý tìm kiếm ID, password trong AdminUser và thông báo
        [HttpPost]
        public ActionResult LoginAcount(AdminUser _user, string chon)
        {
            var check = db.AdminUsers.Where(s => s.NameUser == _user.NameUser && s.PasswordUser ==_user.PasswordUser).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai Info";
                return View("Index");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["NameUser"] = _user.NameUser;
                Session["PasswodUser"] = _user.PasswordUser;
                Session["chon"] = chon;
                if (chon.ToString() == "1")
                    return RedirectToAction("Index", "Products");
                else if (chon.ToString() == "2")
                    return RedirectToAction("Index", "Categories");
                else if (chon.ToString() == "3")
                    return RedirectToAction("Index", "Customers");
                else if (chon.ToString() == "4")
                    return RedirectToAction("Index", "OrderProes");
                else if (chon.ToString() == "5")
                    return RedirectToAction("RegisterUser", "LoginUser");
                else
                    return RedirectToAction("ProductList", "Products");
            }
        }
        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }
         [HttpPost]
         public ActionResult RegisterUser(AdminUser _user)
         {
            if (ModelState.IsValid)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                db.AdminUsers.Add(_user);
                db.SaveChanges();
            }
            else
            {
                ViewBag.ErrorRegister = "ID này đã có rồi !!!";
            }
         return View();
         }
        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("ProductList", "Products");
        }

    }

}
