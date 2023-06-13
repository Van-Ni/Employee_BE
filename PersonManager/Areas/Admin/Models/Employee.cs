using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonManager.Areas.Admin.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public bool? Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? JoinDate { get; set; }
        public string Avatar { get; set; }
        public bool? Status { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? PositionId { get; set; }
        public string PositionName { get; set; }
        public int? ContractId { get; set; }
        public string ContractName { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
    }
    public class EmployeeTest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}