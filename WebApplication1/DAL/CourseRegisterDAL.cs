using MySql.Data.MySqlClient;
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
        //    public static string s;
        //    public SqlConnection con;
        private MySqlConnection con = null;
        private MySqlConnectionStringBuilder sb = null;
        private MySqlCommand cmd = null;

        public CourseRegisterDAL()
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

        public void AddCourseRegister(int Id, int IdCours, int IdStudent)
        {

            string sqlString = "INSERT INTO CourseRegister (Id, IdCours, IdStudent" +
            ") VALUES (@val1, @val2, @val3);";
            using (MySqlCommand comm = new MySqlCommand())
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
            MySqlCommand com = new MySqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }


        public List<Course> getCoursesByIdStudent(int Id)
        {
            con.Open();
            string sqlString = "select c.Id, c.Name, c.LecturerID from CourseRegister cr, Course c where c.Id = cr.IdCours AND cr.IdStudent = " + Id + ";";
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

        public int maxIdCourseRegister()
        {
            con.Open();
            string sqlString = "select Max(Id) as Id from CourseRegister;";
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

        //delet all CourseRegister By IdCourse And id Student
        public void deleteCourseRegisterByIdCourseAndStudent(int IdCours, int IdStudent)
        {
            con.Open();
            string sqlString = @"DELETE FROM CourseRegister WHERE IdCours = " + IdCours + " AND IdStudent = " + IdStudent + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }
    }
}