using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using Newtonsoft.Json;
using System.Text;
using ticktok_demo.Models;
using System.Web.WebPages;


namespace ticktok_demo.Controllers
{
   // [Authorize]
    [RoutePrefix("api/tracking_sheets")]
    public class TrackingController : ApiController
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString;

        // GET api/values

        public IHttpActionResult GetEmployee(Guid? id, int? month, int? year)
        {
            if (!id.HasValue || id == Guid.Empty)
            {
                return BadRequest("Invalid id");
            }

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString))
                {
                    Tracking trc = new Tracking();

                    using (SqlCommand cmd = new SqlCommand("GetTrackingData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmployeeID", id);
                        cmd.Parameters.AddWithValue("@Month", month);
                        cmd.Parameters.AddWithValue("@Year", year);
                        SqlParameter outputParameter = cmd.Parameters.Add("@OutputList", SqlDbType.VarChar, -1);
                        outputParameter.Direction = ParameterDirection.Output;

                        con.Open();

                        // Use ExecuteReader to retrieve results from the stored procedure
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Assuming your stored procedure returns data
                            if (reader.HasRows)
                            {
                                // Process your data here if needed
                                while (reader.Read())
                                {
                                    // Example: read data from reader
                                    // var someData = reader["ColumnName"];
                                }
                            }
                        }


                        // Assign the output parameter value to trc.OutputList
                        //trc.OutputList = "[" + outputParameter.Value.ToString() + "]";
                        //trc.OutputList = String.Format("[{0}]", outputParameter.Value);

                        var holidaysString = (string)cmd.Parameters["@OutputList"].Value;
                        var holidaysArray = holidaysString.Split(new[] { "date:", "startTime:", "endTime:", "workingHours:", "projectId:", "employeeId:", "trackingId:", "project:", "dayType:", "approveStatus:" }, StringSplitOptions.RemoveEmptyEntries);

                        var formattedHolidays = new List<object>();

                        for (int i = 0; i < holidaysArray.Length; i += 10)
                        {
                            DateTime date;
                            if (!DateTime.TryParse(holidaysArray[i], out date))
                            {
                                // Handle parsing failure
                                throw new ArgumentException("Invalid date format");
                            }

                            var startTime = TimeSpan.Parse(holidaysArray[i + 1]);
                            var endTime = TimeSpan.Parse(holidaysArray[i + 2]);
                            var workingHours = TimeSpan.Parse(holidaysArray[i + 3]);
                            var projectId = holidaysArray[i + 4].Trim();
                            var employeeId = holidaysArray[i + 5].Trim();
                            var trackingId = holidaysArray[i + 6].Trim();
                            var project = holidaysArray[i + 7].Trim().TrimEnd(','); ;
                            var dayType = holidaysArray[i + 8].Trim().TrimEnd(','); ;
                            var approveStatus = holidaysArray[i + 9].Trim().TrimEnd(','); ;

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

                        trc.gettrackingsheet = formattedHolidays.ToArray();

                    }

                    // Return your tracking object here
                    return Ok(trc);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }





        // POST api/values
        public void Post([FromBody] string value)
        {


        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
