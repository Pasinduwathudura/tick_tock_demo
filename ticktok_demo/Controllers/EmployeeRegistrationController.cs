using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using Newtonsoft.Json;
using System.Text;
using ticktok_demo.Models;

namespace ticktok_demo.Controllers
{
    [RoutePrefix("api/EmployeeRegistration")]
    public class EmployeeRegistrationController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString;

        // POST api/employee_creation/create_employee
        [HttpPost]
        [Route("EmployeeRegistration")]
        public IHttpActionResult EmployeeRegistration([FromBody] Employee Employee)
        {
            if (Employee == null)
            {
                return BadRequest("Invalid employee data");
            }

            try
            {

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString))
                // using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spCreateEmployee", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        //cmd.Parameters.AddWithValue("@emp_id", Employee.emp_id);
                        cmd.Parameters.AddWithValue("@emp_first_name", Employee.emp_first_name);
                        cmd.Parameters.AddWithValue("@emp_last_name", Employee.emp_last_name);
                        cmd.Parameters.AddWithValue("@comp_id", Employee.comp_id);
                        //cmd.Parameters.AddWithValue("@holiday_id", Employee.holiday_id);
                        //cmd.Parameters.AddWithValue("@user_id", Employee.user_id);
                        cmd.Parameters.AddWithValue("@job_des_id", Employee.job_des_id);
                        cmd.Parameters.AddWithValue("@pic", Employee.pic);
                        cmd.Parameters.AddWithValue("@employee_no", Employee.employee_no);
                        cmd.Parameters.AddWithValue("@client_id", Employee.client_id);
                        cmd.Parameters.AddWithValue("@manager_id", Employee.manager_id);
                        cmd.Parameters.AddWithValue("@project_id", Employee.project_id);
                        cmd.Parameters.AddWithValue("@is_active_mobile", Employee.is_active_mobile);
                        cmd.Parameters.AddWithValue("@is_active_web", Employee.is_active_web);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok("Employee created successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
