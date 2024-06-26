﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ticktok_demo.Controllers
{
    public class TrackingControllerBulkController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString);

        [System.Web.Http.HttpPost]
        public IHttpActionResult InsertTrackingData([FromBody] JArray jsonData)
        {
            if (jsonData == null)
            {
                return BadRequest("No data provided.");
            }

            try
            {
                con.Open();

                string outputMessage = "";

                foreach (var item in jsonData)
                {
                    var json = item.ToString();
                    using (SqlCommand cmd = new SqlCommand("InsertTrackingDataNew", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@jsonData", json);

                        // Output parameter to capture stored procedure message
                        SqlParameter outputParameter = new SqlParameter();
                        outputParameter.ParameterName = "@outputMessage";
                        outputParameter.SqlDbType = SqlDbType.NVarChar;
                        outputParameter.Size = -1; // Set size accordingly
                        outputParameter.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParameter);

                        cmd.ExecuteNonQuery();

                        // Retrieve output message only if it's not already present in outputMessage
                        string storedProcedureMessage = outputParameter.Value.ToString();
                        if (!outputMessage.Contains(storedProcedureMessage))
                        {
                            outputMessage += storedProcedureMessage + "";
                        }
                    }
                }

                con.Close();

                return Ok(outputMessage);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }
}