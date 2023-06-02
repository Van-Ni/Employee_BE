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
    public class SalaryController : Controller
    {
        private HRMEntities db = new HRMEntities();

        // GET: Admin/Salary
        public ActionResult Index()
        {
            var salarys = db.salarys
         .Select(e => new SalaryViewModel
         {
             Id = e.id,
             BasicSalary = e.basicSalary,
             Allowance = e.allowance,
             Coefficient = e.coefficient,
             HourlyRate = e.hourlyRate,
             Employee_id = e.employee_id
         })
         .ToList();

            return View(salarys);
        }

        // GET: Admin/Salary/Details/5
        public ActionResult Details(int? id)
        {
            var salary = db.salarys
                .Where(e => e.id == id)
                .Select(e => new SalaryViewModel
                {
                    Id = e.id,
                    BasicSalary = e.basicSalary,
                    Allowance = e.allowance,
                    Coefficient = e.coefficient,
                    HourlyRate = e.hourlyRate,
                    Employee_id = e.employee_id
                })
                .SingleOrDefault();

            if (salary == null)
            {
                return HttpNotFound();
            }

            return View(salary);
        }

        // GET: Admin/Salary/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Salary/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(salary salary)
        {
            if (ModelState.IsValid)
            {
                db.salarys.Add(salary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(salary);
            }
        }

        // GET: Admin/Salary/Edit/5
        public ActionResult Edit(int? id)
        {
            var salary = db.salarys.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }

            var salaryViewModel = new SalaryViewModel
            {
                Id = salary.id,
                BasicSalary = salary.basicSalary,
                Allowance = salary.allowance,
                Coefficient = salary.coefficient,
                HourlyRate = salary.hourlyRate,
                Employee_id = salary.employee_id
            };

            return View(salaryViewModel);
        }

        // POST: Admin/Salary/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, salary salary)
        {
            var salaryToUpdate = db.salarys.Find(id);
            if (salaryToUpdate == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                salaryToUpdate.basicSalary = salary.basicSalary;
                salaryToUpdate.allowance = salary.allowance;
                salaryToUpdate.coefficient = salary.coefficient;
                salaryToUpdate.hourlyRate = salary.hourlyRate;
                salaryToUpdate.employee_id = salary.employee_id;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(salary);
            }
        }

        // GET: Admin/Salary/Delete/5
        public ActionResult Delete(int? id)
        {
            var salaryToDelete = db.salarys.Find(id);
            if (salaryToDelete == null)
            {
                return HttpNotFound();
            }

            db.salarys.Remove(salaryToDelete);
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
