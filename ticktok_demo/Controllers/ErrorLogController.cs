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
using System.Diagnostics;

namespace tiktocktest.Controllers
{
    public class ErrorLogController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString;

        [System.Web.Http.HttpPost]
        public IHttpActionResult InsertErrorLog(InsertErrorLog data)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("InsertErrorLog", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", data.UserId);
                    command.Parameters.AddWithValue("@StatusCode", data.StatusCode);
                    command.Parameters.AddWithValue("@APIName", data.APIName);
                    command.Parameters.AddWithValue("@Remark", data.Remark);

                    // Execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    // Log the number of rows affected
                    Debug.WriteLine($"Rows affected: {rowsAffected}");

                    if (rowsAffected > 0)
                    {
                        // Return the inserted data
                        return Ok(data);
                    }
                    else
                    {
                        return BadRequest("Insert failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Debug.WriteLine($"Exception: {ex.Message}");
                return InternalServerError(ex);
            }
        }
    }
}