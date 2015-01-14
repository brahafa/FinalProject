using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebApplication1.classes;


namespace WebApplication1.DAL
{
    public class CourseRegisterDAL
    {
        public static string s;
        public SqlConnection con;

        public CourseRegisterDAL()
        {
            s = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            con = new SqlConnection(s); 
        }

        public void AddCourseRegister(int Id, int IdCours, int IdStudent)
        {

            string sqlString = "INSERT INTO CourseRegister (Id, IdCours, IdStudent" +
            ") VALUES (@val1, @val2, @val3);";
            using (SqlCommand comm = new SqlCommand())
            {
                comm.Connection = con;
                comm.CommandText = sqlString;
                comm.Parameters.AddWithValue("@val1", Id);
                comm.Parameters.AddWithValue("@val2", IdCours);
                comm.Parameters.AddWithValue("@val3", IdStudent);

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

        //delet all CourseRegister By IdCourse
        public void deleteCourseRegisterByIdCourse(int IdCours)
        {
            con.Open();
            string sqlString = @"DELETE FROM CourseRegister WHERE IdCours = " + IdCours + ";";
            SqlCommand com = new SqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }


        public List<Course> getCoursesByIdStudent(int Id)
        {
            con.Open();
            string sqlString = "select c.Id, c.Name, c.LecturerID from CourseRegister cr, Course c where c.Id = cr.IdCours AND cr.IdStudent = " + Id + ";";
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

        public int maxIdCourseRegister()
        {
            con.Open();
            string sqlString = "select Max(Id) as Id from CourseRegister;";
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

        //delet all CourseRegister By IdCourse And id Student
        public void deleteCourseRegisterByIdCourseAndStudent(int IdCours, int IdStudent)
        {
            con.Open();
            string sqlString = @"DELETE FROM CourseRegister WHERE IdCours = " + IdCours + " AND IdStudent = " + IdStudent + ";";
            SqlCommand com = new SqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }
    }
}