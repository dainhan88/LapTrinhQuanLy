﻿using System;
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
        Encrytion enc = new Encrytion () ;
        // GET: Account

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
                //ma hoa mat khau truoc khi luu va database
                acc.PassWord = enc.PasswordEncrytion(acc.PassWord);
                db.Accounts.Add(acc);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            return View(acc);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login( Account acc)
        {
            if (ModelState.IsValid)
            {
                string encrytionPass = enc.PasswordEncrytion(acc.PassWord);
                var model = db.Accounts.Where(m => m.UseName == acc.UseName && m.PassWord == encrytionPass).ToList().Count();
                //thong tin dang nhap chinh xac 
                if (model == 1)
                {
                    FormsAuthentication.SetAuthCookie(acc.UseName, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Thong tin dang nhap khong chinh xac");
                }
            }
            return View(acc);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}