using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LapTrinhQuanLy.Models;
using System.Web.Security;

namespace LapTrinhQuanLy.Controllers
{
    public class AccountController : Controller
    {
        LTQLDBContext db = new LTQLDBContext();
        Encrytion enc = new Encrytion();
        StringProcess strPro = new StringProcess();


        public ActionResult Login(string returnUrl)

        {
            if (CheckSession() == 1)

            {

                return RedirectToAction("Index", "HomeAdmin", new { Area = "Admins" });
            }
            else if (CheckSession() == 2)

            {
                return RedirectToAction("Index", "HomeEmp", new { Area = "Employees" });

            }
          ViewBag.ReturnUrl = returnUrl;
           return View();

     }
        [HttpPost]
        [AllowAnonymous]

        public ActionResult Login(Account acc, string returnUrl)

        {
            try
            {
                if (!string.IsNullOrEmpty(acc.UseName) && !string.IsNullOrEmpty(acc.PassWord))
               {

                   using (var db = new LTQLDBContext())

                    {
                      var passToMD5 = strPro.GetMD5(acc.PassWord);
                        var account = db.Accounts.Where(m => m.UseName.Equals(acc.UseName) && m.PassWord.Equals(passToMD5)).Count();
                        if (account == 1)
                        {
                            FormsAuthentication.SetAuthCookie(acc.UseName, false);
                           Session["idUser"] = acc.UseName;
                            Session["roleUser"] = acc.RoleID;
                            return RedirectTolocal(returnUrl);
                        }

                        ModelState.AddModelError("", "Thông tin đăng nhập chưa chính xác");

                    }
                }
                ModelState.AddModelError("", "Username and password is required.");
            }

            catch
            {
            ModelState.AddModelError("", "Hệ thống đang được bảo trì, vui lòng liên hệ với quản trị viên");
            }
            return View(acc);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(Account acc)
        {
            if (ModelState.IsValid)
            {
                //Mã Hóa mật khẩu trước khi cho vào database
                acc.PassWord = enc.PasswordEncrytion(acc.PassWord);
                db.Accounts.Add(acc);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            return View(acc);
        }
        //[HttpGet]
        //public ActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        //public ActionResult Login(Account acc)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string encrytionPass = enc.PasswordEncrytion(acc.PassWord);
        //        var model = db.Accounts.Where(m => m.UseName == acc.UseName && m.PassWord == encrytionPass).ToList().Count();
        //        //thong tin dang nhap chinh xac 
        //        if (model == 1)
        //        {
        //            FormsAuthentication.SetAuthCookie(acc.UseName, true);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Thong tin dang nhap khong chinh xac");
        //        }
        //    }
        //    return View(acc);
        //}

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["iduser"] = null;
            return RedirectToAction("Login", "Account");
        }
        private ActionResult RedirectTolocal (string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || returnUrl== "/")
                    {
                if(CheckSession() ==1)
                {
                    return RedirectToAction("Index", "HomeAdmin", new { Area = "Admins" });
                }    
                else if (CheckSession()==2)
                {
                    return RedirectToAction("Index", "HomeEmp", new { Area = "Employees" });
                }    
                    }
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }    
            else
            {
                return RedirectToAction("Index", "Home");
            }    
        }

        private int CheckSession()
        {
            using (var db = new LTQLDBContext())
            {
                var user = HttpContext.Session["idUser"];
                if (user != null)
                {
                    var role = db.Accounts.Find(user.ToString()).RoleID;
                    if (role != null)
                    {
                        if (role.ToString() == "Admin")
                        {
                            return 1;
                        }
                        else if (role.ToString() == "nv")
                        {
                            return 2;
                        }
                    }
                }
            }
            return 0;
        }
    }
}