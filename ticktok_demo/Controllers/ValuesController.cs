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

namespace ticktok_demo.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString);
        Dashboard emp = new Dashboard();
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5

        public Dashboard Get(Guid? id)
        {
            // Check if the ID is null or empty
            if (!id.HasValue || id == Guid.Empty)
            {
                // Return null if ID is invalid
                return null;
            }

            SqlCommand cmd = new SqlCommand("DashBoard", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empId", id);
            cmd.Parameters.Add("@workingDays", SqlDbType.Int).Direction = ParameterDirection.Output;
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

            con.Open();
           cmd.ExecuteNonQuery();
            con.Close();

            Dashboard emp = null;
            if (cmd.Parameters["@workingDays"].Value != DBNull.Value)
            {
                emp = new Dashboard();

                emp.empFirstName = cmd.Parameters["@empFirstName"].Value.ToString();
                emp.pic = cmd.Parameters["@pic"].Value.ToString();
                emp.employeeNo = cmd.Parameters["@employeeNo"].Value.ToString();
                emp.companyId = cmd.Parameters["@companyId"].Value.ToString();
                emp.jobName = cmd.Parameters["@jobName"].Value.ToString();
                emp.employeeId = cmd.Parameters["@employeeId"].Value.ToString();
                emp.countryName = cmd.Parameters["@countryName"].Value.ToString();
                emp.compName = cmd.Parameters["@compName"].Value.ToString();
                emp.projectName = cmd.Parameters["@projectName"].Value.ToString();
                //emp.holidays = "[" + cmd.Parameters["@holidays"].Value.ToString() + "]";
                //emp.holiDays = cmd.Parameters["@holiDays"].Value.ToString();
                // Assuming emp_id is of type Guid
                // emp.emp_id = id.GetValueOrDefault<Guid>();
                // Assuming cmd.Parameters["@holidays"].Value is a list or an array
                // Assuming cmd.Parameters["@holidays"].Value is a string containing comma-separated holiday names
                var holidaysString = (string)cmd.Parameters["@holidays"].Value;

                // Split the string into individual holiday names
                var holidaysArray = holidaysString.Split(',');

                // Convert the array to a list
                var holidaysList = new List<string>(holidaysArray);

                // Build a string representation of the list
                var holidaysResult = "[" + string.Join(",", holidaysList) + "]";

                // Assign the string representation to emp.holidays
                emp.holidays = holidaysResult;
                //emp.leaves = "[" + cmd.Parameters["@leaves"].Value.ToString() + "]";



            }

            return emp;
        }

        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
