using Clicker.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;


namespace Clicker.DAL
{
    public class AnswerDAL
    {
    //    public static string s;
    //    public SqlConnection con;
        private MySqlConnection con = null;
        private MySqlConnectionStringBuilder sb = null;
        private MySqlCommand cmd = null;
        public AnswerDAL()
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

        // add Answer
        public void AddAnswer(int Id, String Answer, int IdQuestion, int CorrectAnswer)
        {
            string sqlString = "INSERT INTO Answer (Id, Answer, IdQuestion, CorrectAnswer" +
            ") VALUES (@val1, @val2, @val3, @val4);";
            using (MySqlCommand comm = new MySqlCommand())
            {
                comm.Connection = con;
                comm.CommandText = sqlString;
                comm.Parameters.AddWithValue("@val1", Id);
                comm.Parameters.AddWithValue("@val2", Answer);
                comm.Parameters.AddWithValue("@val3", IdQuestion);
                comm.Parameters.AddWithValue("@val4", CorrectAnswer);

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

        // delete Answer by id
        public void deleteAnswer(int Id)
        {
            con.Open();
            string sqlString = @"DELETE FROM Answer WHERE Id = " + Id + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        // delete all Answer by IdQuestion
        public void deleteAnswerByIdQuestion(int IdQuestion)
        {
            con.Open();
            string sqlString = @"DELETE FROM Answer WHERE IdQuestion = " + IdQuestion + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        // get Answer by IdQuestion
        public List<Answer> getAllAnswerByIdQuestion(int IdQuestion)
        {
            con.Open();
            string sqlString = "select * from Answer a where a.IdQuestion = " + IdQuestion + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Answer> listAnswer = new List<Answer>();
            using (MySqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listAnswer.Add(new Answer(Convert.ToInt32(rdr["Id"]), rdr["Answer"].ToString(), Convert.ToInt32(rdr["IdQuestion"]), Convert.ToInt32(rdr["CorrectAnswer"])));
                }
            }
            con.Close();
            return listAnswer;
        }
        public int maxIdAnswer()
        {
            con.Open();
            string sqlString = "select Max(Id) as Id from Answer;";
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

        //delet all answer with same idQuestion



    }
}