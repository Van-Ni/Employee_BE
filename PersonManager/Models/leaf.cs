//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PersonManager.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class leaf
    {
        public int id { get; set; }
        public Nullable<System.DateTime> leave_date { get; set; }
        public string reason { get; set; }
        public Nullable<bool> approved { get; set; }
        public string leave_type { get; set; }
        public Nullable<int> employee_id { get; set; }
    
        public virtual employee employee { get; set; }
    }
}
