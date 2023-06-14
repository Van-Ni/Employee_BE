using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonManager.Areas.Admin.Models
{
    public class EmployeeSalaryModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public List<int> Ids { get; set; }
    }
    public class EmployeeSalaryCalculator
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int TotalWorkDays { get; set; }
        public int TotalHolidayDays { get; set; }
        public int TotalPaidLeaveDays { get; set; }
        public int TotalOverTimeHours { get; set; }
        public decimal TotalSalary { get; set; }
        public bool calculatedSalary { get; set; }
    }
}