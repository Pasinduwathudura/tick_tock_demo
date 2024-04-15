using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ticktok_demo.Models;
using System.Web.WebPages;

namespace ticktok_demo.Controllers
{
    // [Authorize]
    //[RoutePrefix("api/CalendarEntries")]
    public class CalendarEntriesController : ApiController
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString;

        // GET api/values

        public IHttpActionResult GetEmployee(Guid? EmployeeId, Guid? CountryId, int? Month, int? Year)
        {
            if (!EmployeeId.HasValue || EmployeeId == Guid.Empty)
            {
                return BadRequest("Invalid id");
            }

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString))
                {
                    CalendarEntry trc = new CalendarEntry();

                    using (SqlCommand cmd = new SqlCommand("CalenderEntries", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmployeeID", EmployeeId);
                        cmd.Parameters.AddWithValue("@Month", Month);
                        cmd.Parameters.AddWithValue("@Year", Year);
                        cmd.Parameters.AddWithValue("@CountryId", CountryId);
                        SqlParameter outputParameter = cmd.Parameters.Add("@CalendarEntries", SqlDbType.VarChar, -1);
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

                        var holidaysString = (string)cmd.Parameters["@CalendarEntries"].Value;
                        var holidaysArray = holidaysString.Split(new[] { "date:", "weekDayName:", "dayStatus:", "workedType:", "workedMinitue:", "isWorked:", "approvalStatus:", "holidayName:", "holidayType:", "trackingId:", "dayType:" }, StringSplitOptions.RemoveEmptyEntries);

                        var formattedHolidays = new List<object>();

                        for (int i = 0; i < holidaysArray.Length; i += 11)
                        {
                            DateTime date;
                            if (!DateTime.TryParse(holidaysArray[i], out date))
                            {
                                // Handle parsing failure
                                throw new ArgumentException("Invalid date format");
                            }

                            var weekDayName = holidaysArray[i + 1].Trim();
                            var dayStatus = holidaysArray[i + 2].Trim(); ;
                            var workedType = holidaysArray[i + 3].Trim();
                            var workedMinitue = holidaysArray[i + 4].Trim().AsInt(); ;
                            var isWorked = holidaysArray[i + 5].Trim().AsBool(); ;
                            var approvalStatus = holidaysArray[i + 6].Trim();
                            var holidayName = holidaysArray[i + 7].Trim().TrimEnd(','); ;
                            var holidayType = holidaysArray[i + 8].Trim().TrimEnd(','); ;
                            var trackingId = holidaysArray[i + 9].Trim().TrimEnd(','); ;
                            var dayType = holidaysArray[i + 10].Trim().TrimEnd(','); ;

                            formattedHolidays.Add(new
                            {
                                date = date.ToString("yyyy-MM-dd"),
                                trackingId = trackingId,
                                weekDayName = weekDayName,
                                dayStatus = dayStatus,
                                dayType = dayType,
                                workedType = workedType,
                                workedMinutes = workedMinitue,
                                isWorked = isWorked,
                                approvalStatus = approvalStatus,
                                holidayName = holidayName,
                                holidayType = holidayType,
                                returnType = "APPROVED"
                            });
                        }

                        trc.calendarEntrys = formattedHolidays.ToArray();

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