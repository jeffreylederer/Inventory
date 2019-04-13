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
    public class BiasController : Controller
    {
        private BowlContext db = new BowlContext();

        // GET: Bias
        public ActionResult Index()
        {
            return View(db.Biases.ToList());
        }

        // GET: Bias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bias bias = db.Biases.Find(id);
            if (bias == null)
            {
                return HttpNotFound();
            }
            return View(bias);
        }

        // GET: Bias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BiasSize")] Bias bias)
        {
            if (ModelState.IsValid)
            {
                db.Biases.Add(bias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bias);
        }

        // GET: Bias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bias bias = db.Biases.Find(id);
            if (bias == null)
            {
                return HttpNotFound();
            }
            return View(bias);
        }

        // POST: Bias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BiasSize")] Bias bias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bias);
        }

        // GET: Bias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bias bias = db.Biases.Find(id);
            if (bias == null)
            {
                return HttpNotFound();
            }
            return View(bias);
        }

        // POST: Bias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bias bias = db.Biases.Find(id);
            db.Biases.Remove(bias);
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
