using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonManager.Areas.Admin.Models
{
    public class PositionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}