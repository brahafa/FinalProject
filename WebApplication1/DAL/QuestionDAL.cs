﻿using Clicker.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;


namespace Clicker.DAL
{
    public class QuestionDAL
    {
        //    public static string s;
        //    public SqlConnection con;
        private MySqlConnection con = null;
        private MySqlConnectionStringBuilder sb = null;
        private MySqlCommand cmd = null;
        public QuestionDAL()
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

        // add Question
        public void AddQuestion(int Id, String Question, int IdQuestionnaire, int Type, String File1)
        {

            String sqlString = "INSERT INTO Question (Id, Question, IdQuestionnaire, Type, File1" +
            ") VALUES (@val1, @val2, @val3, @val4, @val5);";
            using (MySqlCommand comm = new MySqlCommand())
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
            MySqlCommand com = new MySqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        // delete all Question By Questionnaire
        public void deleteQuestionByQuestionnaire(int IdQuestionnaire)
        {
            con.Open();
            string sqlString = @"DELETE FROM Question WHERE IdQuestionnaire = " + IdQuestionnaire + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        //get All Question By Questionnaire
        public List<Question> getAllQuestionByQuestionnaire(int IdQuestionnaire)
        {
            con.Open();
            string sqlString = "select * from Question q where q.IdQuestionnaire = " + IdQuestionnaire + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Question> listQuestion = new List<Question>();
            using (MySqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listQuestion.Add(new Question(Convert.ToInt32(rdr["Id"]), rdr["Question"].ToString(), Convert.ToInt32(rdr["IdQuestionnaire"]),
                        Convert.ToInt32(rdr["Type"]), rdr["File1"].ToString()));
                }
            }
            con.Close();
            return listQuestion;
        }

        public List<Question> getAllQuestionByIDAndQuestionnaire(int IdQuestionnaire, int Id)
        {
            con.Open();
            string sqlString = "select * from Question q where q.IdQuestionnaire = " + IdQuestionnaire + " AND q.Id=" + Id + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Question> listQuestion = new List<Question>();
            using (MySqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listQuestion.Add(new Question(Convert.ToInt32(rdr["Id"]), rdr["Question"].ToString(), Convert.ToInt32(rdr["IdQuestionnaire"]),
                        Convert.ToInt32(rdr["Type"]), rdr["File1"].ToString()));
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
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Question> listQuestion = new List<Question>();
            using (MySqlDataReader rdr = com.ExecuteReader())
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

        public String getNameQuestById(int id)
        {
            con.Open();
            String sqlString = "select Question from Question q where q.Id = " + id + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            String nameQuest="";
            using (MySqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    nameQuest = (rdr["Question"].ToString());
                }
            }
            con.Close();



            return nameQuest;
        }
    }
}