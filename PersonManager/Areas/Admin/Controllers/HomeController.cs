using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonManager.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private HRMEntities db = new HRMEntities();

        // GET: Admin/Home
        public ActionResult Index(user userAcc)
        {
            if (ModelState.IsValid)
            {
                if (Session["id"] != null)
                {
                    ViewBag.DepartmentTotal = db.departments.Count();
                    ViewBag.PositionTotal = db.positions.Count();
                    ViewBag.EmployeeTotal = db.employees.Count();
                    ViewBag.LeaveTotal = db.leaves.Count();
                    ViewBag.ContractTotal = db.contracts.Count();
                    ViewBag.UserTotal = db.users.Count();
                    ViewBag.RoleTotal = db.roles.Count();
                    ViewBag.SalaryTotal = db.salarys.Count();
                    return View();
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            return View(userAcc);
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var data = db.users.Where(s => s.username.Equals(username) && s.password.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["id"] = data.FirstOrDefault().id;
                    Session["username"] = data.FirstOrDefault().username;
                    Session["roleid"] = data.FirstOrDefault().role_id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();

        }

        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

    }
}