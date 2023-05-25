using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonManager.Areas.Admin.Models;
using PersonManager.Models;

namespace PersonManager.Areas.Admin.Controllers
{
    public class HolidayController : Controller
    {
        private HRMEntities db = new HRMEntities();

        // GET: Admin/Holiday
        public ActionResult Index()
        {
            var holidays = db.holidays
         .Select(e => new HolidayViewModel
         {
             Id = e.id,
             Holiday_date = e.holiday_date,
             Description = e.description
         })
         .ToList();
            return View(holidays);
        }

        // GET: Admin/Holiday/Details/5
        public ActionResult Details(int? id)
        {
            var holiday = db.holidays
                .Where(e => e.id == id)
                .Select(e => new HolidayViewModel
                {
                    Id = e.id,
                    Holiday_date = e.holiday_date,
                    Description = e.description
                })
                .SingleOrDefault();

            if (holiday == null)
            {
                return HttpNotFound();
            }

            return View(holiday);
        }

        // GET: Admin/Holiday/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Holiday/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(holiday holiday)
        {
            if (ModelState.IsValid)
            {
                db.holidays.Add(holiday);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(holiday);
            }
        }

        // GET: Admin/Holiday/Edit/5
        public ActionResult Edit(int? id)
        {
            var holiday = db.holidays.Find(id);
            if (holiday == null)
            {
                return HttpNotFound();
            }

            var holidayViewModel = new HolidayViewModel
            {
                Id = holiday.id,
                Holiday_date = holiday.holiday_date,
                Description = holiday.description
            };

            return View(holidayViewModel);
        }

        // POST: Admin/Holiday/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, holiday holiday)
        {
            var holidayToUpdate = db.holidays.Find(id);
            if (holidayToUpdate == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                holidayToUpdate.holiday_date = holiday.holiday_date;
                holidayToUpdate.description = holiday.description;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(holiday);
            }
        }

        // GET: Admin/Holiday/Delete/5
        public ActionResult Delete(int? id)
        {
            var holidayToDelete = db.holidays.Find(id);
            if (holidayToDelete == null)
            {
                return HttpNotFound();
            }

            db.holidays.Remove(holidayToDelete);
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
