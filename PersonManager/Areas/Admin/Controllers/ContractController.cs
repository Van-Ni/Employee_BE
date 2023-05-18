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
    public class ContractController : Controller
    {
        private HRMEntities db = new HRMEntities();

        // GET: Admin/Contract
        public ActionResult Index()
        {
            var contracts = db.contracts
         .Select(e => new ContractViewModel
         {
             Id = e.id,
             Type = e.type,
             StartDate = e.startDate,
             EndDate = e.endDate,
             Note = e.note
         })
         .ToList();

            return View(contracts);
        }

        // GET: Admin/Contract/Details/5
        public ActionResult Details(int? id)
        {
            var contract = db.contracts
                .Where(e => e.id == id)
                .Select(e => new ContractViewModel
                {
                    Id = e.id,
                    Type = e.type,
                    StartDate = e.startDate,
                    EndDate = e.endDate,
                    Note = e.note
                })
                .SingleOrDefault();

            if (contract == null)
            {
                return HttpNotFound();
            }

            return View(contract);
        }

        // GET: Admin/Contract/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Contract/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(contract contract)
        {
            if (ModelState.IsValid)
            {
                db.contracts.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(contract);
            }
        }

        // GET: Admin/Contract/Edit/5
        public ActionResult Edit(int? id)
        {
            var contract = db.contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }

            var contractViewModel = new ContractViewModel
            {
                Id = contract.id,
                Type = contract.type,
                StartDate = contract.startDate,
                EndDate = contract.endDate,
                Note = contract.note
            };

            return View(contractViewModel);
        }

        // POST: Admin/Contract/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, contract contract)
        {
            var contractToUpdate = db.contracts.Find(id);
            if (contractToUpdate == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                contractToUpdate.type = contract.type;
                contractToUpdate.startDate = contract.startDate;
                contractToUpdate.endDate = contract.endDate;
                contractToUpdate.note = contract.note;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(contract);
            }
        }

        public ActionResult Delete(int? id)
        {
            var contractToDelete = db.contracts.Find(id);
            if (contractToDelete == null)
            {
                return HttpNotFound();
            }

            db.contracts.Remove(contractToDelete);
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
