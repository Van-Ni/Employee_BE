using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonManager.Areas.Admin.Models
{
    public class SalaryViewModel
    {
        public int Id { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? Allowance { get; set; }
        public int? Coefficient { get; set; }
        public decimal? HourlyRate { get; set; }
        public int? Employee_id { get; set; }
    }
}