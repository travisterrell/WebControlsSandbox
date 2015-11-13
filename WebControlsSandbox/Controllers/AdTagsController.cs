using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TypeAhead.Models;

namespace TypeAhead.Controllers
{
    public class AdTagsController : Controller
    {
        private WebControlsEntities db = new WebControlsEntities();

        // GET: AdTags
        public ActionResult Index()
        {
            var adTags = db.AdTags.Include(a => a.Client);
            return View(adTags.ToList());
        }

        // GET: AdTags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdTag adTag = db.AdTags.Find(id);
            if (adTag == null)
            {
                return HttpNotFound();
            }
            return View(adTag);
        }

        // GET: AdTags/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name");
            return View();
        }

        // POST: AdTags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ClientId")] AdTag adTag)
        {
            if (ModelState.IsValid)
            {
                db.AdTags.Add(adTag);
                var added = db.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);

                //foreach (var a in added)    
                //{
                //    var change = new AuditHistory 
                //    {
                //        UserId = 1,
                //        UserName = "Travis",
                //        EntityName = a.Entity.GetType().Name,
                //        Modifications = a.CurrentValues.PropertyNames.ToDictionary(pn => pn, pn => a.CurrentValues[pn])
                //    };
                    

                //    db.AuditHistories.Add(change);
                //}

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", adTag.ClientId);
            return View(adTag);
        }

        // GET: AdTags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdTag adTag = db.AdTags.Find(id);
            if (adTag == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", adTag.ClientId);
            return View(adTag);
        }

        // POST: AdTags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ClientId")] AdTag adTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", adTag.ClientId);
            return View(adTag);
        }

        // GET: AdTags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdTag adTag = db.AdTags.Find(id);
            if (adTag == null)
            {
                return HttpNotFound();
            }
            return View(adTag);
        }

        // POST: AdTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdTag adTag = db.AdTags.Find(id);
            db.AdTags.Remove(adTag);
            db.SaveChanges();
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
