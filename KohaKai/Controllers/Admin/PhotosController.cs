using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KohaKai;
using KohaKai.Models;

namespace KohaKai.Controllers
{
    public class PhotosController : Controller
    {
        // GET: photos
        public kohakaiEntities db= new kohakaiEntities();
        public async Task<ActionResult> Index()
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            return View(await db.photos.ToListAsync());
        }

        // GET: photos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            photo photo = await db.photos.FindAsync(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: photos/Create
        public ActionResult Create()
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            return View();
        }

        // POST: photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,title,pic")] photo photo)
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var picFile = Request.Files[0];
                    var imgByte = new byte[picFile.ContentLength];
                    picFile.InputStream.Read(imgByte, 0, picFile.ContentLength);
                    photo.pic = imgByte;
                    db.photos.Add(photo);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(photo);
        }

        // GET: photos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            photo photo = await db.photos.FindAsync(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,title,pic")] photo photo)
        {
            if (ModelState.IsValid)
            {

                if (Request.Files.Count > 0)
                {
                    var picFile = Request.Files[0];
                    if (picFile.ContentLength > 0)
                    {
                        var imgByte = new byte[picFile.ContentLength];
                        picFile.InputStream.Read(imgByte, 0, picFile.ContentLength);
                        photo.pic = imgByte;
                        db.Entry(photo).State = EntityState.Modified;
                    }

                    else
                    {
                        db.Entry(photo).State = EntityState.Unchanged;
                    }
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(photo);
        }

        // GET: photos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            photo photo = await db.photos.FindAsync(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            photo photo = await db.photos.FindAsync(id);
            db.photos.Remove(photo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult GetImage(int id)
        {
            var imgByte = db.photos.Find(id).pic;
            return new FileContentResult(imgByte, "image/jpeg");
        }
    }
}
