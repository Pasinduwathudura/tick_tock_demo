using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace tiktocktest.Controllers
{
    public class CalendarController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString;

        [HttpGet]
        public IHttpActionResult GetCalendarDetails(Guid countryId, int year, Guid employeeId)
        {
            try
            {
                // Define the parameters for the stored procedure
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@country_id", SqlDbType.UniqueIdentifier) { Value = countryId },
                    new SqlParameter("@year", SqlDbType.Int) { Value = year },
                    new SqlParameter("@employee_id", SqlDbType.UniqueIdentifier) { Value = employeeId },
                    new SqlParameter("@holiday_output", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output },
                    new SqlParameter("@leave_output", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output }
                };

                // Call the stored procedure
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("dbo.CalenderDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(parameters.ToArray());

                        connection.Open();
                        command.ExecuteNonQuery();

                        string leaveResult = Convert.ToString(command.Parameters["@leave_output"].Value);

                        // Split the leave result string
                        string[] leaveTaskData = leaveResult.Split(new[] { "leaveDate :", "leaveReason :", "leaveStatus :", "halfDayType :", "leaveRequestDate :", "leaveGroup :", "leaveGroupId :", "approveByEmpId :", "approvePerson :" }, StringSplitOptions.RemoveEmptyEntries);

                        // Format the leave tasks
                        List<object> formattedLeaveTasks = new List<object>();
                        for (int i = 0; i < leaveTaskData.Length; i += 9)
                        {
                            //var leaveDate = DateTime[i].Trim();
                            var leaveDate = DateTime.Parse(leaveTaskData[i].Trim());
                            var leaveReason = leaveTaskData[i + 1].Trim();
                            var leaveStatus = leaveTaskData[i + 2].Trim();
                            var halfDayType = leaveTaskData[i + 3].Trim();
                            var leaveRequestDate = DateTime.Parse(leaveTaskData[i + 4].Trim());
                            var leaveGroup = leaveTaskData[i + 5].Trim();
                            var leaveGroupId = leaveTaskData[i + 6].Trim();
                            var approveByEmpId = leaveTaskData[i + 7].Trim();
                            var approvePerson = leaveTaskData[i + 8].Trim().TrimEnd(','); ;



                            formattedLeaveTasks.Add(new
                            {
                                leaveDate = leaveDate,
                                leaveReason = leaveReason,
                                leaveStatus = leaveStatus,
                                leaveType = halfDayType,
                                leaveGroup = leaveGroup,
                                leaveGroupId = leaveGroupId,
                                approveByEmpId = approveByEmpId,
                                approvePerson = approvePerson,
                                leaveRequestDate = leaveRequestDate
                            });
                        }

                        string holidayResult = Convert.ToString(command.Parameters["@holiday_output"].Value);

                        // Split the holiday result string
                        string[] holidayTaskData = holidayResult.Split(new[] { "holidayDate :", "holidayName :", "holidayType :" }, StringSplitOptions.RemoveEmptyEntries);

                        // Format the holiday tasks
                        List<object> formattedHolidayTasks = new List<object>();
                        for (int i = 0; i < holidayTaskData.Length; i += 3)
                        {
                            var holidayDate = holidayTaskData[i].Trim();
                            var holidayName = holidayTaskData[i + 1].Trim();
                            var holidayType = holidayTaskData[i + 2].Trim().TrimEnd(','); ;

                            formattedHolidayTasks.Add(new
                            {
                                holidayDate = holidayDate,
                                holidayName = holidayName,
                                holidayType = holidayType
                            });
                        }

                        // Merge leave and holiday tasks
                        var allTasks = new { leaves = formattedLeaveTasks, holidays = formattedHolidayTasks };

                        return Ok(allTasks);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions
                return InternalServerError(ex);
            }
        }
    }
}
