using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                        Convert.ToInt32(rdr["IdStudent"]), Convert.ToDateTime(rdr["Date"]), Convert.ToInt32(rdr["YN"])));
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
    }
}