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
}