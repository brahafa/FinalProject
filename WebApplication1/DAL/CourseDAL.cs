using FinalProject.classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebApplication1.classes;

namespace FinalProject.DAL
{
    public class CourseDAL
    {
        public static string s;
        public SqlConnection con;
        public CourseDAL()
        {
            s = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            con = new SqlConnection(s); 
        }

        public void AddCourse(int Id, String Name, int LecturerID)
        {

            string sqlString = "INSERT INTO Course (Id, Name, LecturerID) VALUES (@val1, @val2, @val3);";
            using (SqlCommand comm = new SqlCommand())
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
            SqlCommand com = new SqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }
        public int maxIdCourse()
        {
            con.Open();
            string sqlString = "select Max(Id) as Id from Course;";
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

        public List<Course> getCoursesByIdLecturer(int Id)
        {
            con.Open();
            string sqlString = "select * from Course c where c.LecturerID = " + Id + ";";
            SqlCommand com = new SqlCommand(sqlString, con);
            List<Course> listCourse = new List<Course>();
            using (SqlDataReader rdr = com.ExecuteReader())
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
            SqlCommand com = new SqlCommand(sqlString, con);
            String nameCourse = "";
            using (SqlDataReader rdr = com.ExecuteReader())
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