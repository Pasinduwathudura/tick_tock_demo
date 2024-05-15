using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using ticktok_demo.Models;

namespace ticktok_demo.Controllers
{
    public class UpdateEntriesByManagerController : ApiController
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString;

        [HttpPost]
        public IHttpActionResult InsertTrackingData([FromBody] JArray jsonData)
        {
            if (jsonData == null || jsonData.Count == 0)
            {
                return BadRequest("No data provided ##.");
            }

            string outputMessage = "";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    foreach (var item in jsonData)
                    {
                        var json = item.ToString();
                        using (SqlCommand cmd = new SqlCommand("updateEntriesByManager", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@jsonInput", json);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                return Ok("Entries updated successfully.");
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
