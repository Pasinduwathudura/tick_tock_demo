using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Net.Http;
using System.Net;
using ticktok_demo.Models;

namespace ticktok_demo.Controllers
{
    public class userRolesController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString;

        // GET: api/getEmployeeDetailsbyManagerId?managerId={managerId}
        [System.Web.Http.HttpGet]
        //@userId
        public IHttpActionResult GetEmployeeDetailsByManagerId(string userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("login", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@userId", userId);

                        // Add output parameter for @EmployeeDetails
                        SqlParameter employeeDetailsParam = new SqlParameter("@output", SqlDbType.VarChar, -1);
                        employeeDetailsParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(employeeDetailsParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value (JSON string)
                        string employeeDetailsJson = Convert.ToString(command.Parameters["@output"].Value);

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