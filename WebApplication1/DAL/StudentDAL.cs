using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebApplication1.classes;

namespace WebApplication1.DAL
{
    public class StudentDAL
    {
            public static string s;
        public SqlConnection con;
        public StudentDAL()
        {
            s = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            con = new SqlConnection(s); 
        }
        public void AddNewStudent(int Id, String Name, String email, String image, String password)
        {

            string sqlString = "INSERT INTO Student (Id, Name, email, image, password) VALUES (@val1, @val2, @val3, @val4, @val5);";
            using (SqlCommand comm = new SqlCommand())
            {
                comm.Connection = con;
                comm.CommandText = sqlString;
                comm.Parameters.AddWithValue("@val1", Id);
                comm.Parameters.AddWithValue("@val2", Name);
                comm.Parameters.AddWithValue("@val3", email);
                comm.Parameters.AddWithValue("@val4", image);
                comm.Parameters.AddWithValue("@val5", password);
                try
                {
                    con.Open();
                    comm.ExecuteNonQuery();
                    con.Close();
                }
                catch (SqlException e)
                {
                    Debug.WriteLine("error: " + e.Errors);
                }
            }

        }

        public void deleteStudentById(int Id)
        {
            con.Open();
            string sqlString = @"DELETE FROM Student WHERE Id = " + Id + ";";
            SqlCommand com = new SqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        public int maxIdStudent()
        {
            con.Open();
            string sqlString = "select Max(Id) as Id from Student;";
            SqlCommand com = new SqlCommand(sqlString, con);
            int maxId = 0;
            using (SqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    maxId = Convert.ToInt32(rdr["Id"]);
                }
            }

            con.Close();
            return maxId;
        }
        public List<Student> getStudentByPassword(String password, String email)
        {
            con.Open();
            string sqlString = "select * from Student where email='" + email + "' AND password='" + password + "';";

            SqlCommand com = new SqlCommand(sqlString, con);
            List<Student> listLecturer = new List<Student>();
            using (SqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listLecturer.Add(new Student(Convert.ToInt32(rdr["Id"]), rdr["Name"].ToString(), rdr["email"].ToString(), rdr["image"].ToString(), rdr["password"].ToString()));
                }
            }
            con.Close();
            return listLecturer;
        }

        public List<Student> getStudentByEmail(string email)
        {
            con.Open();
            string sqlString = "select * from Student where email= '" + email + "';";
            SqlCommand com = new SqlCommand(sqlString, con);
            List<Student> listStudent = new List<Student>();
            using (SqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listStudent.Add(new Student(Convert.ToInt32(rdr["Id"]), rdr["Name"].ToString(), rdr["email"].ToString(), rdr["image"].ToString(), rdr["password"].ToString()));
                }
            }
            con.Close();
            return listStudent;
        }
    }
}