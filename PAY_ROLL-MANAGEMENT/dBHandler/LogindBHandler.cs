using Pay_Roll_Management.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PAY_ROLL_MANAGEMENT.dBHandler
{
    public class LogindBHandler
    {
        private SqlConnection con;
        private void connection()
        {
            con = new SqlConnection("Data Source=.;Initial Catalog=Pay_Roll;Integrated Security=True");
        }
        private void connectionManage()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            else
                con.Open();
        }

        // Create a Login Data for user //

        public Employee GetByUserName(string UserName)
        {
            connection();
            Employee employee = new Employee();



            SqlCommand cmd = new SqlCommand("Login", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            DataTable dt = new DataTable();
            connectionManage();
            sd.Fill(dt);
            connectionManage();
            if (dt.Rows.Count != 0)
            {
                employee.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                employee.EmployeeId = Convert.ToString(dt.Rows[0]["EmployeeId"]);
                employee.FullName = Convert.ToString(dt.Rows[0]["FullName"]);
                employee.Gender = Convert.ToString(dt.Rows[0]["Gender"]);
                employee.DateofBirth = Convert.ToDateTime(dt.Rows[0]["DateofBirth"]);
                employee.ContactNo = Convert.ToString(dt.Rows[0]["ContactNo"]);
                employee.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                employee.Designation = Convert.ToString(dt.Rows[0]["Designation"]);
                employee.AllocatedLocation = Convert.ToString(dt.Rows[0]["AllocatedLocation"]);
                employee.DateofJoining = Convert.ToDateTime(dt.Rows[0]["DateofJoining"]);
                employee.NameofManager = Convert.ToString(dt.Rows[0]["NameofManager"]);
                employee.HomeAddress = Convert.ToString(dt.Rows[0]["HomeAddress"]);
                employee.Admin = Convert.ToBoolean(dt.Rows[0]["Admin"]);

                dt.Dispose();
                return employee;
            }
            dt.Dispose();
            return null;


        }
    }
}