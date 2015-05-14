using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Clicker.Classes;

namespace Clicker.DAL
{
    public class LecturerDAL
    {
        //    public static string s;
        //    public SqlConnection con;
        private MySqlConnection con = null;
        private MySqlConnectionStringBuilder sb = null;
        private MySqlCommand cmd = null;
        public LecturerDAL()
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
        public void UpdateLecturer(int id,string Name, string image, String password, String degree)
        {
            string sqlString = "UPDATE Lecturer SET Name = @val2, image=@val3, password=@val4 , degree=@val5  WHERE Id="+id+" ;";
            using (MySqlCommand comm = new MySqlCommand())
          {
              comm.Connection = con;
              comm.CommandText = sqlString;
              comm.Parameters.AddWithValue("@val2", Name);
              comm.Parameters.AddWithValue("@val3", image);
              comm.Parameters.AddWithValue("@val4", password);
              comm.Parameters.AddWithValue("@val5", degree);
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
        public void AddNewLecturer(int Id, string Name, string email, string image, String password, String degree)
        {

            string sqlString = "INSERT INTO Lecturer (Id, Name, email, image, password, degree) VALUES (@val1, @val2, @val3, @val4, @val5, @val6);";
            using (MySqlCommand comm = new MySqlCommand())
            {
                comm.Connection = con;
                comm.CommandText = sqlString;
                comm.Parameters.AddWithValue("@val1", Id);
                comm.Parameters.AddWithValue("@val2", Name);
                comm.Parameters.AddWithValue("@val3", email);
                comm.Parameters.AddWithValue("@val4", image);
                comm.Parameters.AddWithValue("@val5", password);
                comm.Parameters.AddWithValue("@val6", degree);


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

        public void deleteLecturerById(int Id)
        {
            con.Open();
            string sqlString = @"DELETE FROM Lecturer WHERE Id = " + Id + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        public int maxIdLecturer()
        {
            con.Open();
            string sqlString = "select Max(l.Id) as Id from Lecturer l;";
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
        public List<Lecturer> getLecturerByPassword(String password, String email)
        {
            con.Open();
            string sqlString = "select * from Lecturer where email='" + email + "' AND password='" + password + "';";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Lecturer> listLecturer = new List<Lecturer>();
            using (MySqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listLecturer.Add(new Lecturer(Convert.ToInt32(rdr["Id"]), rdr["Name"].ToString(), rdr["email"].ToString(), rdr["image"].ToString(), rdr["password"].ToString() , rdr["degree"].ToString()));
                }
            }
            con.Close();
            return listLecturer;
        }
        public List<Lecturer> getLecturerByEmail( String email)
        {
            con.Open();                                         
            string sqlString = "select * from Lecturer where email= '" + email + "';";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Lecturer> listLecturer = new List<Lecturer>();
            using (MySqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listLecturer.Add(new Lecturer(Convert.ToInt32(rdr["Id"]), rdr["Name"].ToString(), rdr["email"].ToString(), rdr["image"].ToString(), rdr["password"].ToString(), rdr["degree"].ToString()));
                }
            }
            con.Close();
            return listLecturer;
        }
    }
}