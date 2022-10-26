using Pay_Roll_Management.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PAY_ROLL_MANAGEMENT.dBHandler
{
    public class Signup
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

        public bool AddNewEmployee(Employee employee)
        {
            employee.EmployeeId = $"SA000{GetId()}";
            connection();
            SqlCommand cmd = new SqlCommand("AddEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.Parameters.AddWithValue("Id", employee.Id);
            cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
            cmd.Parameters.AddWithValue("@FullName", employee.FullName);
            cmd.Parameters.AddWithValue("@UserName", employee.UserName);
            cmd.Parameters.AddWithValue("@Password", employee.Password);
            cmd.Parameters.AddWithValue("@Gender", employee.Gender);
            cmd.Parameters.AddWithValue("@DateofBirth", employee.DateofBirth);
            cmd.Parameters.AddWithValue("@ContactNo", employee.ContactNo);
            cmd.Parameters.AddWithValue("@EmailId", employee.EmailId);
            cmd.Parameters.AddWithValue("@Designation", employee.Designation);
            cmd.Parameters.AddWithValue("@AllocatedLocation", employee.AllocatedLocation);
            cmd.Parameters.AddWithValue("@DateofJoining", employee.DateofJoining);
            cmd.Parameters.AddWithValue("@NameofManager", employee.NameofManager);
            cmd.Parameters.AddWithValue("@HomeAddress", employee.HomeAddress);
            connectionManage();
            int i = cmd.ExecuteNonQuery();
            connectionManage();



            if (i >= 1)
                return true;
            else
                return false;
        }
        private int GetId()
        {
            int tempInt = 0;
            connection();
            SqlCommand cmd = new SqlCommand("GetId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            connectionManage();
            int i = cmd.ExecuteNonQuery();
            connectionManage();
            tempInt = Convert.ToInt32(cmd.Parameters["@Id"].Value);
            return tempInt;



        }

    }
}