using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ticktok_demo.Models
{
    public class Dashboard
    {
        public string employeeId { get; set; }
        public string empFirstName { get; set; }
        public string pic { get; set; }
        public string employeeNo { get; set; }
        public string activeMonth { get; set; }
        public string companyId { get; set; }
        public string compName { get; set; }
        public string clientId { get; set; }
        public string clientName { get; set; }
        public string jobName { get; set; }
        public string projectId { get; set; }
        public string projectName { get; set; }
        public string managerId { get; set; }
        public string managerName { get; set; }
        public string managerEmail { get; set; }
        //public string workedDays { get; set; }
        public float numWorkingDays { get; set; }
        public float workedMinutes { get; set; }
        public float totalWorkedDays { get; set; }
        public float totalLeaveDays { get; set; }
        public float leavesTaken { get; set; }
        public float annualLeave { get; set; }
        public float medicalLeaves { get; set; }
        public float unpaidLeaves { get; set; }
        public float leaveBalance { get; set; }
        public float companyOff { get; set; }
        public float annualLeaveBalance { get; set; }
        public float medicalLeaveBalance { get; set; }
        public string countryId { get; set; }
        public string countryName { get; set; }
        public dynamic holidays { get; set; }
        public dynamic projectList { get; set; }
        public dynamic companyLocations { get; set; }
        public dynamic homeLocations { get; set; }
        public dynamic approvalLevels { get; set; }

        //public string leaves { get; set; }


    }
}