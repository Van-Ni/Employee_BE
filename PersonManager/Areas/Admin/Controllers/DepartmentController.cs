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
    public class DepartmentController : Controller
    {
        private HRMEntities db = new HRMEntities();

        // GET: Admin/Department
        public ActionResult Index()
        {
            var departments = db.departments
         .Select(e => new DepartmentViewModel
         {
             Id = e.id,
             Name = e.name,
             Description = e.description
         })
         .ToList();

            return View(departments);
        }

        // GET: Admin/Department/Details/5
        public ActionResult Details(int? id)
        {
            var department = db.departments
                .Where(e => e.id == id)
                .Select(e => new DepartmentViewModel
                {
                    Id = e.id,
                    Name = e.name,
                    Description = e.description
                })
                .SingleOrDefault();

            if (department == null)
            {
                return HttpNotFound();
            }

            return View(department);
        }

        // GET: Admin/Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(department department)
        {
            if (ModelState.IsValid)
            {
                db.departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(department);
            }
        }

        // GET: Admin/Department/Edit/5
        public ActionResult Edit(int? id)
        {
            var department = db.departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }

            var departmentViewModel = new DepartmentViewModel
            {
                Id = department.id,
                Name = department.name,
                Description = department.description
            };

            return View(departmentViewModel);
        }

        // POST: Admin/Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, department department)
        {
            var departmentToUpdate = db.departments.Find(id);
            if (departmentToUpdate == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                departmentToUpdate.name = department.name;
                departmentToUpdate.description = department.description;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(department);
            }
        }

        public ActionResult Delete(int? id)
        {
            var departmentToDelete = db.departments.Find(id);
            if (departmentToDelete == null)
            {
                return HttpNotFound();
            }

            db.departments.Remove(departmentToDelete);
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
