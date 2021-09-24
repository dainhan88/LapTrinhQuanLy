using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LapTrinhQuanLy.Models;

namespace LapTrinhQuanLy.Controllers
{
    public class PeoNewController : Controller
    {
        LTQLDBContext db = new LTQLDBContext ();
        AutoGenerateKey Genkey = new AutoGenerateKey();
        // GET: PeoNew
        public ActionResult Index()
        {
            return View(db.People.ToList());
        }
        public ActionResult Create()

        {
            var PSID = "";
            var countPerSon = db.People.Count();
            if (countPerSon == 0)
            {
                PSID = "PS001";
            }
            else
            {
                //lay gia tri PSID moi nhat
                var PersonID = db.People.ToList().OrderByDescending(m => m.PersonID).FirstOrDefault().PersonID;
                PSID = Genkey.GenerateKey(PersonID);
            }
            ViewBag.PersonID = PSID;
            return View();
        }
        [HttpPost]
        public ActionResult Create (Person PS)
        {
            if (ModelState.IsValid)

            {
                db.People.Add(PS);
                db.SaveChanges();
                // Lay du lieu tu client gui len va luu vao database
                return RedirectToAction("Index");
            }

            return View(PS);
        }
    }
}