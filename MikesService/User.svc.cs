using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MikesService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "User" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select User.svc or User.svc.cs at the Solution Explorer and start debugging.
    public class User : IUser
    {
        public List<Objects.Employee> GetEmployeeInfo(string firstName, string lastName)
        {
            List<Objects.Employee> employees = new List<Objects.Employee>();
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.MikesAppDB);
            SqlDataReader reader;
            SqlCommand cmd = new SqlCommand(Properties.Settings.Default.GetEmployeeInfo, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FirstName", (firstName != null) ? firstName : ""));
            cmd.Parameters.Add(new SqlParameter("@LastName", (lastName != null) ? lastName :  ""));
            connection.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Objects.Employee employee = new Objects.Employee();
                employee.FirstName = reader["FirstName"].ToString();
                employee.LastName = reader["LastName"].ToString();
                employee.Address = reader["Address"].ToString();
                employee.Phone = reader["Phone"].ToString();
                employee.Email = reader["Email"].ToString();
                employee.ID = reader["ID"].ToString();
                employees.Add(employee);
            }
            connection.Close();
            connection.Dispose();
            return employees;
        }

        public bool SaveEmployeeInfo(string firstName, string lastName, string address, string email, string phone)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.MikesAppDB);
            SqlDataReader reader;
            SqlCommand cmd = new SqlCommand(Properties.Settings.Default.SaveEmployeeInfo, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Phone", phone);
            connection.Open();
            reader = cmd.ExecuteReader();
            return false;
        }
    }
}
