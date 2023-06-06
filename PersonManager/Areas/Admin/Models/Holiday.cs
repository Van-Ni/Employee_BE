using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonManager.Areas.Admin.Models
{
    public class HolidayViewModel
    {
        public int Id { get; set; }
        public DateTime? Holiday_date { get; set; }
        public string Description { get; set; }
        public int? Days_off { get; set; }
    }
}