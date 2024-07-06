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
    [RoutePrefix("api/UpdateEmployee")]
    public class UpdateEmployeeController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString;

        // POST api/UpdateEmployee/UpdateEmployee
        [HttpPost]
        [Route("UpdateEmployee")]
        public IHttpActionResult UpdateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Invalid employee data");
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spUpdateEmployee", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@emp_id", employee.emp_id);
                        cmd.Parameters.AddWithValue("@emp_first_name", employee.emp_first_name);
                        cmd.Parameters.AddWithValue("@emp_last_name", employee.emp_last_name);
                        cmd.Parameters.AddWithValue("@comp_id", employee.comp_id);
                        cmd.Parameters.AddWithValue("@holiday_id", employee.holiday_id);
                        cmd.Parameters.AddWithValue("@user_id", employee.user_id);
                        cmd.Parameters.AddWithValue("@job_des_id", employee.job_des_id);
                        cmd.Parameters.AddWithValue("@pic", employee.pic);
                        cmd.Parameters.AddWithValue("@employee_no", employee.employee_no);
                        cmd.Parameters.AddWithValue("@client_id", employee.client_id);
                        cmd.Parameters.AddWithValue("@manager_id", employee.manager_id);
                        cmd.Parameters.AddWithValue("@project_id", employee.project_id);
                        cmd.Parameters.AddWithValue("@is_active_mobile", employee.is_active_mobile);
                        cmd.Parameters.AddWithValue("@is_active_web", employee.is_active_web);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok("Employee updated successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
