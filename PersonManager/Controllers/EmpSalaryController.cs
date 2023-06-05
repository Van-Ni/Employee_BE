using Newtonsoft.Json;
using PersonManager.Areas.Admin.Models;
using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PersonManager.Controllers
{
    public class EmpSalaryController : ApiController
    {
        private HRMEntities db = new HRMEntities();
        // POST api/payroll
        [HttpPost]
        [Route("api/Payroll/Calculate")]
        public IHttpActionResult Calculate(EmployeeSalaryModel emps)
        {
            DateTime startDate = new DateTime(emps.Year, emps.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            int holidayCount = 0;

            // Lấy danh sách ngày lễ trong tháng
            var holidays = db.holidays
                .Where(h => h.holiday_date.Value.Month == emps.Month)
                .ToList();
            // Tính tổng các days_off của ngày lễ
            foreach (var holiday in holidays)
            {
                holidayCount += holiday.days_off ?? 0;
            }

            foreach (int employeeId in emps.Ids)
            {
                int attendanceCount = db.attendances
                .Count(a => a.employee_id == employeeId && a.date >= startDate && a.date <= endDate);
                int leaveCount = db.leaves
                .Count(l => l.employee_id == employeeId && l.leave_date >= startDate && l.leave_date <= endDate);
                int totalWorkDay = holidayCount + attendanceCount + leaveCount;
                return Ok(totalWorkDay);
            }
            return Ok();
        }
    }
}
