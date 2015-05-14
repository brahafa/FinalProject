using Clicker.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Clicker.DAL
{
    public class CourseDAL
    {
        //public static string s;
        //public SqlConnection con;
        private MySqlConnection con = null;
        private MySqlConnectionStringBuilder sb = null;
        private MySqlCommand cmd = null;
        public CourseDAL()
        {
            //s = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            //con = new SqlConnection(s);
            sb = new MySqlConnectionStringBuilder();
            sb.Server = "1dca19b5-5ffe-4e06-a129-a4650083dc91.mysql.sequelizer.com";
            sb.UserID = "kybmpfzqrrkskryu";
            sb.Password = "GxNAiPtZ2pFVYEetBzQEDi6m56QvhNKkr8qk4NKSLjJuNhViLfpaazsyAAEk87Sn";
            sb.Database = "db1dca19b55ffe4e06a129a4650083dc91";
            sb.CharacterSet = "utf8";
            try
            {
                con = new MySqlConnection(sb.ToString());
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: {0}", e.ToString());
            }
        }

        public void AddCourse(int Id, String Name, int LecturerID)
        {

            string sqlString = "INSERT INTO Course (Id, Name, LecturerID) VALUES (@val1, @val2, @val3);";
            using (MySqlCommand comm = new MySqlCommand())
            {
                comm.Connection = con;
                comm.CommandText = sqlString;
                comm.Parameters.AddWithValue("@val1", Id);
                comm.Parameters.AddWithValue("@val2", Name);
                comm.Parameters.AddWithValue("@val3", LecturerID);

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
        public void deleteCoursesById(int Id)
        {
            con.Open();
            string sqlString = @"DELETE FROM Course WHERE Id = " + Id + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }
        public int maxIdCourse()
        {
            con.Open();
            string sqlString = "select Max(Id) as Id from Course;";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            int maxId = 0;
            using (MySqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    maxId = Convert.ToInt32(rdr["Id"]);
                }
            }

            con.Close();
            return maxId;
        }

        public List<Course> getCoursesByIdLecturer(int Id)
        {
            con.Open();
            string sqlString = "select * from Course c where c.LecturerID = " + Id + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Course> listCourse = new List<Course>();
            using (MySqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listCourse.Add(new Course(Convert.ToInt32(rdr["Id"]), rdr["Name"].ToString(), Convert.ToInt32(rdr["LecturerID"])));
                }
            }
            con.Close();
            return listCourse;
        }
        public String getNameById(int Id)
        {
            con.Open();
            string sqlString = "select c.Name from Course c where c.Id = " + Id + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            String nameCourse = "";
            using (MySqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    nameCourse = rdr["Name"].ToString();
                }
            }
            con.Close();
            return nameCourse;
        }
    }
}