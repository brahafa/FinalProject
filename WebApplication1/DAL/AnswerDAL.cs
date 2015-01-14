using WebApplication1.classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;


namespace WebApplication1.DAL
{
    public class AnswerDAL
    {
        public static string s;
        public SqlConnection con;
        public AnswerDAL()
        {
            s = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            con = new SqlConnection(s); 
        }

        // add Answer
        public void AddAnswer(int Id, String Answer, int IdQuestion, int CorrectAnswer)
        {

            string sqlString = "INSERT INTO Answer (Id, Answer, IdQuestion, CorrectAnswer" +
            ") VALUES (@val1, @val2, @val3, @val4);";
            using (SqlCommand comm = new SqlCommand())
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
            SqlCommand com = new SqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        // delete all Answer by IdQuestion
        public void deleteAnswerByIdQuestion(int IdQuestion)
        {
            con.Open();
            string sqlString = @"DELETE FROM Answer WHERE IdQuestion = " + IdQuestion + ";";
            SqlCommand com = new SqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        // get Answer by IdQuestion
        public List<Answer> getAllAnswerByIdQuestion(int IdQuestion)
        {
            con.Open();
            string sqlString = "select * from Answer a where a.IdQuestion = " + IdQuestion + ";";
            SqlCommand com = new SqlCommand(sqlString, con);
            List<Answer> listAnswer = new List<Answer>();
            using (SqlDataReader rdr = com.ExecuteReader())
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

        //delet all answer with same idQuestion



    }
}