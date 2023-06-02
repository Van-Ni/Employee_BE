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
    public class RewardDisciplineController : Controller
    {
        private HRMEntities db = new HRMEntities();

        // GET: Admin/RewardDiscipline
        public ActionResult Index()
        {
            var rewardDisciplines = db.rewardDisciplines
         .Select(e => new RewardDisciplineViewModel
         {
             Id = e.id,
             Transaction_date = e.transaction_date,
             Reward_amount = e.reward_amount,
             Reason = e.reason,
             Employee_id = e.employee_id
         })
         .ToList();

            return View(rewardDisciplines);
        }

        // GET: Admin/RewardDiscipline/Details/5
        public ActionResult Details(int? id)
        {
            var rewardDiscipline = db.rewardDisciplines
                .Where(e => e.id == id)
                .Select(e => new RewardDisciplineViewModel
                {
                    Id = e.id,
                    Transaction_date = e.transaction_date,
                    Reward_amount = e.reward_amount,
                    Reason = e.reason,
                    Employee_id = e.employee_id
                })
                .SingleOrDefault();

            if (rewardDiscipline == null)
            {
                return HttpNotFound();
            }

            return View(rewardDiscipline);
        }

        // GET: Admin/RewardDiscipline/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/RewardDiscipline/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(rewardDiscipline rewardDiscipline)
        {
            if (ModelState.IsValid)
            {
                db.rewardDisciplines.Add(rewardDiscipline);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(rewardDiscipline);
            }
        }

        // GET: Admin/RewardDiscipline/Edit/5
        public ActionResult Edit(int? id)
        {
            var rewardDiscipline = db.rewardDisciplines.Find(id);
            if (rewardDiscipline == null)
            {
                return HttpNotFound();
            }

            var rewardDisciplineViewModel = new RewardDisciplineViewModel
            {
                Id = rewardDiscipline.id,
                Transaction_date = rewardDiscipline.transaction_date,
                Reward_amount = rewardDiscipline.reward_amount,
                Reason = rewardDiscipline.reason,
                Employee_id = rewardDiscipline.employee_id
            };

            return View(rewardDisciplineViewModel);
        }

        // POST: Admin/RewardDiscipline/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, rewardDiscipline rewardDiscipline)
        {
            var rewardDisciplineToUpdate = db.rewardDisciplines.Find(id);
            if (rewardDisciplineToUpdate == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                rewardDisciplineToUpdate.transaction_date = rewardDiscipline.transaction_date;
                rewardDisciplineToUpdate.reward_amount = rewardDiscipline.reward_amount;
                rewardDisciplineToUpdate.reason = rewardDiscipline.reason;
                rewardDisciplineToUpdate.employee_id = rewardDiscipline.employee_id;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(rewardDiscipline);
            }
        }

        public ActionResult Delete(int? id)
        {
            var rewardDisciplineToDelete = db.rewardDisciplines.Find(id);
            if (rewardDisciplineToDelete == null)
            {
                return HttpNotFound();
            }

            db.rewardDisciplines.Remove(rewardDisciplineToDelete);
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
