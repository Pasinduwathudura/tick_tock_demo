using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using ticktok_demo.Models;

namespace ticktok_demo.Controllers
{
    [RoutePrefix("getSubmittedEntiesByEmpId")]
    public class GetSubmittedEntriesByEmpIdController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString;

        [HttpGet]
        public IHttpActionResult GetSubmittedEntriesByEmpId(Guid employeeId, Guid managerId, int? year, int? month, string approvalStatus)
        {
            try
            {
                if (employeeId == Guid.Empty || managerId == Guid.Empty)
                {
                    return BadRequest("EmployeeId and ManagerId are required.");
                }

                getSubmitted trc = new getSubmitted(); // Declaration and initialization of Tracking object

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("getSubmittedEntiesByEmpId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@employeeId", employeeId);
                        cmd.Parameters.AddWithValue("@managerId", managerId);
                        cmd.Parameters.AddWithValue("@year", year ?? 0);
                        cmd.Parameters.AddWithValue("@month", month ?? 0);
                        cmd.Parameters.AddWithValue("@approvalStatus", string.IsNullOrEmpty(approvalStatus) ? DBNull.Value : (object)approvalStatus);
                        //cmd.Parameters.AddWithValue("@approvalStatus", approvalStatus ?? 0);

                        // Output parameters
                        SqlParameter outputParameter1 = cmd.Parameters.Add("@outputResults", SqlDbType.NVarChar, -1);
                        outputParameter1.Direction = ParameterDirection.Output;

                        SqlParameter outputParameter2 = cmd.Parameters.Add("@employeeDetails", SqlDbType.NVarChar, -1);
                        outputParameter2.Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery(); // ExecuteNonQuery for stored procedure

                        // Retrieve output parameters
                        if (outputParameter1.Value != DBNull.Value)
                        {
                            string entriesString = outputParameter1.Value.ToString();
                            trc.trackingsheetList = ParseEntries(entriesString);
                        }

                        if (outputParameter2.Value != DBNull.Value)
                        {
                            string employeeDetailsString = outputParameter2.Value.ToString();
                            trc.employeeDetails = ParseEmployeeDetails(employeeDetailsString);
                        }
                    }
                }

                return Ok(trc); // Return IHttpActionResult here
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Status = "Error",
                    Message = ex.Message
                };
                // Return error response
                return InternalServerError(new Exception("An unexpected error occurred.", ex));
            }
        }

        private List<object> ParseEntries(string entriesString)
        {
            // Parsing entriesString to extract tracking entries
            string[] entriesArray = entriesString.Split(new[] { "trackingId:", "employeeId:", "trackingStartTime:", "trackingEndTime:", "companyId:", 
                "projectId:", "trackingDate:", "dayType:", "timeEntryManagerId:", "trackingApproveStatus:", "timeEntryLogApprovalStatus:",
                "submittedDate:", "project_name:" }, StringSplitOptions.RemoveEmptyEntries);

            List<object> formattedEntries = new List<object>();

            for (int i = 0; i < entriesArray.Length; i += 13)
            {
                var entry = new
                {
                    trackingId = entriesArray[i].Trim(),
                    employeeId = entriesArray[i + 1].Trim(),
                    trackingStartTime = entriesArray[i + 2].Trim(),
                    trackingEndTime = entriesArray[i + 3].Trim(),
                    companyId = entriesArray[i + 4].Trim(),
                    projectId = entriesArray[i + 5].Trim(),
                    trackingDate = entriesArray[i + 6].Trim(),
                    dayType = entriesArray[i + 7].Trim(),
                    timeEntryManagerId = entriesArray[i + 8].Trim(),
                    trackingApproveStatus = entriesArray[i + 9].Trim(),
                    timeEntryLogApprovalStatus = entriesArray[i + 10].Trim(),
                    submittedDate = entriesArray[i + 11].Trim(),
                    projectName = entriesArray[i + 12].Trim().TrimEnd(',')
                };
                formattedEntries.Add(entry);
            }

            return formattedEntries;
        }

        private object ParseEmployeeDetails(string employeeDetailsString)
        {
            // Parsing employeeDetailsString to extract employee details
            string[] entriesArray = employeeDetailsString.Split(new[] { "emp_id:", "emp_first_name:", "emp_last_name:", "active_month:", "comp_id:", "company_name:",
                "holiday_id:", "user_id:", "job_desc_name:", "pic:", "employee_no:",
                "client_id:", "client_name:", "manager_id:", "manager_name:", "project_id:", "project_name:", "country_name:" }, StringSplitOptions.RemoveEmptyEntries);

            if (entriesArray.Length >= 18)
            {
                return new
                {
                    EmployeeId = entriesArray[0].Trim(),
                    EmployeeFirstName = entriesArray[1].Trim(),
                    EmployeeLastName = entriesArray[2].Trim(),
                    ActiveMonth = entriesArray[3].Trim(),
                    CompanyId = entriesArray[4].Trim(),
                    CompanyName = entriesArray[5].Trim(),
                    HolidayId = entriesArray[6].Trim(),
                    UserId = entriesArray[7].Trim(),
                    JobDescriptionName = entriesArray[8].Trim(),
                    Pic = entriesArray[9].Trim(),
                    EmployeeNo = entriesArray[10].Trim(),
                    ClientId = entriesArray[11].Trim(),
                    ClientName = entriesArray[12].Trim(),
                    ManagerId = entriesArray[13].Trim(),
                    ManagerName = entriesArray[14].Trim(),
                    ProjectId = entriesArray[15].Trim(),
                    ProjectName = entriesArray[16].Trim(),
                    CountryName = entriesArray[17].Trim().TrimEnd(',')
                };
            }
            else
            {
                return null;
            }
        }
    }
}
