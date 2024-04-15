using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using ticktok_demo.Models;

namespace ticktok_demo.Controllers
{
    public class TrackingSheetYearController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString;

        public IHttpActionResult GetEmployee(Guid? id, int? year)
        {
            if (!id.HasValue || id == Guid.Empty)
            {
                return BadRequest("Invalid id");
            }

            try
            {
                List<object> formattedHolidays = new List<object>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("TrackingSheetYear", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmployeeID", id);
                        cmd.Parameters.AddWithValue("@Year", year);
                        SqlParameter outputParameter = cmd.Parameters.Add("@OutputList", SqlDbType.VarChar, -1);
                        outputParameter.Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();

                        // Check if output parameter is null or empty
                        if (outputParameter.Value == DBNull.Value || string.IsNullOrWhiteSpace(outputParameter.Value.ToString()))
                        {
                            return NotFound(); // Return 404 if no data found
                        }

                        string holidaysString = outputParameter.Value.ToString();
                        string[] holidaysArray = holidaysString.Split(new[] { "date:", "startTime:", "endTime:", "workingHours:", "projectId:", "employeeId:", "trackingId:", "project:", "dayType:", "approveStatus:" }, StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < holidaysArray.Length; i += 10)
                        {
                            DateTime date;
                            if (!DateTime.TryParse(holidaysArray[i], out date))
                            {
                                throw new ArgumentException("Invalid date format");
                            }

                            TimeSpan startTime = TimeSpan.Parse(holidaysArray[i + 1]);
                            TimeSpan endTime = TimeSpan.Parse(holidaysArray[i + 2]);
                            TimeSpan workingHours = TimeSpan.Parse(holidaysArray[i + 3]);
                            string projectId = holidaysArray[i + 4].Trim();
                            string employeeId = holidaysArray[i + 5].Trim();
                            string trackingId = holidaysArray[i + 6].Trim();
                            string project = holidaysArray[i + 7].Trim().TrimEnd(',');
                            string dayType = holidaysArray[i + 8].Trim().TrimEnd(',');
                            string approveStatus = holidaysArray[i + 9].Trim().TrimEnd(',');

                            formattedHolidays.Add(new
                            {
                                date = date,
                                startTime = startTime,
                                endTime = endTime,
                                workingHours = workingHours,
                                projectId = projectId,
                                employeeId = employeeId,
                                trackingId = trackingId,
                                project = project,
                                dayType = dayType,
                                approveStatus = approveStatus
                            });
                        }
                    }
                }

                return Ok(new Tracking { gettrackingsheet = formattedHolidays.ToArray() });
            }
            catch (SqlException ex)
            {
                // Log the exception
                // Handle SQL-related exceptions
                return InternalServerError(new Exception("An error occurred while accessing the database.", ex));
            }
            catch (ArgumentException ex)
            {
                // Log the exception
                // Handle parsing-related exceptions
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                // Handle other exceptions
                return InternalServerError(ex);
            }
        }
    }
}
