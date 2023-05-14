using PersonManager.Areas.Admin.Models;
using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonManager.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        private HRMEntities db = new HRMEntities();
        // GET: Admin/Role
        public ActionResult Index()
        {
            var roles = db.roles
         .Select(e => new RoleViewModel
         {
             Id = e.id,
             Name = e.name,
             Description = e.description
         })
         .ToList();

            return View(roles);
        }
        public ActionResult GetRole(int id)
        {
            var role = db.roles
                .Where(e => e.id == id)
                .Select(e => new RoleViewModel
                {
                    Id = e.id,
                    Name = e.name,
                    Description = e.description
                })
                .SingleOrDefault();

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }
        public ActionResult Delete(int id)
        {
            var roleToDelete = db.roles.Find(id);
            if (roleToDelete == null)
            {
                return HttpNotFound();
            }

            db.roles.Remove(roleToDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(role role)
        {
            if (ModelState.IsValid)
            {
                db.roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(role);
            }
        }
        public ActionResult Edit(int id)
        {
            var role = db.roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }

            var roleViewModel = new RoleViewModel
            {
                Id = role.id,
                Name = role.name,
                Description = role.description
            };

            return View(roleViewModel);
        }
        [HttpPost]
        public ActionResult Edit(int id, role role)
        {
            var roleToUpdate = db.roles.Find(id);
            if (roleToUpdate == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                roleToUpdate.name = role.name;
                roleToUpdate.description = role.description;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(role);
            }
        }
    }
}