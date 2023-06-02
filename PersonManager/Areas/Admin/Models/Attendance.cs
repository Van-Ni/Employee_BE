using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonManager.Areas.Admin.Models
{
    public class AttendanceViewModel
    {
        public int Id { get; set; }
        public int? Employee_id { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Check_in_time { get; set; }
        public TimeSpan? Check_out_time { get; set; }
        public float? Overtime_hours { get; set; }
        public int? Status { get; set; }
    }
}