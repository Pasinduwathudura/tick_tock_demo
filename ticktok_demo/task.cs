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
    
    public partial class task
    {
        public System.Guid taskId { get; set; }
        public string taskName { get; set; }
        public Nullable<System.TimeSpan> taskStartTime { get; set; }
        public Nullable<System.TimeSpan> taskEndTime { get; set; }
        public string taskDescription { get; set; }
        public Nullable<System.Guid> trackingSheetId { get; set; }
        public Nullable<System.Guid> projectId { get; set; }
        public string taskDate { get; set; }
    
        public virtual project project { get; set; }
        public virtual tracking_sheet tracking_sheet { get; set; }
    }
}
