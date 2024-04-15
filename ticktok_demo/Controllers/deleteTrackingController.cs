using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Http;

namespace tiktocktest.Controllers
{
    public class DeleteTrackingController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString;

        public IHttpActionResult DeleteTracking(Guid trackingId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("DeleteTracking", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@trackingId", trackingId);

                        con.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // return Ok("NotFound");
                            return Content(System.Net.HttpStatusCode.NotFound, "Tracking with ID " + trackingId + " not found.");
                        }
                        else
                        {
                            // return NotFound();
                            return Ok("trackingId " + trackingId + " deleted successfully.");
                            // Or appropriate HTTP status code
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                // You can use a logging framework like Serilog, NLog, or log4net
                // Example: logger.Error("An error occurred while deleting tracking information.", ex);

                // Return a meaningful error response
                return InternalServerError(ex); // Or appropriate HTTP status code
            }
        }

        public void Post([FromBody] string value)
        {
            // Implement POST method logic here
        }

        public void Put(int id, [FromBody] string value)
        {
            // Implement PUT method logic here
        }
    }
}
