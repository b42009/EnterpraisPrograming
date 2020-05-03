using CACHIA_MIGUEL_EP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace CACHIA_MIGUEL_EP.Controllers
{
    public class ItemTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult index()
        {
            var data = db.ItemTypes.Include(p=>p.Category);

            return View(data.ToList());
        }
        public ActionResult Create()
        {
            Populatelist();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Name,Image")] ItemType ItemType)
        {
            if (ModelState.IsValid)
            {
                db.ItemTypes.Add(ItemType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Populatelist();
            return View(ItemType);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemType ItemType = db.ItemTypes.Find(id);
            if (ItemType == null)
            {
                return HttpNotFound();
            }
            Populatelist();
            return View(ItemType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemTypeID,CategoryId,Name,Image")] ItemType ItemType)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(ItemType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
             Populatelist();
            return View(ItemType);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemType ItemType = db.ItemTypes.Find(id);
            if (ItemType == null)
            {
                return HttpNotFound();
            }
            return View(ItemType);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemType ItemType = db.ItemTypes.Find(id);
            db.ItemTypes.Remove(ItemType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public void Populatelist()
        {
            var categoryque = from c in db.Category orderby c.CategoryName select c;
            ViewBag.CategoryId = new SelectList(categoryque, "CategoryId", "CategoryName");
    }
    }
}