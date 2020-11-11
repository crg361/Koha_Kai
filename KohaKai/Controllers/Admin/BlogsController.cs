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
    public class BlogsController : Controller
    {
        public kohakaiEntities db= new kohakaiEntities();
        // GET: blogs
        public async Task<ActionResult> Index()
        {
             if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            return View(await db.blogs.ToListAsync());
        }

        // GET: blogs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            blog blog = await db.blogs.FindAsync(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: blogs/Create
        public ActionResult Create()
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            return View();
        }

        // POST: blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,author,date,title,body")] blog blog)
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            if (ModelState.IsValid)
            {
                db.blogs.Add(blog);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(blog);
        }

        // GET: blogs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            blog blog = await db.blogs.FindAsync(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,author,date,title,body")] blog blog)
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: blogs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            blog blog = await db.blogs.FindAsync(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            blog blog = await db.blogs.FindAsync(id);
            db.blogs.Remove(blog);
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
    }
}
