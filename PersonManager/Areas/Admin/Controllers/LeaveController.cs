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
    public class LeaveController : Controller
    {
        private HRMEntities db = new HRMEntities();

        // GET: Admin/Leave
        public ActionResult Index()
        {
            var leaves = db.leaves
         .Select(e => new LeaveViewModel
         {
             Id = e.id,
             Leave_date = e.leave_date,
             Reason = e.reason,
             Approved = e.approved,
             Leave_type = e.leave_type,
             Employee_id = e.employee_id
         })
         .ToList();

            return View(leaves);
        }

        // GET: Admin/Leave/Details/5
        public ActionResult Details(int? id)
        {
            var leave = db.leaves
                .Where(e => e.id == id)
                .Select(e => new LeaveViewModel
                {
                    Id = e.id,
                    Leave_date = e.leave_date,
                    Reason = e.reason,
                    Approved = e.approved,
                    Leave_type = e.leave_type,
                    Employee_id = e.employee_id
                })
                .SingleOrDefault();

            if (leave == null)
            {
                return HttpNotFound();
            }

            return View(leave);
        }

        // GET: Admin/Leave/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Leave/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(leaf leave)
        {
            if (ModelState.IsValid)
            {
                db.leaves.Add(leave);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(leave);
            }
        }

        // GET: Admin/Leave/Edit/5
        public ActionResult Edit(int? id)
        {
            var leave = db.leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }

            var leaveViewModel = new LeaveViewModel
            {
                Id = leave.id,
                Leave_date = leave.leave_date,
                Reason = leave.reason,
                Approved = leave.approved,
                Leave_type = leave.leave_type,
                Employee_id = leave.employee_id
            };

            return View(leaveViewModel);
        }

        // POST: Admin/Leave/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, leaf leave)
        {
            var leaveToUpdate = db.leaves.Find(id);
            if (leaveToUpdate == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                leaveToUpdate.leave_date = leave.leave_date;
                leaveToUpdate.reason = leave.reason;
                leaveToUpdate.approved = leave.approved;
                leaveToUpdate.leave_type = leave.leave_type;
                leaveToUpdate.employee_id = leave.employee_id;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(leave);
            }
        }

        // GET: Admin/Leave/Delete/5
        public ActionResult Delete(int? id)
        {
            var leaveToDelete = db.leaves.Find(id);
            if (leaveToDelete == null)
            {
                return HttpNotFound();
            }

            db.leaves.Remove(leaveToDelete);
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
