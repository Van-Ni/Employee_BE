using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonManager.Areas.Admin.Models
{
    public class LeaveViewModel
    {
        public int Id { get; set; }
        public DateTime? Leave_date { get; set; }
        public string Reason { get; set; }
        public Boolean? Approved { get; set; }
        public string Leave_type { get; set; }
        public int? Employee_id { get; set; }
    }
}