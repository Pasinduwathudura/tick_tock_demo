using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
//using tiktocktest.Models;

namespace tiktocktest.Controllers
{
    [Authorize]
    public class TaskListController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString;

        // GET: api/taskList/GetTasksByTrackingSheetId?trackingSheetId={trackingSheetId}
        [HttpGet]
        public IHttpActionResult GetTasksByTrackingSheetId(string trackingSheetId)
        {
            List<object> formattedTasks = new List<object>();

            // SQL query to call the stored procedure
            string query = "EXEC GetTasksByTrackingSheetId @TrackingSheetId, @Result OUTPUT";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TrackingSheetId", trackingSheetId);

                    // Add output parameter for @Result
                    command.Parameters.Add("@Result", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value
                        string result = Convert.ToString(command.Parameters["@Result"].Value);

                        // Split the result string
                        //string[] taskData = result.Split(new[] { "trackingId:", "taskId:", "taskName:", "taskStartTime:", "taskEndTime:", "taskDescription:", "projectId:", "taskDate:" }, StringSplitOptions.RemoveEmptyEntries);

                        // Format the tasks
                        string[] taskData = result.Split(new[] { "trackingId:", "taskId:", "taskName:", "taskStartTime:", "taskEndTime:", "taskDescription:", "projectId:", "taskDate:" }, StringSplitOptions.RemoveEmptyEntries);

                        // Format the tasks
                        for (int i = 0; i < taskData.Length; i += 8) // Changed to 8 because of the additional fields
                        {
                            var trackingId = taskData[i].Trim();
                            var taskId = taskData[i + 1].Trim();
                            var taskName = taskData[i + 2].Trim();
                            var taskStartTime = taskData[i + 3].Trim();
                            var taskEndTime = taskData[i + 4].Trim();
                            var taskDescription = taskData[i + 5].Trim();
                            var projectId = taskData[i + 6].Trim();
                            var taskDate = taskData[i + 7].Trim();

                            formattedTasks.Add(new
                            {
                                trackingId = trackingId,
                                taskId = taskId,
                                taskName = taskName,
                                taskStartTime = taskStartTime,
                                taskEndTime = taskEndTime,
                                taskDescription = taskDescription,
                                projectId = projectId,
                                taskDate = taskDate
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        // You can use a logging framework like Serilog, NLog, or log4net
                        // Example: logger.Error("An error occurred while getting tasks by tracking sheet ID.", ex);

                        // Return a meaningful error response
                        return InternalServerError(ex); // Or appropriate HTTP status code
                    }
                }
            }

            return Ok(new { Results = formattedTasks });
        }
    }
}
