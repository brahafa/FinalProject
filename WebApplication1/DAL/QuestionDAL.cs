using WebApplication1.classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;


namespace WebApplication1.DAL
{
    public class QuestionDAL
    {
            public static string s;
        public SqlConnection con;
        public QuestionDAL()
        {
            s = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            con = new SqlConnection(s); 
        }

        // add Question
        public void AddQuestion(int Id, String Question, int IdQuestionnaire, int Type, String File1)
        {

            String sqlString = "INSERT INTO Question (Id, Question, IdQuestionnaire, Type, File1" +
            ") VALUES (@val1, @val2, @val3, @val4, @val5);";
            using (SqlCommand comm = new SqlCommand())
            {
                comm.Connection = con;
                comm.CommandText = sqlString;
                comm.Parameters.AddWithValue("@val1", Id);
                comm.Parameters.AddWithValue("@val2", Question);
                comm.Parameters.AddWithValue("@val3", IdQuestionnaire);
                comm.Parameters.AddWithValue("@val4", Type);
                comm.Parameters.AddWithValue("@val5", File1);

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

        // delete Question
        public void deleteQuestion(int Id)
        {
            con.Open();
            string sqlString = @"DELETE FROM Question WHERE Id = " + Id + ";";
            SqlCommand com = new SqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        // delete all Question By Questionnaire
        public void deleteQuestionByQuestionnaire(int IdQuestionnaire)
        {
            con.Open();
            string sqlString = @"DELETE FROM Question WHERE IdQuestionnaire = " + IdQuestionnaire + ";";
            SqlCommand com = new SqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        //get All Question By Questionnaire
        public List<Question> getAllQuestionByQuestionnaire(int IdQuestionnaire)
        {
            con.Open();
            string sqlString = "select * from Question q where q.IdQuestionnaire = " + IdQuestionnaire + ";";
            SqlCommand com = new SqlCommand(sqlString, con);
            List<Question> listQuestion = new List<Question>();
            using (SqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listQuestion.Add(new Question(Convert.ToInt32(rdr["Id"]), rdr["Question"].ToString(), Convert.ToInt32(rdr["IdQuestionnaire"]),
                        Convert.ToInt32(rdr["Type"]), rdr["File"].ToString()));
                }
            }
            con.Close();
            return listQuestion;
        }

        //get All Question By Type
        public List<Question> getAllQuestionByType(int Type)
        {
            con.Open();
            string sqlString = "select * from Question q where q.Type = " + Type + ";";
            SqlCommand com = new SqlCommand(sqlString, con);
            List<Question> listQuestion = new List<Question>();
            using (SqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listQuestion.Add(new Question(Convert.ToInt32(rdr["Id"]), rdr["Question"].ToString(), Convert.ToInt32(rdr["IdQuestionnaire"]),
                        Convert.ToInt32(rdr["Type"]), rdr["File"].ToString()));
                }
            }
            con.Close();
            return listQuestion;
        }
        public int maxIdQuestion()
        {
            con.Open();
            string sqlString = "select Max(Id) as Id from Question;";
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
    }
}