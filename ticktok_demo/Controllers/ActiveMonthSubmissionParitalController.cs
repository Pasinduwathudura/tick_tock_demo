using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.Http;
using System.Configuration;
using ticktok_demo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace tiktocktest.Controllers
{
    public class ActiveMonthSubmissionPartialController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString;

        [HttpPut]
        public IHttpActionResult ActiveMonthSubmission([FromBody] approveStatus data)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("approveStatusPartial", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Input parameters
                    command.Parameters.AddWithValue("@employeeId", data.employeeId);
                    command.Parameters.AddWithValue("@trackingDate", data.trackingDate);
                    command.Parameters.AddWithValue("@approvalStatus", data.approvalStatus);


                    // Output parameters
                    command.Parameters.Add("@employeeName", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@managerEmail", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@employeeNo", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@monthName", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@trackingSheetData", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();
                    // Extract year from tracking date
                    DateTime trackingDate = Convert.ToDateTime(data.trackingDate);
                    string yearName = trackingDate.Year.ToString();

                    // Retrieve output parameters
                    string employeeName = Convert.ToString(command.Parameters["@employeeName"].Value);
                    string managerEmail = Convert.ToString(command.Parameters["@managerEmail"].Value);
                    string employeeNo = Convert.ToString(command.Parameters["@employeeNo"].Value);
                    string monthName = Convert.ToString(command.Parameters["@monthName"].Value);
                    string trackingSheetData = Convert.ToString(command.Parameters["@trackingSheetData"].Value);

                    string emailSubject = $"Approval Request: Partial Time Sheet Submission for {monthName} {yearName} [Employee ID: {employeeNo}] - {employeeName}";


                    // Configure Gmail SMTP settings
                    string smtpServer = "smtp.gmail.com";
                    int smtpPort = 587;
                    string smtpUsername = "pw4493074@gmail.com";
                    string smtpPassword = "rekd tywk paqw mtsc";

                    using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
                    {
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                        client.EnableSsl = true;

                        // Construct email body with tracking sheet data in a table for the primary recipient
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(smtpUsername, "TikTockUpdates");
                        mail.To.Add(managerEmail);
                        mail.Subject = emailSubject;
                        //mail.Subject = $"Approval Request Partial {employeeName} {monthName}";
                        string approvalUrl = $"https://ticktock-25b0d.web.app/requests/timesheets/{data.employeeId}";
                        mail.Body = $@"
                            <!DOCTYPE html>
                            <html>
                            <head>
                                <style>
                                    table {{
                                        border-collapse: collapse;
                                        width: 100%;
                                    }}
                                    th, td {{
                                        border: 1px solid #ddd;
                                        padding: 8px;
                                        text-align: left;
                                    }}
                                </style>
                            </head>
                            <body>
                                <p>Dear Manager,</p>
                                <p>The approval status for employee {employeeName} (Employee ID: {employeeNo}) for the month of {monthName} has been updated to: {data.approvalStatus}.</p>
                                {(string.IsNullOrEmpty(data.note) ? "" : $"<p><span style='color:blue;'>*Note: {data.note}</span></p>")}
                                <p>Please review and approve the submission by clicking the following link: <a href='{approvalUrl}'>{approvalUrl}</a></p>
                                <table>
                                    <tr>
                                        <th>DATE</th>
                                        <th>TYPE</th>
                                        <th>START TIME</th>
                                        <th>END TIME</th>
                                        <th>PROJECT</th>
                                        <th>APPROVE STATUS</th>
                                        
                                    </tr>";

                        // Add rows for tracking sheet data for the primary recipient
                        //var trackingSheetArray = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(trackingSheetData);
                        //foreach (var item in trackingSheetArray)

                        List<Dictionary<string, string>> trackingSheetArray = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(trackingSheetData);

                        trackingSheetArray = trackingSheetArray.OrderBy(item => DateTime.Parse(item["trackingDate"])).ToList();

                        foreach (var item in trackingSheetArray)

                        {
                            mail.Body += $@"
                                    <tr>
                                        <td>{DateTime.Parse(item["trackingDate"]).ToString("dd-MM-yyyy")}</td>
                                        <td>{item["dayType"]}</td>
                                        <td>{item["trackingStartTime"]}</td>
                                        <td>{item["trackingEndTime"]}</td>
                                        <td>{item["projectName"]}</td>
                                        <td>{item["approveStatus"]}</td>
                                    </tr>";
                        }

                        // Close the table and add closing HTML tags for the primary recipient
                        mail.Body += $@"
                                </table>
                                <p>Regards,</p>
                                <p><span style='color:red;'>This is an auto-generated email; please do not reply.</span></p>
                            </body>
                            </html>";
                        

                        mail.IsBodyHtml = true;

                        // Send primary email to manager
                        client.Send(mail);

                        // Create another email for CC recipients with a different format
                        MailMessage ccMail = new MailMessage();
                        ccMail.From = new MailAddress(smtpUsername, "TikTockUpdates");

                        // Add CC recipients
                        foreach (var ccEmail in data.ccEmails)
                        {
                            ccMail.To.Add(ccEmail);
                        }

                        // Construct email body for CC recipients
                        ccMail.Subject = emailSubject;
                        //ccMail.Subject = $"Approval Request Partial {employeeName} {monthName}";
                        ccMail.Body = $@"
                            <!DOCTYPE html>
                            <html>
                            <head>
                                <style>
                                    table {{
                                        border-collapse: collapse;
                                        width: 100%;
                                    }}
                                    th, td {{
                                        border: 1px solid #ddd;
                                        padding: 8px;
                                        text-align: left;
                                    }}
                                </style>
                            </head>
                            <body>
                            <p>Dear All,</p>
                            <p>The approval status for employee {employeeName} (Employee ID: {employeeNo}) for the month of {monthName} has been updated to: {data.approvalStatus}.</p>
                            {(string.IsNullOrEmpty(data.note) ? "" : $"<p><span style='color:blue;'>*Note: {data.note}</span></p>")}
                            <table>
                                <tr>
                                        <th>DATE</th>
                                        <th>TYPE</th>
                                        <th>START TIME</th>
                                        <th>END TIME</th>
                                        <th>PROJECT</th>
                                        <th>APPROVE STATUS</th>
                                </tr>";

                        // Add rows for tracking sheet data for the CC recipients
                        foreach (var item in trackingSheetArray)
                        {
                            ccMail.Body += $@"
                                    <tr>
                                        <td>{DateTime.Parse(item["trackingDate"]).ToString("dd-MM-yyyy")}</td>
                                        <td>{item["dayType"]}</td>
                                        <td>{item["trackingStartTime"]}</td>
                                        <td>{item["trackingEndTime"]}</td>
                                        <td>{item["projectName"]}</td>
                                        <td>{item["approveStatus"]}</td>
                                    </tr>";
                        }

                        // Close the table and add closing HTML tags for the CC recipients
                        ccMail.Body += $@"
                            </table>
                            <p>Regards,</p>
                            <p><span style='color:red;'>This is an auto-generated email; please do not reply.</span></p>";

                        ccMail.IsBodyHtml = true;

                        // Send CC email to additional recipients
                        client.Send(ccMail);
                    }

                    return Ok("success");
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
        }
    }
}
