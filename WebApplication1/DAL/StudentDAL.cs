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
    public class StudentDAL
    {
        //    public static string s;
        //    public SqlConnection con;
        private MySqlConnection con = null;
        private MySqlConnectionStringBuilder sb = null;
        private MySqlCommand cmd = null;
        public StudentDAL()
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
        public void AddNewStudent(int Id, String Name, String email, String image, String password)
        {

            string sqlString = "INSERT INTO Student (Id, Name, email, image, password) VALUES (@val1, @val2, @val3, @val4, @val5);";
            using (MySqlCommand comm = new MySqlCommand())
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
            MySqlCommand com = new MySqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        public int maxIdStudent()
        {
            con.Open();
            string sqlString = "select Max(Id) as Id from Student;";
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
        public List<Student> getStudentByPassword(String password, String email)
        {
            con.Open();
            string sqlString = "select * from Student where email='" + email + "' AND password='" + password + "';";

            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Student> listLecturer = new List<Student>();
            using (MySqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listLecturer.Add(new Student(Convert.ToInt32(rdr["Id"]), rdr["Name"].ToString(), rdr["email"].ToString(), rdr["image"].ToString(), rdr["password"].ToString()));
                }
            }
            con.Close();
            return listLecturer;
        }

        public List<Student> getStudentByEmail(String email)
        {
            con.Open();
            string sqlString = "select * from Student where email= '" + email + "';";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Student> listStudent = new List<Student>();
            using (MySqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listStudent.Add(new Student(Convert.ToInt32(rdr["Id"]), rdr["Name"].ToString(), rdr["email"].ToString(), rdr["image"].ToString(), rdr["password"].ToString()));
                }
            }
            con.Close();
            return listStudent;
        }

        public void UpdateStudent(int id, String Name, String image, String password)
        {
            string sqlString = "UPDATE Student SET Name = @val2, image=@val3, password=@val4   WHERE Id=" + id + " ;";
            using (MySqlCommand comm = new MySqlCommand())
            {
                comm.Connection = con;
                comm.CommandText = sqlString;
                comm.Parameters.AddWithValue("@val2", Name);
                comm.Parameters.AddWithValue("@val3", image);
                comm.Parameters.AddWithValue("@val4", password);

                try
                {
                    con.Open();
                    comm.ExecuteNonQuery();
                    con.Close();
                }


                catch (System.Data.SqlClient.SqlException sqlException)
                {
                    Debug.WriteLine(sqlException.Message);
                }

            }
        }
    }
}