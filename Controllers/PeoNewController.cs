using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
        //[AllowHtml]
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
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonID,PersonName")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}