using CACHIA_MIGUEL_EP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CACHIA_MIGUEL_EP.Controllers
{
    public class CategorieController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult index()
        {
          

            return View(db.Category.ToList());
        }
        public ActionResult Create()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName")] Category Category)
        {
            if (ModelState.IsValid)
            { 
                db.Category.Add(Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Category);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category Category = db.Category.Find(id);
            if (Category == null)
            {
                return HttpNotFound();
            }
            return View(Category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName")] Category Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Category);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category locality = db.Category.Find(id);
            if (locality == null)
            {
                return HttpNotFound();
            }
            return View(locality);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category locality = db.Category.Find(id);
            db.Category.Remove(locality);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}