using PAY_ROLL_MANAGEMENT.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PAY_ROLL_MANAGEMENT.dBHandler
{
    public class SalarydBHandler
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

        // Add a Salary Report//

        public bool AddSalaryReport(Salary report)
        {
            
            connection();
            SqlCommand cmd = new SqlCommand("AddSalaryReport", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("Id", report.Id);
            cmd.Parameters.AddWithValue("EmployeeId", report.EmployeeId);
            cmd.Parameters.AddWithValue("FullName", report.FullName);
            cmd.Parameters.AddWithValue("Designation", report.Designation);
            cmd.Parameters.AddWithValue("BasicSalary", report.BasicSalary);
            cmd.Parameters.AddWithValue("Allowances", report.Allowances);
            cmd.Parameters.AddWithValue("Deduction", report.Deduction);
            cmd.Parameters.AddWithValue("NetSalary", report.NetSalary);
            cmd.Parameters.AddWithValue("ApprovedBy", report.ApprovedBy);

            connectionManage();
            int i = cmd.ExecuteNonQuery();
            connectionManage();



            if (i >= 1)
                return true;
            else
                return false;


        }

        //Get SalaryId//

        private int GetSalaryId()
        {
            int tempInt = 0;
            connection();
            SqlCommand cmd = new SqlCommand("GetSalaryId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            connectionManage();
            int i = cmd.ExecuteNonQuery();
            connectionManage();
            tempInt = Convert.ToInt32(cmd.Parameters["@Id"].Value);
            return tempInt;

        }

        //Get Salary Reports//

        public List<Salary> GetSalaryReport()
        {
            connection();
            List<Salary> employees = new List<Salary>();

            SqlCommand cmd = new SqlCommand("GetSalaryReport", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            connectionManage();
            sd.Fill(dt);
            connectionManage();

            foreach (DataRow dr in dt.Rows)
            {
                employees.Add(
                    new Salary
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        EmployeeId = Convert.ToString(dr["EmployeeId"]),
                        FullName = Convert.ToString(dr["FullName"]),
                        Designation= Convert.ToString(dr["Designation"]),
                        BasicSalary= Convert.ToString(dr["BasicSalary"]),
                        Allowances= Convert.ToString(dr["Allowances"]),
                        Deduction= Convert.ToString(dr["Deduction"]),
                        NetSalary= Convert.ToString(dr["NetSalary"]),
                        ApprovedBy= Convert.ToString(dr["ApprovedBy"])


                    });
            }

            dt.Dispose();
            return employees;

        }

        //Delete Salary Report//

        public bool DeleteSalaryReport(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteSalaryReport", con);
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

        //Update Salary Report//

        public Salary GetBySalaryId(int reportId)
        {
            connection();
            Salary employee = new Salary();



            SqlCommand cmd = new SqlCommand("GetBySalaryId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@Id", reportId);
            DataTable dt = new DataTable();
            connectionManage();
            sd.Fill(dt);
            connectionManage();
            if (dt.Rows.Count != 0)
            {
                employee.Id = Convert.ToInt32(dt.Rows[0]["Id"]);

                employee.EmployeeId = Convert.ToString(dt.Rows[0]["EmployeeId"]);
                employee.FullName = Convert.ToString(dt.Rows[0]["FullName"]);
                employee.Designation = Convert.ToString(dt.Rows[0]["Designation"]);
                employee.BasicSalary = Convert.ToString(dt.Rows[0]["BasicSalary"]);
                employee.Allowances = Convert.ToString(dt.Rows[0]["Allowances"]);
                employee.Deduction = Convert.ToString(dt.Rows[0]["Deduction"]);
                employee.NetSalary = Convert.ToString(dt.Rows[0]["NetSalary"]);
                employee.ApprovedBy = Convert.ToString(dt.Rows[0]["ApprovedBy"]);

                dt.Dispose();
                return employee;
            }
            dt.Dispose();
            return null;



        }
        public bool UpdateSalary(Salary employee)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateSalaryReport", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("Id", employee.Id);
            cmd.Parameters.AddWithValue("EmployeeId", employee.EmployeeId);
            cmd.Parameters.AddWithValue("FullName", employee.FullName);
            cmd.Parameters.AddWithValue("Designation", employee.Designation);
            cmd.Parameters.AddWithValue("BasicSalary", employee.BasicSalary);
            cmd.Parameters.AddWithValue("Allowances", employee.Allowances);
            cmd.Parameters.AddWithValue("Deduction", employee.Deduction);
            cmd.Parameters.AddWithValue("NetSalary", employee.NetSalary);
            cmd.Parameters.AddWithValue("ApprovedBy", employee.ApprovedBy);


            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        //Get Salary of Logged User//

        public List<Salary> UserSalaryReport(string reportId)
        {
            connection();
            List<Salary> employee = new List<Salary>();

            SqlCommand cmd = new SqlCommand("SalaryUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@EmployeeId", reportId);
            DataTable dt = new DataTable();
            connectionManage();
            sd.Fill(dt);
            connectionManage();

            foreach (DataRow dr in dt.Rows)
            {
                employee.Add(
                    new Salary
                    {

                     Id = Convert.ToInt32(dr["Id"]),
                     EmployeeId = Convert.ToString(dr["EmployeeId"]),
                     FullName = Convert.ToString(dr["FullName"]),
                     Designation = Convert.ToString(dr["Designation"]),
                     BasicSalary = Convert.ToString(dr["BasicSalary"]),
                     Allowances = Convert.ToString(dr["Allowances"]),
                     Deduction = Convert.ToString(dr["Deduction"]),
                     NetSalary = Convert.ToString(dr["NetSalary"]),
                     ApprovedBy = Convert.ToString(dr["ApprovedBy"])



                    });
            }

            dt.Dispose();
            return employee;

        }

    }
}