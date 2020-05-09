using CACHIA_MIGUEL_EP.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace CACHIA_MIGUEL_EP.Controllers
{
    public class ItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly UserManager<ApplicationUser> um;
        [Authorize()]
        public ActionResult index()
        {

            if (User.IsInRole("Admin"))
            {
                var data = db.items.Include(p => p.ItemType).Include(q => q.Quality);
                return View(data.ToList());
            }
            else if (User.IsInRole("RegisteredUser"))
            {
                var loged = (User.Identity.GetUserId());
                // var data = from c in db.items join where c.owner == loged select c;
                var data = db.items.Include(p => p.ItemType).Include(q => q.Quality).Where(x => x.owner.Contains(loged)).ToList();
                return View(data.ToList());
            }
            else
            {
                return HttpNotFound();
            }
        }
        [Authorize()]
        public ActionResult Create()
        {
           
            Populatelist();

            return View();
        }
        [Authorize()]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,ItemTypeID, Quantity, Qualityid, Price,owner")] Item Item)
        {
            if (ModelState.IsValid)
            {

                Item.owner=  User.Identity.GetUserId();
                db.items.Add(Item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Populatelist();
          
            return View(Item);
        }
        [Authorize()]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item Item = db.items.Find(id);
            if (Item == null)
            {
                return HttpNotFound();
            }
            Populatelist();
            return View(Item);
        }
        [Authorize()]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,ItemTypeID, Quantity, Qualityid, Price")] Item Item)
        {
            Item Itemm = db.items.Find(Item.ItemId);
            Item.owner = Itemm.owner;
            if (ModelState.IsValid)
            {
               db.Entry(Item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        
            Populatelist();
            return View(Item);
        }
     
        [Authorize()]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item Item = db.items.Find(id);
            if (Item == null)
            {
                return HttpNotFound();
            }

            return View(Item);
        }
          [Authorize()]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item Item = db.items.Find(id);
            db.items.Remove(Item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public void Populatelist()
        {
            var Itemtypeque = from c in db.ItemTypes orderby c.Name select c;
            ViewBag.ItemTypeID = new SelectList(Itemtypeque, "ItemTypeID", "Name");
            var Qualitypeque = from c in db.Qualitys orderby c.QualityName select c;
            ViewBag.Qualityid = new SelectList(Qualitypeque, "QualityId", "QualityName");

        }
      
    }
}