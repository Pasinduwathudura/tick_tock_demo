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
    [Authorize]
    [RoutePrefix("api/dashboard")]

    public class DashboardController : ApiController
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString);
        Dashboard emp = new Dashboard();

        // GET api/values/5

        public IHttpActionResult GetEmployee(string username, int? month, int? year)
        {
            try
            {
                // Check if the email is null or empty

                if (string.IsNullOrEmpty(username))
                {
                    // Return BadRequest if email is invalid
                    return BadRequest("Invalid email");
                }

                // Your database connection setup
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString);
                Dashboard emp = new Dashboard();
                {
                    using (SqlCommand cmd = new SqlCommand("DashBoardTest", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@empId", id);
                        cmd.Parameters.AddWithValue("@month", month);
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.Parameters.AddWithValue("@email", username);
                        cmd.Parameters.Add("@workedDays", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@empFirstName", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@pic", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@employeeNo", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@companyId", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@jobName", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@employeeId", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@countryName", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@compName", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@holidays", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@projectName", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@leaves", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@numWorkingDays", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@workedMinutes", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@totalLeaveDays", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@leavesTaken", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@leaveBalance", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@projectId", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        //2024/02/22
                        cmd.Parameters.Add("@managerName", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@clientName", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@clientId", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@managerId", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@projectList", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@countryId", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@companyLocations", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@homeLocations", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        //2024/02/26
                        cmd.Parameters.Add("@companyOff", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@annualLeaveBalance", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@medicalLeaveBalance", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@activeMonth", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        //2024/04/16
                        cmd.Parameters.Add("@managerEmail", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@totalWorkedDays", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@approvalLevels", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@annualLeaves", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@medicalLeaves", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@unpaidLeaves", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();

                        // Read the output parameters and construct the employee object
                        // employee emp = null;
                        if (cmd.Parameters["@email"].Value != DBNull.Value)
                        {
                            emp = new Dashboard();

                            emp.empFirstName = cmd.Parameters["@empFirstName"].Value.ToString();
                            emp.pic = cmd.Parameters["@pic"].Value.ToString();
                            emp.employeeNo = cmd.Parameters["@employeeNo"].Value.ToString();
                            emp.activeMonth = cmd.Parameters["@activeMonth"].Value.ToString();
                            emp.companyId = cmd.Parameters["@companyId"].Value.ToString();
                            emp.jobName = cmd.Parameters["@jobName"].Value.ToString();
                            emp.employeeId = cmd.Parameters["@employeeId"].Value.ToString();
                            emp.countryName = cmd.Parameters["@countryName"].Value.ToString();
                            emp.compName = cmd.Parameters["@compName"].Value.ToString();
                            emp.projectName = cmd.Parameters["@projectName"].Value.ToString();
                            emp.projectId = cmd.Parameters["@projectId"].Value.ToString();
                            emp.countryId = cmd.Parameters["@countryId"].Value.ToString();
                            emp.numWorkingDays = cmd.Parameters["@numWorkingDays"].Value.ToString().AsFloat();
                            //emp.workedDays = cmd.Parameters["@workedDays"].Value.ToString();
                            emp.workedMinutes = cmd.Parameters["@workedMinutes"].Value.ToString().AsFloat();
                            // Convert holidays string to list
                            emp.totalLeaveDays = cmd.Parameters["@totalLeaveDays"].Value.ToString().AsFloat();
                            emp.totalWorkedDays = cmd.Parameters["@totalWorkedDays"].Value.ToString().AsFloat();
                            emp.leavesTaken = cmd.Parameters["@leavesTaken"].Value.ToString().AsFloat();
                            emp.annualLeave = cmd.Parameters["@annualLeaves"].Value.ToString().AsFloat();
                            emp.medicalLeaves = cmd.Parameters["@medicalLeaves"].Value.ToString().AsFloat();
                            emp.unpaidLeaves = cmd.Parameters["@unpaidLeaves"].Value.ToString().AsFloat();
                            emp.leaveBalance = cmd.Parameters["@leaveBalance"].Value.ToString().AsFloat();
                            emp.companyOff = cmd.Parameters["@companyOff"].Value.ToString().AsFloat();
                            emp.annualLeaveBalance = cmd.Parameters["@annualLeaveBalance"].Value.ToString().AsFloat();
                            emp.medicalLeaveBalance = cmd.Parameters["@medicalLeaveBalance"].Value.ToString().AsFloat();
                            //2024/02/22
                            emp.managerName = cmd.Parameters["@managerName"].Value.ToString();
                            emp.clientName = cmd.Parameters["@clientName"].Value.ToString();
                            emp.managerEmail = cmd.Parameters["@managerEmail"].Value.ToString().Trim();
                            emp.clientId = cmd.Parameters["@clientId"].Value.ToString();
                            emp.managerId = cmd.Parameters["@managerId"].Value.ToString();



                            var holidaysValue = cmd.Parameters["@holidays"].Value;
                            if (holidaysValue == DBNull.Value)
                            {
                                emp.holidays = new object[] { };
                            }
                            else
                            {
                                var holidaysString = (string)cmd.Parameters["@holidays"].Value;
                                var holidaysArray = holidaysString.Split(',');

                                var formattedHolidays = holidaysArray.Select(holiday =>
                                {
                                    var parts = holiday.Split(':');
                                    // Parse the date part to DateTime
                                    DateTime date;
                                    if (!DateTime.TryParse(parts[0].Trim(), out date))
                                    {
                                        // Handle parsing failure
                                        throw new ArgumentException("Invalid date format");
                                    }
                                    var description = parts[1].Trim();
                                    return new { Date = date, Description = description };
                                });

                                emp.holidays = formattedHolidays.ToArray();

                            }


                            var projectListValue = cmd.Parameters["@projectList"].Value;
                            if (projectListValue == DBNull.Value)
                            {
                                emp.projectList = new object[] { };
                            }
                            else
                            {
                                var projectListString = (string)cmd.Parameters["@projectList"].Value;
                                var projectsArray = projectListString.Split(',');

                                var formattedProjects = projectsArray.Select(project =>
                                {
                                    var parts = project.Split(':');
                                    var projectName = parts[0].Trim();
                                    var projectId = Guid.Parse(parts[1].Trim()); // Assuming project ID is a Guid

                                    return new { ProjectName = projectName, ProjectId = projectId };
                                });

                                emp.projectList = formattedProjects.ToArray();

                            }


                            var approvalLevelsValue = cmd.Parameters["@approvalLevels"].Value;
                            if (approvalLevelsValue == DBNull.Value)
                            {
                                emp.approvalLevels = new object[] { };
                            }
                            else
                            {
                                var approvalLevelsString = (string)cmd.Parameters["@approvalLevels"].Value;
                                var approvalLevelsArray = approvalLevelsString.Split(',');
                                var formattedApprovalLevels = approvalLevelsArray.Select(ApprovalLevels =>
                                {
                                    var parts = ApprovalLevels.Split(':');
                                    //var empId = parts[0].Trim();
                                    var empEmail = parts[0].Trim();
                                    var empName = parts[1].Trim();
                                    var jobDes = parts[2].Trim();
                                    var empId = parts[3].Trim();
                                    var approvalType = parts[4].Trim(); // Corrected index here
                                    var approvalLevel = parts[5].Trim(); // Assuming project ID is a Guid, corrected index

                                    return new { empId = empId, empEmail = empEmail, empName = empName, jobDes = jobDes, approvalType = approvalType, approvalLevel = approvalLevel };
                                });

                                emp.approvalLevels = formattedApprovalLevels.ToArray();

                            }


                            // Now you can use formattedProjects array
                            // For example, if emp.holidays is an array of Project objects


                            //emp.leaves = "[" + cmd.Parameters["@leaves"].Value.ToString() + "]";
                        }

                        // 2024-02-27 new implement (End)



                        var companyLocationsListValue = cmd.Parameters["@companyLocations"].Value;
                        if (companyLocationsListValue == DBNull.Value)
                        {
                            emp.companyLocations = new object[] { };
                        }
                        else
                        {

                            var companyLocationsListString = (string)cmd.Parameters["@companyLocations"].Value;

                            if (!string.IsNullOrEmpty(companyLocationsListString))
                            {
                                var companyLocationsArray = companyLocationsListString.Split(',');

                                var formattedcompanyLocations = companyLocationsArray.Select(companyLocations =>
                                {
                                    var parts = companyLocations.Split(':');

                                    if (parts.Length >= 5)
                                    {
                                        var officeId = parts[0].Trim();
                                        var locationName = parts[1].Trim();
                                        var longitude = parts[2].Trim().AsDecimal();
                                        var latitude = parts[3].Trim().AsDecimal();
                                        var radiusMt = parts[4].Trim().AsDecimal();

                                        return new
                                        {
                                            officeId = officeId,
                                            locationName = locationName,
                                            longitude = longitude,
                                            latitude = latitude,
                                            radiusMt = radiusMt
                                        };
                                    }
                                    else
                                    {
                                        // Log or handle invalid input here
                                        return null;
                                    }

                                });
                                /// emp.companyLocations = formattedcompanyLocations.ToArray();
                                emp.companyLocations = formattedcompanyLocations.Where(loc => loc != null).ToArray();

                                // Now you can use formattedcompanyLocations for further processing
                            }
                            else
                            {

                                // Handle case where companyLocationsListString is null or empty
                            }



                            var homeLocationsListValue = cmd.Parameters["@homeLocations"].Value;
                            if (homeLocationsListValue == DBNull.Value)
                            {
                                emp.homeLocations = new object[] { };
                            }
                            else
                            {

                                var homeLocationsListString = (string)cmd.Parameters["@homeLocations"].Value;

                                if (!string.IsNullOrEmpty(homeLocationsListString))
                                {
                                    var homeLocationsArray = homeLocationsListString.Split(',');

                                    var formattedhomeLocations = homeLocationsArray.Select(companyLocation =>
                                    {
                                        var parts = companyLocation.Split(':');

                                        if (parts.Length >= 5)
                                        {
                                            var locationId = parts[0].Trim();
                                            var locationName = parts[1].Trim();
                                            var longitude = decimal.Parse(parts[2].Trim());
                                            var latitude = decimal.Parse(parts[3].Trim());
                                            var radiusMt = decimal.Parse(parts[4].Trim());

                                            return new
                                            {
                                                locationId = locationId,
                                                locationName = locationName,
                                                longitude = longitude,
                                                latitude = latitude,
                                                radiusMt = radiusMt
                                            };
                                        }
                                        else
                                        {
                                            // Log or handle invalid input here
                                            return null;
                                        }
                                    }).Where(loc => loc != null).ToArray();

                                    // Now you can use formattedhomeLocations for further processing
                                    // emp.homeLocations = formattedhomeLocations.ToArray();
                                    emp.homeLocations = formattedhomeLocations;
                                }
                            }
                            // 2024-02-27 new implement (End)


                            return Ok(emp);
                        }
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

            // Add a default return statement outside of all conditional blocks
            return BadRequest("Invalid request"); // Or any appropriate response
        }
    }
}
