using PersonManager.Areas.Admin.Models;
using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PersonManager.Areas.Admin.Controllers
{
    public class EmpSalaryController : Controller
    {
        private HRMEntities db = new HRMEntities();

        [HttpGet]
        public ActionResult Index(int? month, int? year)
        {
            // Danh sách nhân viên
            List<EmployeeTest> employees = new List<EmployeeTest>
            {
             new EmployeeTest { Id = 13, Name = "vanni" },
             new EmployeeTest { Id = 12, Name = "quoc an" }
            };

            ViewBag.Employees = employees;
            ViewBag.Month = month;
            ViewBag.Year = year;


            return View(employees);
        }

        [HttpPost]
        public ActionResult Submit(int[] id, int month, int year)
        {
           // return Json(new { id, month, year });
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            // Tính tổng số ngày công hành chính trong tháng 
            int totalDaysInMonth = DateTime.DaysInMonth(year, month);
            int workingDays = 0;//ngày công chuẩn

            for (int day = 1; day <= totalDaysInMonth; day++)
            {
                DateTime currentDate = new DateTime(year, month, day);

                // Kiểm tra ngày có phải là ngày cuối tuần (Thứ 7 hoặc Chủ nhật) hay không
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDays++;
                }
            }

            int holidayCount = 0;

            // Lấy danh sách ngày lễ trong tháng
            var holidays = db.holidays
                .Where(h => h.holiday_date.Value.Month == month)
                .ToList();
            // Tính tổng các days_off của ngày lễ
            foreach (var holiday in holidays)
            {
                holidayCount += holiday.days_off ?? 0;
            }

            foreach (int employeeId in id)
            {
                int monthlySalary = 0;
                int attendanceCount = db.attendances
                .Count(a => a.employee_id == employeeId && a.date >= startDate && a.date <= endDate);
                int totalOvertimeHours = db.attendances
                .Where(a => a.employee_id == employeeId && a.date >= startDate && a.date <= endDate)
                .Sum(a => a.overtime_hours) ?? 0;
                int leaveCount = db.leaves
                .Count(l => l.employee_id == employeeId && l.leave_date >= startDate && l.leave_date <= endDate);
                int totalWorkDay = holidayCount + attendanceCount + leaveCount;//số ngày làm việc thực tế

                // Lấy thông tin lương của nhân viên
                var employeeSalary = db.salarys.FirstOrDefault(s => s.employee_id == employeeId);
                // Lấy danh sách rewardDiscipline của nhân viên
                var rewardDisciplines = db.rewardDisciplines.Where(rd => rd.employee_id == employeeId).ToList();
                if (employeeSalary != null)
                {
                    decimal basicSalary = employeeSalary.basicSalary ?? 0;
                    decimal allowance = employeeSalary.allowance ?? 0;

                    // Lương tháng = Lương + phụ cấp (nếu có) / ngày công chuẩn X Số ngày làm việc thực tế
                    monthlySalary = (int)((basicSalary + allowance) / workingDays * totalWorkDay);
                }
                if (totalOvertimeHours > 0)
                {
                    monthlySalary += (int)(totalOvertimeHours * employeeSalary.hourlyRate);
                }
                if (rewardDisciplines.Any())
                {

                    foreach (var rewardDiscipline in rewardDisciplines)
                    {
                        monthlySalary += (int)rewardDiscipline.reward_amount;
                    }

                }

                if (IsEmployeeSalaryExists(employeeId, month, year))
                {
                    UpdateEmployeeSalary(employeeId, month, year, totalWorkDay, holidayCount,
                        leaveCount, totalOvertimeHours, monthlySalary);
                }
                else
                {
                    // Add new salary record
                    AddEmployeeSalary(employeeId, month, year, totalWorkDay, holidayCount,
                        leaveCount, totalOvertimeHours, monthlySalary);
                }
            }
            return Json("ok");
            return RedirectToAction("Index");
        }

        // POST: /Payroll/Calculate
        [HttpPost]
        public ActionResult Calculate(EmployeeSalaryModel emps)
        {
            DateTime startDate = new DateTime(emps.Year, emps.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            // Tính tổng số ngày công hành chính trong tháng 
            int totalDaysInMonth = DateTime.DaysInMonth(emps.Year, emps.Month);
            int workingDays = 0;//ngày công chuẩn

            for (int day = 1; day <= totalDaysInMonth; day++)
            {
                DateTime currentDate = new DateTime(emps.Year, emps.Month, day);

                // Kiểm tra ngày có phải là ngày cuối tuần (Thứ 7 hoặc Chủ nhật) hay không
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDays++;
                }
            }

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
                int monthlySalary = 0;
                int attendanceCount = db.attendances
                .Count(a => a.employee_id == employeeId && a.date >= startDate && a.date <= endDate);
                int totalOvertimeHours = db.attendances
                .Where(a => a.employee_id == employeeId && a.date >= startDate && a.date <= endDate)
                .Sum(a => a.overtime_hours) ?? 0;
                int leaveCount = db.leaves
                .Count(l => l.employee_id == employeeId && l.leave_date >= startDate && l.leave_date <= endDate);
                int totalWorkDay = holidayCount + attendanceCount + leaveCount;//số ngày làm việc thực tế

                // Lấy thông tin lương của nhân viên
                var employeeSalary = db.salarys.FirstOrDefault(s => s.employee_id == employeeId);
                // Lấy danh sách rewardDiscipline của nhân viên
                var rewardDisciplines = db.rewardDisciplines.Where(rd => rd.employee_id == employeeId).ToList();
                if (employeeSalary != null)
                {
                    decimal basicSalary = employeeSalary.basicSalary ?? 0;
                    decimal allowance = employeeSalary.allowance ?? 0;

                    // Lương tháng = Lương + phụ cấp (nếu có) / ngày công chuẩn X Số ngày làm việc thực tế
                    monthlySalary = (int)((basicSalary + allowance) / workingDays * totalWorkDay);
                }
                if (totalOvertimeHours > 0)
                {
                    monthlySalary += (int)(totalOvertimeHours * employeeSalary.hourlyRate);
                }
                if (rewardDisciplines.Any())
                {

                    foreach (var rewardDiscipline in rewardDisciplines)
                    {
                        monthlySalary += (int)rewardDiscipline.reward_amount;
                    }

                }

                if (IsEmployeeSalaryExists(employeeId, emps.Month, emps.Year))
                {
                    UpdateEmployeeSalary(employeeId, emps.Month, emps.Year, totalWorkDay, holidayCount,
                        leaveCount, totalOvertimeHours, monthlySalary);
                }
                else
                {
                    // Add new salary record
                    AddEmployeeSalary(employeeId, emps.Month, emps.Year, totalWorkDay, holidayCount,
                        leaveCount, totalOvertimeHours, monthlySalary);
                }
            }
            return Json("ok");
            return RedirectToAction("Index");
        }

        private bool IsEmployeeSalaryExists(int employeeId, int month, int year)
        {
            return db.employee_salary.Any(s =>
                s.employee_id == employeeId && s.month == month && s.year == year);
        }

        private void UpdateEmployeeSalary(int employeeId, int month, int year, int totalWorkDays, int totalHolidayDays,
            int totalPaidLeaveDays, int totalOverTimeHours, decimal totalSalary)
        {
            var existingSalary = db.employee_salary.FirstOrDefault(s =>
                s.employee_id == employeeId && s.month == month && s.year == year);
            if (existingSalary != null)
            {
                // Các phần mã khác giữ nguyên
            }

            db.SaveChanges();
        }

        private void AddEmployeeSalary(int employeeId, int month, int year, int totalWorkDays, int totalHolidayDays,
            int totalPaidLeaveDays, int totalOverTimeHours, decimal totalSalary)
        {
            var newSalary = new employee_salary
            {
                employee_id = employeeId,
                month = month,
                year = year,
                totalWorkDays = totalWorkDays,
                totalHolidayDays = totalHolidayDays,
                totalPaidLeaveDays = totalPaidLeaveDays,
                totalOverTimeHours = totalOverTimeHours,
                totalSalary = totalSalary
            };

            db.employee_salary.Add(newSalary);
            db.SaveChanges();
        }



    }
}