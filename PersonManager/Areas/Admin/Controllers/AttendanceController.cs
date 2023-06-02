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
    public class AttendanceController : Controller
    {
        private HRMEntities db = new HRMEntities();

        // GET: Admin/Attendance
        public ActionResult Index()
        {
            var attendances = db.attendances
         .Select(e => new AttendanceViewModel
         {
             Id = e.id,
             Employee_id = e.employee_id,
             Date = e.date,
             Check_in_time = e.check_in_time,
             Check_out_time = e.check_out_time, 
             Overtime_hours = e.overtime_hours,
             Status = e.status
         })
         .ToList();

            return View(attendances);
        }
    }
}
