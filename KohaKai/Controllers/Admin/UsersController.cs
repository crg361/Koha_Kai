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
    public class UsersController : Controller
    {
        public kohakaiEntities db= new kohakaiEntities();
        // GET: users
        public async Task<ActionResult> Index()
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            return View(await db.users.ToListAsync());
        }

        // GET: users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = await db.users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: users/Create
        public ActionResult Create()
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name,pw")] user user)
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = await db.users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,pw")] user user)
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = await db.users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (MyModel.AdminName == "") { Response.Redirect("Login"); }
            user user = await db.users.FindAsync(id);
            db.users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //--->
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
