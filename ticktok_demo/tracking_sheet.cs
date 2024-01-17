//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ticktok_demo
{
    using System;
    using System.Collections.Generic;
    
    public partial class tracking_sheet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tracking_sheet()
        {
            this.tasks = new HashSet<task>();
        }
    
        public System.Guid tracking_id { get; set; }
        public Nullable<System.Guid> employee_id { get; set; }
        public Nullable<System.DateTime> tracking_date { get; set; }
        public Nullable<System.TimeSpan> tracking_start_time { get; set; }
        public Nullable<System.TimeSpan> tracking_end_time { get; set; }
        public Nullable<int> total_hours { get; set; }
        public Nullable<System.Guid> company_id { get; set; }
        public Nullable<System.Guid> project_id { get; set; }
    
        public virtual company company { get; set; }
        public virtual employee employee { get; set; }
        public virtual project project { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<task> tasks { get; set; }
    }
}
