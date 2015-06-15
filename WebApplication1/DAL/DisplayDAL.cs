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
    public class DisplayDAL
    {

        private MySqlConnection con = null;
        private MySqlConnectionStringBuilder sb = null;
        private MySqlCommand cmd = null;

        public DisplayDAL()
        {

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

        public void AddNewDisplay(int Id, int IdQuestion, int IdQuestionnaire)
        {
            String sqlString = "INSERT INTO Display (Id, IdQuestion, IdQuestionnaire" +
            ") VALUES (@val1, @val2, @val3);";
            using (MySqlCommand comm = new MySqlCommand())
            {
                comm.Connection = con;
                comm.CommandText = sqlString;
                comm.Parameters.AddWithValue("@val1", Id);
                comm.Parameters.AddWithValue("@val2", IdQuestion);
                comm.Parameters.AddWithValue("@val3", IdQuestionnaire);


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
        public void deleteDisplayByIdQuestion(int IdQuestion)
        {

            con.Open();
            string sqlString = @"DELETE FROM Display WHERE IdQuestion = " + IdQuestion + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        public void deleteDisplayByIdQuestionnaire(int IdQuestionnaire)
        {

            con.Open();
            string sqlString = @"DELETE FROM Display WHERE IdQuestionnaire = " + IdQuestionnaire + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        public int maxIdDisplay()
        {

            con.Open();
            string sqlString = "select Max(Id) as Id from Display;";
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

        //public List<Display> getDisplayByIdQuestion(int IdQuestion)
        //{

        //    con.Open();
        //    string sqlString = "select * from Display where IdQuestion = " + IdQuestion + ";";
        //    MySqlCommand com = new MySqlCommand(sqlString, con);
        //    List<Display> listDisplayQuestion = new List<Display>();
        //    using (MySqlDataReader rdr = com.ExecuteReader())
        //    {
        //        while (rdr.Read())
        //        {
        //            listDisplayQuestion.Add(new Display(Convert.ToInt32(rdr["Id"]), Convert.ToInt32(rdr["IdQuestion"])
        //                , Convert.ToInt32(rdr["IdQuestionnnaire"])));
        //        }
        //    }
        //    con.Close();
        //    return listDisplayQuestion;

        //}

        public List<Display> getDisplayByQuestion()
        {

            con.Open();
            string sqlString = "select * from Display where IdQuestionnaire = 0;";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Display> listDisplayQuestion = new List<Display>();
            using (MySqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listDisplayQuestion.Add(new Display(Convert.ToInt32(rdr["Id"]), Convert.ToInt32(rdr["IdQuestion"])
                        , Convert.ToInt32(rdr["IdQuestionnaire"])));
                }
            }
            con.Close();
            return listDisplayQuestion;

        }

        //public List<Display> getDisplayByIdQuestionnnaire(int IdQuestionnnaire)
        //{

        //    con.Open();
        //    string sqlString = "select * from Display where IdQuestionnnaire = " + IdQuestionnnaire + ";";
        //    MySqlCommand com = new MySqlCommand(sqlString, con);
        //    List<Display> listDisplayQuestion = new List<Display>();
        //    using (MySqlDataReader rdr = com.ExecuteReader())
        //    {
        //        while (rdr.Read())
        //        {
        //            listDisplayQuestion.Add(new Display(Convert.ToInt32(rdr["Id"]), Convert.ToInt32(rdr["IdQuestion"])
        //                , Convert.ToInt32(rdr["IdQuestionnnaire"])));
        //        }
        //    }
        //    con.Close();
        //    return listDisplayQuestion;

        //}

        public List<Display> getDisplayByQuestionnaire()
        {

            con.Open();
            string sqlString = "select * from Display where IdQuestion = 0;";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Display> listDisplayQuestion = new List<Display>();
            using (MySqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listDisplayQuestion.Add(new Display(Convert.ToInt32(rdr["Id"]), Convert.ToInt32(rdr["IdQuestion"])
                        , Convert.ToInt32(rdr["IdQuestionnaire"])));
                }
            }
            con.Close();
            return listDisplayQuestion;

        }
    }
}