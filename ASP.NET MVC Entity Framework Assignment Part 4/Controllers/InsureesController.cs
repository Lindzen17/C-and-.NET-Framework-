using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsure.Models;

namespace CarInsure.Controllers
{
    public class InsureesController : Controller
    {
        private carinsureEntities db = new carinsureEntities();

        // GET: Insurees
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insurees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insurees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insurees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,FullCoverage,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                // Calculate and assign Quote
                insuree.Quote = Math.Round(CalculateQuote(insuree), (int)2);

                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }

        private decimal CalculateQuote(Insuree insuree)
        {
            decimal quote = 50m; // base
            DateTime today = DateTime.Today;
            int age = today.Year - insuree.DateOfBirth.Year;
            if (insuree.DateOfBirth > today.AddYears(-age)) age--;

            // Age-based additions
            if (age <= 18)
                quote += 100m;
            else if (age >= 19 && age <= 25)
                quote += 50m;
            else // age >= 26
                quote += 25m;

            // Car year Rules
            if (insuree.CarYear < 2000) quote += 25m;
            if (insuree.CarYear > 2015) quote += 25m;

            // Porsche rules (case-insensitive)
            if (!string.IsNullOrEmpty(insuree.CarMake) &&
                insuree.CarMake.Equals("Porsche", StringComparison.OrdinalIgnoreCase))
            {
                quote += 25m;
                if (!string.IsNullOrEmpty(insuree.CarModel) && 
                    insuree.CarModel.Equals("911 Carrera", StringComparison.OrdinalIgnoreCase))
                {
                    quote += 25m; // extra for 911 Carrera
                }
            }

            // Speeding tickets
            if (insuree.SpeedingTickets > 0)
                quote += 10m * insuree.SpeedingTickets;

            // DUI: add 25%
            if (insuree.DUI)
                quote *= 1.25m;

            // Full coverage: add 50%
            if (insuree.FullCoverage)
                quote *= 1.5m;

            return quote;
        }

        // GET: Insurees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insurees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,FullCoverage,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                var dbInsuree = db.Insurees.Find(insuree.Id);
                if (dbInsuree == null) return HttpNotFound();

                // update fields
                dbInsuree.FirstName = insuree.FirstName;
                dbInsuree.LastName = insuree.LastName;
                dbInsuree.EmailAddress = insuree.EmailAddress;
                dbInsuree.DateOfBirth = insuree.DateOfBirth;
                dbInsuree.CarYear = insuree.CarYear;
                dbInsuree.CarMake = insuree.CarMake;
                dbInsuree.CarModel = insuree.CarModel;
                dbInsuree.SpeedingTickets = insuree.SpeedingTickets;
                dbInsuree.DUI = insuree.DUI;
                dbInsuree.FullCoverage = insuree.FullCoverage;

                // recalc
                dbInsuree.Quote = Math.Round(CalculateQuote(dbInsuree), (int)2m);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insurees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insurees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
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

        // Admin view to see all insurees and their quotes
        [Authorize] // optional - remove if you don't have authentication set up yet
        public ActionResult Admin()
        {
            var list = db.Insurees.ToList();
            return View(list);
        }

    }
}
