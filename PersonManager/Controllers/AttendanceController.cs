using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PersonManager.Controllers
{
    public class AttendanceController : ApiController
    {
        private HRMEntities db = new HRMEntities();

        [HttpGet]
        [Route("api/Attendance/GetAttendances")]
        public IHttpActionResult GetAttendances()
        {
            var attendances = db.attendances
                .Select(a => new {
                    Id = a.id,
                    Date = a.date,
                    CheckInTime = a.check_in_time,
                    CheckOutTime = a.check_out_time,
                    Overtime = a.overtime_hours,
                    EmployeeId = a.employee_id,
                    Status = a.status
                })
                .ToList();
            if (attendances.Count == 0)
            {
                return NotFound();
            }
            return Ok(attendances);
        }
        [HttpPost]
        public IHttpActionResult CheckIn(int id)
        {
            var employee = db.employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            var now = DateTime.Now.TimeOfDay; 
            if (now > TimeSpan.FromHours(10))
            {
                return BadRequest("Cannot check in after 10am.");
            }
            if (now > TimeSpan.FromHours(6.5))
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
            }
            var attendance = new attendance
            {
                date = DateTime.Today,
                check_in_time = now,
                employee_id = id,
                status = now <= TimeSpan.FromHours(8.5) ? 0 : 1
            };

            db.attendances.Add(attendance);
            db.SaveChanges();

            return Ok();
            
        }
        [HttpPut]
        public IHttpActionResult CheckOut(int id)
        {
            // Tìm bản ghi điểm danh của nhân viên với employeeId và ngày hiện tại
            var attendance = db.attendances
                .Where(a => a.employee_id == id && a.date == DateTime.Today)
                .FirstOrDefault();

            if (attendance == null)
            {
                // Nếu không tìm thấy bản ghi điểm danh
                return NotFound();
            }
            else if (attendance.check_out_time != null)
            {
                // Nếu đã checkout rồi
                return BadRequest("checked out");
            }
            else
            {
                // Thực hiện checkout và cập nhật bản ghi điểm danh
                var now = DateTime.Now.TimeOfDay;
                if (now > TimeSpan.FromHours(19))
                {
                    attendance.overtime_hours = 2;
                    attendance.check_out_time = now;
                }
                else if (now > TimeSpan.FromHours(17))
                {
                    attendance.check_out_time = now;
                }
                else
                {
                    return BadRequest("Cannot check out before 5 PM");
                }

                db.SaveChanges();

                return Ok();
            }
        }
    }
}
