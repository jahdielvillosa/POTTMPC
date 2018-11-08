using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobsV1.Models;

namespace JobsV1.Controllers
{
    public class CarRateUnitPackagesController : Controller
    {
        private JobDBContainer db = new JobDBContainer();

        // GET: CarRateUnitPackages
        public ActionResult Index(int? sortby)
        {
            List<CarRateUnitPackage> carRateUnitPackages = new List<CarRateUnitPackage>();
            if (sortby != null)
            {
                if(sortby == 1)
                    carRateUnitPackages = db.CarRateUnitPackages.Include(c => c.CarRatePackage).Include(c => c.CarUnit).OrderBy(s=>s.CarUnitId).ToList();
                if (sortby == 2)
                    carRateUnitPackages = db.CarRateUnitPackages.Include(c => c.CarRatePackage).Include(c => c.CarUnit).OrderBy(s => s.CarRatePackageId).ToList();
            }
            else {
                carRateUnitPackages = db.CarRateUnitPackages.Include(c => c.CarRatePackage).Include(c => c.CarUnit).ToList();
            }
            return View(carRateUnitPackages);
        }

        // GET: CarRateUnitPackages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarRateUnitPackage carRateUnitPackage = db.CarRateUnitPackages.Find(id);
            if (carRateUnitPackage == null)
            {
                return HttpNotFound();
            }
            return View(carRateUnitPackage);
        }

        // GET: CarRateUnitPackages/Create
        public ActionResult Create()
        {
            ViewBag.CarRatePackageId = new SelectList(db.CarRatePackages, "Id", "Description");
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description");
            return View();
        }

        // POST: CarRateUnitPackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CarRatePackageId,CarUnitId,DailyRate,FuelLonghaul,FuelDaily")] CarRateUnitPackage carRateUnitPackage)
        {
            if (ModelState.IsValid)
            {
                db.CarRateUnitPackages.Add(carRateUnitPackage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarRatePackageId = new SelectList(db.CarRatePackages, "Id", "Description", carRateUnitPackage.CarRatePackageId);
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", carRateUnitPackage.CarUnitId);
            return View(carRateUnitPackage);
        }

        // GET: CarRateUnitPackages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarRateUnitPackage carRateUnitPackage = db.CarRateUnitPackages.Find(id);
            if (carRateUnitPackage == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarRatePackageId = new SelectList(db.CarRatePackages, "Id", "Description", carRateUnitPackage.CarRatePackageId);
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", carRateUnitPackage.CarUnitId);
            return View(carRateUnitPackage);
        }

        // POST: CarRateUnitPackages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CarRatePackageId,CarUnitId,DailyRate,FuelLonghaul,FuelDaily")] CarRateUnitPackage carRateUnitPackage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carRateUnitPackage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarRatePackageId = new SelectList(db.CarRatePackages, "Id", "Description", carRateUnitPackage.CarRatePackageId);
            ViewBag.CarUnitId = new SelectList(db.CarUnits, "Id", "Description", carRateUnitPackage.CarUnitId);
            return View(carRateUnitPackage);
        }

        // GET: CarRateUnitPackages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarRateUnitPackage carRateUnitPackage = db.CarRateUnitPackages.Find(id);
            if (carRateUnitPackage == null)
            {
                return HttpNotFound();
            }
            return View(carRateUnitPackage);
        }

        // POST: CarRateUnitPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarRateUnitPackage carRateUnitPackage = db.CarRateUnitPackages.Find(id);
            db.CarRateUnitPackages.Remove(carRateUnitPackage);
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
