using Pay_Roll_Management.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Pay_Roll_Management.dBHandler
{
    public class EmployeedBHandler
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




        // **************** ADD NEW Employee *********************//
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

        // **************** Get Employees *********************

        public List<Employee> GetEmployees()
        {
            connection();
            List<Employee> employees = new List<Employee>();

            SqlCommand cmd = new SqlCommand("GetEmployees", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            connectionManage();
            sd.Fill(dt);
            connectionManage();

            foreach (DataRow dr in dt.Rows)
            {
                employees.Add(
                    new Employee
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        EmployeeId = Convert.ToString(dr["EmployeeId"]),
                        FullName = Convert.ToString(dr["FullName"]),
                        Gender = Convert.ToString(dr["Gender"]),
                        DateofBirth = Convert.ToDateTime(dr["DateofBirth"]),
                        ContactNo = Convert.ToString(dr["ContactNo"]),
                        EmailId = Convert.ToString(dr["EmailId"]),
                        Designation = Convert.ToString(dr["Designation"]),
                        AllocatedLocation = Convert.ToString(dr["AllocatedLocation"]),
                        DateofJoining = Convert.ToDateTime(dr["DateofJoining"]),
                        NameofManager = Convert.ToString(dr["NameofManager"]),
                        HomeAddress = Convert.ToString(dr["HomeAddress"]),
                        Admin = Convert.ToBoolean(dr["Admin"])
                    });
            }

            dt.Dispose();
            return employees;

        }

        //**************** Delete Employee *********************

        public bool DeleteEmployee(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

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

        //**************** Update Employee *********************
        public Employee GetById(int employeeId)
        {
            connection();
            Employee employee = new Employee();



            SqlCommand cmd = new SqlCommand("GetById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@Id", employeeId);
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
                
                

                dt.Dispose();
                return employee;
            }
            dt.Dispose();
            return null;



        }
        public bool UpdateDetails(Employee employee)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("Id", employee.Id);
            cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
            cmd.Parameters.AddWithValue("@FullName", employee.FullName);
            cmd.Parameters.AddWithValue("@Gender", employee.Gender);
            cmd.Parameters.AddWithValue("@DateofBirth", employee.DateofBirth);
            cmd.Parameters.AddWithValue("@ContactNo", employee.ContactNo);
            cmd.Parameters.AddWithValue("@EmailId", employee.EmailId);
            cmd.Parameters.AddWithValue("@Designation", employee.Designation);
            cmd.Parameters.AddWithValue("@AllocatedLocation", employee.AllocatedLocation);
            cmd.Parameters.AddWithValue("@DateofJoining", employee.DateofJoining);
            cmd.Parameters.AddWithValue("@NameofManager", employee.NameofManager);
            cmd.Parameters.AddWithValue("@HomeAddress", employee.HomeAddress);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        //Get details of Employee with respect to EmployeeId//

        public List<Employee> UserEmployees( string Employee)
        {
            connection();
            List<Employee> employees = new List<Employee>();

            SqlCommand cmd = new SqlCommand("UserEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@EmployeeId", Employee);
            DataTable dt = new DataTable();
            connectionManage();
            sd.Fill(dt);
            connectionManage();

            foreach (DataRow dr in dt.Rows)
            {
                employees.Add(
                    new Employee
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        EmployeeId = Convert.ToString(dr["EmployeeId"]),
                        FullName = Convert.ToString(dr["FullName"]),
                        Gender = Convert.ToString(dr["Gender"]),
                        DateofBirth = Convert.ToDateTime(dr["DateofBirth"]),
                        ContactNo = Convert.ToString(dr["ContactNo"]),
                        EmailId = Convert.ToString(dr["EmailId"]),
                        Designation = Convert.ToString(dr["Designation"]),
                        AllocatedLocation = Convert.ToString(dr["AllocatedLocation"]),
                        DateofJoining = Convert.ToDateTime(dr["DateofJoining"]),
                        NameofManager = Convert.ToString(dr["NameofManager"]),
                        HomeAddress = Convert.ToString(dr["HomeAddress"]),
                        Admin = Convert.ToBoolean(dr["Admin"])

                    });
            }

            dt.Dispose();
            return employees;

         


        }

        


    }
}