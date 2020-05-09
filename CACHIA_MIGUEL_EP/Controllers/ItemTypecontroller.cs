using CACHIA_MIGUEL_EP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using Dropbox.Api;
using Dropbox.Api.Files;

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
        [Authorize()]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName");
            return View();
        }



        // POST: Properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        static String ApplicationName = "Miguel_EP";
        [Authorize()]
        [HttpPost]
        [ValidateAntiForgeryToken]

        
        public ActionResult Create([Bind(Include = "Id,CategoryId,Name, Price")] ItemType itemType, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image == null)
                {
                    ModelState.AddModelError("Image", "Image can not be empty");
                    ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", itemType.CategoryId);
                    return View(itemType);
                }
                else
                    if (!db.ItemTypes.Any(i => i.Name == itemType.Name))
                {
                    string accessToken = "UlztezbHQjAAAAAAAAAAQ5muZHMmiyUleTcvYYB6Neh4j69mGy7VXiMe3aewTJ3x";
                    using (DropboxClient client = new DropboxClient(accessToken, new DropboxClientConfig(ApplicationName)))
                    {



                        string[] spitInputFileName = Image.FileName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                        string fileNameAndExtension = spitInputFileName[spitInputFileName.Length - 1];



                        string[] fileNameAndExtensionSplit = fileNameAndExtension.Split('.');
                        string originalFileName = fileNameAndExtensionSplit[0];
                        string originalExtension = fileNameAndExtensionSplit[1];



                        string fileName = @"/Images/" + originalFileName + Guid.NewGuid().ToString().Replace("-", "") + "." + originalExtension;



                        var updated = client.Files.UploadAsync(
                            fileName,
                            mode: WriteMode.Overwrite.Overwrite.Instance,
                            body: Image.InputStream).Result;



                        var result = client.Sharing.CreateSharedLinkWithSettingsAsync(fileName).Result;
                       
                        itemType.Image = result.Url.Replace("?dl=0", "?raw=1");
                    }

                    db.ItemTypes.Add(itemType);
                    db.SaveChanges();
                    return RedirectToAction("Index");




                }
            }



            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", itemType.CategoryId);
            return View(itemType);

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