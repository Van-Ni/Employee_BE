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
    public class PositionController : Controller
    {
        private HRMEntities db = new HRMEntities();

        // GET: Admin/Position
        public ActionResult Index()
        {
            var positions = db.positions
         .Select(e => new PositionViewModel
         {
             Id = e.id,
             Name = e.name,
             Description = e.description
         })
         .ToList();

            return View(positions);
        }

        // GET: Admin/Position/Details/5
        public ActionResult Details(int? id)
        {
            var position = db.positions
                .Where(e => e.id == id)
                .Select(e => new PositionViewModel
                {
                    Id = e.id,
                    Name = e.name,
                    Description = e.description
                })
                .SingleOrDefault();

            if (position == null)
            {
                return HttpNotFound();
            }

            return View(position);
        }

        // GET: Admin/Position/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Position/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(position position)
        {
            if (ModelState.IsValid)
            {
                db.positions.Add(position);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(position);
            }
        }

        // GET: Admin/Position/Edit/5
        public ActionResult Edit(int? id)
        {
            var position = db.positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }

            var positionViewModel = new PositionViewModel
            {
                Id = position.id,
                Name = position.name,
                Description = position.description
            };

            return View(positionViewModel);
        }

        // POST: Admin/Position/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, position position)
        {
            var positionToUpdate = db.positions.Find(id);
            if (positionToUpdate == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                positionToUpdate.name = position.name;
                positionToUpdate.description = position.description;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(position);
            }
        }
        public ActionResult Delete(int? id)
        {
            var positionToDelete = db.positions.Find(id);
            if (positionToDelete == null)
            {
                return HttpNotFound();
            }

            db.positions.Remove(positionToDelete);
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
