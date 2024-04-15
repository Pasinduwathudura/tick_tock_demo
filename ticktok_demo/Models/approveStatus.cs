using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ticktok_demo.Models
{
    //    public class approveStatus
    //    {
    //        public string employeeId { get; set; }
    //        public string trackingDate { get; set; }
    //        public string approvalStatus { get; set; }

    //        public dynamic CcEmails { get; set; }

    //        public dynamic Note { get; set; }
    //    }
    //}

    public class approveStatus
    {

        public string employeeId { get; set; }
        public string trackingDate { get; set; }
        public string approvalStatus { get; set; }
        public List<string> ccEmails { get; set; }
        public Boolean IsPartial { get; set; }
        public string note { get; set; }
    }

}
