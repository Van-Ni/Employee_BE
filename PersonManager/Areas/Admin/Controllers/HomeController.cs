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
        public ActionResult Index()
        {
            ViewBag.DepartmentTotal = db.departments.Count();
            ViewBag.PositionTotal = db.positions.Count();
            ViewBag.EmployeeTotal = db.employees.Count();
            ViewBag.LeaveTotal = db.leaves.Count();
            ViewBag.ContractTotal = db.contracts.Count();
            ViewBag.UserTotal = db.users.Count();
            ViewBag.RoleTotal = db.roles.Count();
            return View();
        }
    }
}