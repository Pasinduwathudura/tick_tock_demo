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
    
    public partial class holiday
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public holiday()
        {
            this.calenders = new HashSet<calender>();
            this.employees = new HashSet<employee>();
        }
    
        public System.Guid holiday_id { get; set; }
        public string holiday_name { get; set; }
        public Nullable<System.Guid> company_id { get; set; }
        public Nullable<System.Guid> country_id { get; set; }
        public Nullable<System.DateTime> holiday_date { get; set; }
        public string holiday_type { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<calender> calenders { get; set; }
        public virtual company company { get; set; }
        public virtual country country { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<employee> employees { get; set; }
    }
}
