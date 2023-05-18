using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonManager.Areas.Admin.Models
{
    public class ContractViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Note { get; set; }

    }
}