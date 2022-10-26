using PAY_ROLL_MANAGEMENT.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAY_ROLL_MANAGEMENT.dBHandler
{
    
    public class EmployeeLeavedBHandler
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

        //Add a Leave Request//
        
        public bool AddLeaveRequest(EmployeeLeave request)
        {
            request.RequestId = $"REQ000{GetLeaveId()}";
            connection();
            SqlCommand cmd = new SqlCommand("AddRequest", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("Id", request.Id);
            cmd.Parameters.AddWithValue("EmployeeId", request.EmployeeId);
            cmd.Parameters.AddWithValue("FullName", request.FullName);
            cmd.Parameters.AddWithValue("RequestId", request.RequestId);
            cmd.Parameters.AddWithValue("ReasonforLeave", request.ReasonforLeave);
            cmd.Parameters.AddWithValue("StartDate", request.StartDate);
            cmd.Parameters.AddWithValue("EndDate", request.EndDate);
            cmd.Parameters.AddWithValue("NoofDays", request.NoofDays);
           
            connectionManage();
            int i = cmd.ExecuteNonQuery();
            connectionManage();



            if (i >= 1)
                return true;
            else
                return false;
        

        }

        //GetLeaveId//

        private int GetLeaveId()
        {
            int tempInt = 0;
            connection();
            SqlCommand cmd = new SqlCommand("GetLeaveId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            connectionManage();
            int i = cmd.ExecuteNonQuery();
            connectionManage();
            tempInt = Convert.ToInt32(cmd.Parameters["@Id"].Value);
            return tempInt;



        }

        //Get Leave Request//
        
        public List<EmployeeLeave> GetLeaveRequest()
        {
            connection();
            List<EmployeeLeave> employees = new List<EmployeeLeave>();

            SqlCommand cmd = new SqlCommand("GetLeaveRequest", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            connectionManage();
            sd.Fill(dt);
            connectionManage();

            foreach (DataRow dr in dt.Rows)
            {
                employees.Add(
                    new EmployeeLeave
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        EmployeeId = Convert.ToString(dr["EmployeeId"]),
                        FullName = Convert.ToString(dr["FullName"]),
                        RequestId= Convert.ToString(dr["RequestId"]),
                        ReasonforLeave = Convert.ToString(dr["ReasonforLeave"]),
                        StartDate = Convert.ToDateTime(dr["StartDate"]),
                        EndDate = Convert.ToDateTime(dr["EndDate"]),
                        NoofDays = Convert.ToInt32(dr["NoofDays"])


                    });
            }

            dt.Dispose();
            return employees;

        }

        //**************** Delete Employee *********************
        
        public bool DeleteLeaveRequest(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteLeaveRequest", con);
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

        //**************** Update Employee *********************
        
        public EmployeeLeave GetByLeaveId(int requestId)
        {
            connection();
            EmployeeLeave employee = new EmployeeLeave();



            SqlCommand cmd = new SqlCommand("GetByLeaveId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@Id", requestId);
            DataTable dt = new DataTable();
            connectionManage();
            sd.Fill(dt);
            connectionManage();
            if (dt.Rows.Count != 0)
            {
                employee.Id = Convert.ToInt32(dt.Rows[0]["Id"]);

                employee.EmployeeId = Convert.ToString(dt.Rows[0]["EmployeeId"]);
                employee.FullName = Convert.ToString(dt.Rows[0]["FullName"]);
                employee.RequestId = Convert.ToString(dt.Rows[0]["RequestId"]);
                employee.ReasonforLeave = Convert.ToString(dt.Rows[0]["ReasonforLeave"]);
                employee.StartDate = Convert.ToDateTime(dt.Rows[0]["StartDate"]);
                employee.EndDate = Convert.ToDateTime(dt.Rows[0]["EndDate"]);
                employee.NoofDays = Convert.ToInt32(dt.Rows[0]["NoofDays"]);

                dt.Dispose();
                return employee;
            }
            dt.Dispose();
            return null;



        }
        public bool UpdateLeave(EmployeeLeave employee)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateLeaveRequest", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("Id", employee.Id);
            cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
            cmd.Parameters.AddWithValue("@FullName", employee.FullName);
            cmd.Parameters.AddWithValue("@RequestId", employee.RequestId);
            cmd.Parameters.AddWithValue("@ReasonforLeave", employee.ReasonforLeave);
            cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", employee.EndDate);
            cmd.Parameters.AddWithValue("@NoofDays", employee.NoofDays);
     

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        //Get Leave data with respect to Logged User//
        
        public List<EmployeeLeave> UserLeaveRequest(string requestId)
        {
            connection();
            List<EmployeeLeave> employees = new List<EmployeeLeave>();

            SqlCommand cmd = new SqlCommand("LeaveUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@EmployeeId", requestId);
            DataTable dt = new DataTable();
            connectionManage();
            sd.Fill(dt);
            connectionManage();

            foreach (DataRow dr in dt.Rows)
            {
                employees.Add(
                    new EmployeeLeave
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        EmployeeId = Convert.ToString(dr["EmployeeId"]),
                        FullName = Convert.ToString(dr["FullName"]),
                        RequestId = Convert.ToString(dr["RequestId"]),
                        ReasonforLeave = Convert.ToString(dr["ReasonforLeave"]),
                        StartDate = Convert.ToDateTime(dr["StartDate"]),
                        EndDate = Convert.ToDateTime(dr["EndDate"]),
                        NoofDays = Convert.ToInt32(dr["NoofDays"])


                    });
            }

            dt.Dispose();
            return employees;

        }

    }
}