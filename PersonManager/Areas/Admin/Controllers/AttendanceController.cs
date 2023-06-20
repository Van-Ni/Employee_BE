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
                .Include("employee")
         .Select(e => new AttendanceViewModel
         {
             Id = e.id,
             EmployeeName = e.employee.fullname,
             Date = e.date,
             Check_in_time = e.check_in_time,
             Check_out_time = e.check_out_time, 
             Overtime_hours = e.overtime_hours,
             Status = e.status
         })
         .ToList();

            return View(attendances);
        }
        public ActionResult Employee()
        {
            // Lấy mã nhân viên
            var id = (int)HttpContext.Application["id"];
            // Lấy tháng và năm hiện tại
            var now = DateTime.Now;
            var month = now.Month;
            var year = now.Year;

            // Lấy danh sách attendance theo tháng và năm hiện tại và employee_id
            var attendances = db.attendances
                .Where(a => a.date.Value.Month == month && a.date.Value.Year == year && a.employee_id == id)
                .Include("employee")
                .Select(e => new AttendanceViewModel
                {
                    Id = e.id,
                    Employee_id = (int)e.employee_id,
                    EmployeeName = e.employee.fullname,
                    Date = e.date,
                    Check_in_time = e.check_in_time,
                    Check_out_time = e.check_out_time,
                    Overtime_hours = e.overtime_hours,
                    Status = e.status
                })
                .ToList();

            return View(attendances);
        }
        [HttpPost]
        public ActionResult CheckIn()
        {
            // Lấy mã nhân viên
            var id = (int)HttpContext.Application["id"];

            var employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var now = DateTime.Now.TimeOfDay;
          
            if (now > TimeSpan.FromHours(8.5))
            {
                // Thêm bản ghi vào rewardDiscipline
                var reward = new rewardDiscipline
                {
                    transaction_date = DateTime.Now,
                    reward_amount = -50000,
                    reason = "đi trễ",
                    employee_id = id
                };
                db.rewardDisciplines.Add(reward);
                db.SaveChanges();
                return RedirectToAction("Employee");
            }
            var attendance = new attendance
            {
                date = DateTime.Today,
                check_in_time = now,
                employee_id = id,
                status = now < TimeSpan.FromHours(8.5) ? 0 : 1
            };

            db.attendances.Add(attendance);
            db.SaveChanges();
            return RedirectToAction("Employee");
        }
        [HttpPost]
        public ActionResult CheckOut()
        {
            // Lấy mã nhân viên
            var id = (int)HttpContext.Application["id"];
            // Tìm bản ghi điểm danh của nhân viên với employeeId và ngày hiện tại
            var attendance = db.attendances
                .Where(a => a.employee_id == id && a.date == DateTime.Today)
                .FirstOrDefault();

            if (attendance == null)
            {
                // Nếu không tìm thấy bản ghi điểm danh
                return HttpNotFound();
            } else
            {
                // Thực hiện checkout và cập nhật bản ghi điểm danh
                var now = DateTime.Now.TimeOfDay;
                if (now > TimeSpan.FromHours(19))
                {
                    attendance.overtime_hours = 2;
                    attendance.check_out_time = now;
                }
                else if (now > TimeSpan.FromHours(17.5))
                {
                    attendance.check_out_time = now;
                }
                db.SaveChanges();
                return RedirectToAction("Employee");
            }
        }


    }

    

}
