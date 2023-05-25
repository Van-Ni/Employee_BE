using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonManager.Areas.Admin.Models
{
    public class RewardDisciplineViewModel
    {
        public int Id { get; set; }
        public DateTime? Transaction_date { get; set; }
        public decimal? Reward_amount { get; set; }
        public string Reason { get; set; }
        public int? Employee_id { get; set; }
    }
}