using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebApplication1.classes;

namespace WebApplication1.DAL
{
    public class QuestionAskedDAL
    {
        public static string s;
        public SqlConnection con;
        public QuestionAskedDAL()
        {
            s = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            con = new SqlConnection(s); 
        }

        public void AddNewQuestionAsked(int Id, int IdQuestion, int IdStudent, String Date, int YN)
        {

            string sqlString = "INSERT INTO QuestionAsked (Id, IdQuestion, IdStudent, Date, YN) VALUES (@val1, @val2, @val3, @val4, @val5);";
            using (SqlCommand comm = new SqlCommand())
            {
                comm.Connection = con;
                comm.CommandText = sqlString;
                comm.Parameters.AddWithValue("@val1", Id);
                comm.Parameters.AddWithValue("@val2", IdQuestion);
                comm.Parameters.AddWithValue("@val3", IdStudent);
                comm.Parameters.AddWithValue("@val4", Date);
                comm.Parameters.AddWithValue("@val5", YN);


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


        // get QuestionAsked by IdQuestion
        public List<QuestionAsked> getAllQuestionAskedByIdQuestion(int IdQuestion)
        {
            con.Open();
            string sqlString = "select * from QuestionAsked q where q.IdQuestion = " + IdQuestion + ";";
            SqlCommand com = new SqlCommand(sqlString, con);
            List<QuestionAsked> listQuestionAsked = new List<QuestionAsked>();
            using (SqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listQuestionAsked.Add(new QuestionAsked(Convert.ToInt32(rdr["Id"]), Convert.ToInt32(rdr["IdQuestion"]),
                        Convert.ToInt32(rdr["IdStudent"]), (rdr["Date"]).ToString(), Convert.ToInt32(rdr["YN"])));
                }
            }
            con.Close();
            return listQuestionAsked;
        }

        //delet all QuestionAsked By Id Question
        public void deleteQuestionAskedByIdQuestion(int IdQuestion)
        {
            con.Open();
            string sqlString = @"DELETE FROM QuestionAsked WHERE IdQuestion = " + IdQuestion + ";";
            SqlCommand com = new SqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        public int maxIdquestionAsk()
        {
            con.Open();
            string sqlString = "select Max(l.Id) as Id from QuestionAsked l;";
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