using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using ticktok_demo.Models;

namespace ticktok_demo.Controllers
{
    public class GetEmployeeDetailsByManagerIdController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString;

        // GET: api/getEmployeeDetailsbyManagerId?managerId={managerId}
        [HttpGet]
        public IHttpActionResult GetEmployeeDetailsByManagerId(string managerId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetManagerDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@managerId", managerId);

                        // Add output parameter for @EmployeeDetails
                        SqlParameter employeeDetailsParam = new SqlParameter("@EmployeeDetails", SqlDbType.NVarChar, -1);
                        employeeDetailsParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(employeeDetailsParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value (JSON string)
                        string employeeDetailsJson = Convert.ToString(command.Parameters["@EmployeeDetails"].Value);

                        // Deserialize JSON string into a list of objects
                        var employeeList = JsonConvert.DeserializeObject<List<object>>(employeeDetailsJson);

                        // Return the JSON string
                        return Ok(employeeList);
                    }
                }
            }
            catch (Exception ex)
            {
                //return InternalServerError(ex);
                var errorResponse = new ErrorResponse
                {
                    Status = "Error",
                    Message = ex.Message
                };

                // Construct an HttpResponseMessage with status code 500 and the error message
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);

                // Return the response
                return ResponseMessage(response);
            }
        }
    }
}
