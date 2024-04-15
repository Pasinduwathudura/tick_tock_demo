using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using ticktok_demo.Models;

namespace tiktocktest.Controllers
{
    public class InsertEventController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString;

        [HttpPost]
        public IHttpActionResult InsertEventLocation(EventLocationData data)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("InsertEventLocation", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@empId", data.empId);
                    command.Parameters.AddWithValue("@eventTime", data.eventTime);
                    command.Parameters.AddWithValue("@latitude", data.latitude);
                    command.Parameters.AddWithValue("@longitude", data.longitude);

                    // Execute the command and get the inserted eventId
                    int neweventId = Convert.ToInt32(command.ExecuteScalar());

                    // Return the inserted data with the new eventId
                    data.eventId = neweventId;
                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                return InternalServerError(ex);
            }
        }
    }
}