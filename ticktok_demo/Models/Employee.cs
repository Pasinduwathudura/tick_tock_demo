using System;

namespace ticktok_demo.Models
{
    public class Employee
    {

        public Guid emp_id { get; set; }
        public string emp_first_name { get; set; }
        public string emp_last_name { get; set; }
        public Guid comp_id { get; set; }
        public Guid holiday_id { get; set; }
        public Guid user_id { get; set; }
        public Guid job_des_id { get; set; }
        public string pic { get; set; }
        public string employee_no { get; set; }
        public Guid client_id { get; set; }
        public Guid manager_id { get; set; }
        public Guid project_id { get; set; }
        public bool is_active_mobile { get; set; }
        public bool is_active_web { get; set; }

    }
}