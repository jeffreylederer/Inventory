using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inventory.DAL;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class BowlSizesController : Controller
    {
        private BowlContext db = new BowlContext();

        // GET: BowlSizes
        public ActionResult Index()
        {
            return View(db.Bowlsizes.ToList());
        }

        // GET: BowlSizes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BowlSize bowlSize = db.Bowlsizes.Find(id);
            if (bowlSize == null)
            {
                return HttpNotFound();
            }
            return View(bowlSize);
        }

        // GET: BowlSizes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BowlSizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Size")] BowlSize bowlSize)
        {
            if (ModelState.IsValid)
            {
                db.Bowlsizes.Add(bowlSize);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bowlSize);
        }

        // GET: BowlSizes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BowlSize bowlSize = db.Bowlsizes.Find(id);
            if (bowlSize == null)
            {
                return HttpNotFound();
            }
            return View(bowlSize);
        }

        // POST: BowlSizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Size")] BowlSize bowlSize)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bowlSize).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bowlSize);
        }

        // GET: BowlSizes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BowlSize bowlSize = db.Bowlsizes.Find(id);
            if (bowlSize == null)
            {
                return HttpNotFound();
            }
            return View(bowlSize);
        }

        // POST: BowlSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BowlSize bowlSize = db.Bowlsizes.Find(id);
            db.Bowlsizes.Remove(bowlSize);
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
