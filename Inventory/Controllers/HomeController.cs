using System;
using Inventory.DAL;
using Inventory.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using System.Collections.Generic;

namespace Inventory.Controllers
{
    public class HomeController : Controller
    {
        private BowlContext db = new BowlContext();

        // GET: Home
        public ActionResult Index(string sortOrder, string currentFilter, string search, int? page)
        {
            //var bowls = db.Bowls.Include(b => b.Bias).Include(b => b.BowlSize).Include(b => b.Weight).ToList();
           

            ViewBag.CurrentSort = sortOrder;

            ViewBag.SizeSortParm = String.IsNullOrEmpty(sortOrder) ? "size_desc" : "";
            ViewBag.BiasSortParm = sortOrder == "Bias" ? "bias_desc" : "Bias";
            ViewBag.WeightSortParm = sortOrder == "Weight" ? "weight_desc" : "Weight";

            var bowls = db.Bowls.ToList();

            
            if (!string.IsNullOrWhiteSpace(search) || !string.IsNullOrWhiteSpace(sortOrder))
            {
                page = 1;
                
            }
            string selected = "";
            if (search != null)
            {
                if (search != "")
                {
                    bowls = bowls.Where(x => x.BowlSizeId == int.Parse(search)).ToList();
                    selected = search;
                }
                else
                {
                    selected = "";

                }
            }
            else if (!string.IsNullOrWhiteSpace(ViewBag.CurrentFilter))
            {
                bowls = bowls.Where(x => x.BowlSizeId == int.Parse(ViewBag.CurrentFilter.ToString())).ToList();
                selected = ViewBag.CurrentFilter.ToString();
            }
            else if (!string.IsNullOrWhiteSpace(currentFilter))
            {
                bowls = bowls.Where(x => x.BowlSizeId == int.Parse(currentFilter)).ToList();
                selected = currentFilter;
            }
            ViewBag.CurrentFilter = selected;
            

            switch (sortOrder)
            {
                case "size_desc":
                    bowls.Sort((a, b) => b.BowlSizeId.CompareTo(a.BowlSizeId));
                    break;
                case "Bias":
                    bowls.Sort((a, b) => a.BiasId.CompareTo(b.BiasId));
                    break;
                case "bias_desc":
                    bowls.Sort((a, b) => b.BiasId.CompareTo(a.BiasId));
                    break;
                case "Weight":
                    bowls.Sort((a, b) => a.WeightId.CompareTo(b.WeightId));
                    break;
                case "weight_desc":
                    bowls.Sort((a, b) => b.WeightId.CompareTo(a.WeightId));
                    break;
                default:
                    bowls.Sort((a, b) => a.BowlSizeId.CompareTo(b.BowlSizeId));
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewBag.BowlSizeId = new SelectList(db.Bowlsizes, "Id", "Size", selected);
            

            return View(bowls.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Enlarged(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bowl bowl = db.Bowls.Find(id);
            if (bowl == null)
            {
                return HttpNotFound();
            }
            
            var viewModel = new BowlViewModel()
            {
                Picture = bowl.Picture ?? new byte[] {0},
                Id=bowl.Id
            };
            return View(viewModel);
        }

        public ActionResult FileUpload(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bowl bowl = db.Bowls.Find(id);
            if (bowl == null)
            {
                return HttpNotFound();
            }
            var gallery = new ImageGallery()
            {
                Id = id.Value
            };
            return View(gallery);
        }

        [HttpPost]
        public ActionResult FileUpload(ImageGallery IG)
        {
            // Apply Validation Here


            if (IG.File.ContentLength > (2 * 1024 * 1024))
            {
                ModelState.AddModelError("CustomError", "File size must be less than 2 MB");
                return View();
            }
            if (IG.File.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("CustomError", "File type allowed : jpeg");
                return View();
            }

            IG.FileName = IG.File.FileName;
            IG.ImageSize = IG.File.ContentLength;

            Bowl bowl = db.Bowls.Find(IG.Id);

            bowl.Picture = new byte[IG.File.ContentLength];
            IG.File.InputStream.Read(bowl.Picture, 0, IG.File.ContentLength);
            var last = IG.FileName.LastIndexOfAny(new char[] {'\\'});
            bowl.FileName = IG.FileName.Substring(last + 1);
            db.SaveChanges();
                
            return RedirectToAction("Index");
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bowl bowl = db.Bowls.Find(id);
            if (bowl == null)
            {
                return HttpNotFound();
            }
            return View(bowl);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            ViewBag.BiasId = new SelectList(db.Biases, "Id", "BiasSize");
            ViewBag.BowlSizeId = new SelectList(db.Bowlsizes, "Id", "Size");
            ViewBag.WeightId = new SelectList(db.Weights, "Id", "BowlWeight");
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BowlSizeId,BiasId,WeightId,InLocker,OwnerName,Comment")] Bowl bowl)
        {
            if (ModelState.IsValid)
            {
                db.Bowls.Add(bowl);
                db.SaveChanges();
                return RedirectToAction("FileUpload", new {id = bowl.Id});
            }

            ViewBag.BiasId = new SelectList(db.Biases, "Id", "BiasSize", bowl.BiasId);
            ViewBag.BowlSizeId = new SelectList(db.Bowlsizes, "Id", "Size", bowl.BowlSizeId);
            ViewBag.WeightId = new SelectList(db.Weights, "Id", "BowlWeight", bowl.WeightId);
            return View(bowl);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bowl bowl = db.Bowls.Find(id);
            if (bowl == null)
            {
                return HttpNotFound();
            }
            ViewBag.BiasId = new SelectList(db.Biases, "Id", "BiasSize", bowl.BiasId);
            ViewBag.BowlSizeId = new SelectList(db.Bowlsizes, "Id", "Size", bowl.BowlSizeId);
            ViewBag.WeightId = new SelectList(db.Weights, "Id", "BowlWeight", bowl.WeightId);
            return View(bowl);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Picture,FileName,BowlSizeId,BiasId,WeightId,InLocker,OwnerName,Comment")] Bowl bowl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bowl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BiasId = new SelectList(db.Biases, "Id", "BiasSize", bowl.BiasId);
            ViewBag.BowlSizeId = new SelectList(db.Bowlsizes, "Id", "Size", bowl.BowlSizeId);
            ViewBag.WeightId = new SelectList(db.Weights, "Id", "BowlWeight", bowl.WeightId);
            return View(bowl);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bowl bowl = db.Bowls.Find(id);
            if (bowl == null)
            {
                return HttpNotFound();
            }
            return View(bowl);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bowl bowl = db.Bowls.Find(id);
            db.Bowls.Remove(bowl);
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
