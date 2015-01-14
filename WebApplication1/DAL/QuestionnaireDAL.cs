using WebApplication1.classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;


namespace WebApplication1.DAL
{
    public class QuestionnaireDAL
    {
        public static string s;
        public SqlConnection con;
        public QuestionnaireDAL()
        {
            s = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            con = new SqlConnection(s);
        }

        // add Questionnaire
        public void AddQuestionnaire(int Id, String Name, int IdCours, int Permit)
        {

            string sqlString = "INSERT INTO Questionnaire (Id, Name, IdCours, Permit" +
            ") VALUES (@val1, @val2, @val3, @val4);";
            using (SqlCommand comm = new SqlCommand())
            {
                comm.Connection = con;
                comm.CommandText = sqlString;
                comm.Parameters.AddWithValue("@val1", Id);
                comm.Parameters.AddWithValue("@val2", Name);
                comm.Parameters.AddWithValue("@val3", IdCours);
                comm.Parameters.AddWithValue("@val4", Permit);

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

        // delete Questionnaire by id
        public void deleteQuestionnaire(int Id)
        {
            con.Open();
            string sqlString = @"DELETE FROM Questionnaire WHERE Id = " + Id + ";";
            SqlCommand com = new SqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        // delete all Questionnaire by IdCours
        public void deleteQuestionnaireByIdCours(int IdCours)
        {
            con.Open();
            string sqlString = @"DELETE FROM Questionnaire WHERE IdCours = " + IdCours + ";";
            SqlCommand com = new SqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        // get Questionnaire by Name(if permit=1)
        public List<Questionnaire> getAllQuestionnaireByName(String Name)
        {
            con.Open();
            string sqlString = "select * from Questionnaire q where q.Name = " + Name + " AND q.Permit = 1 ;";
            SqlCommand com = new SqlCommand(sqlString, con);
            List<Questionnaire> listQuestionnaire = new List<Questionnaire>();
            using (SqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listQuestionnaire.Add(new Questionnaire(Convert.ToInt32(rdr["Id"]), rdr["Name"].ToString(),
                        Convert.ToInt32(rdr["IdCours"]), Convert.ToInt32(rdr["Permit"])));
                }
            }
            con.Close();
            return listQuestionnaire;
        }

        // get Questionnaire by Lecturer(if permit=1)
        public List<Questionnaire> getAllQuestionnaireByLecturer(int IdLecturer)
        {
            con.Open();
            string sqlString = "select q.Id, q.Name, q.IdCours, q.Permit from Questionnaire q, Course c " +
                "where q.IdCours = c.Id AND c.LecturerID = " + IdLecturer + " AND q.Permit = 1 ;";
            SqlCommand com = new SqlCommand(sqlString, con);
            List<Questionnaire> listQuestionnaire = new List<Questionnaire>();
            using (SqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listQuestionnaire.Add(new Questionnaire(Convert.ToInt32(rdr["Id"]), rdr["Name"].ToString(),
                        Convert.ToInt32(rdr["IdCours"]), Convert.ToInt32(rdr["Permit"])));
                }
            }
            con.Close();
            return listQuestionnaire;
        }

        // get Questionnaire by IdCourse
        public List<Questionnaire> getAllQuestionnaireByIdCourse(int IdCourse)
        {
            con.Open();
            string sqlString = "select q.Id, q.Name, q.IdCours, q.Permit from Questionnaire q, Course c " +
                "where q.IdCours = c.Id AND c.Id = " + IdCourse + ";";
            SqlCommand com = new SqlCommand(sqlString, con);
            List<Questionnaire> listQuestionnaire = new List<Questionnaire>();
            using (SqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listQuestionnaire.Add(new Questionnaire(Convert.ToInt32(rdr["Id"]), rdr["Name"].ToString(),
                        Convert.ToInt32(rdr["IdCours"]), Convert.ToInt32(rdr["Permit"])));
                }
            }
            con.Close();
            return listQuestionnaire;
        }

        public int maxIdQuestionnaire()
        {
            con.Open();
            string sqlString = "select Max(Id) as Id from Questionnaire;";
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

        public List<Questionnaire> getAllQuestionnaireByIdCours(int IdCours)
        {
            con.Open();
            string sqlString = "select * from Questionnaire q where q.IdCours = " + IdCours + "AND q.Permit = 1 ;";
            SqlCommand com = new SqlCommand(sqlString, con);
            List<Questionnaire> listQuestionnaire = new List<Questionnaire>();
            using (SqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listQuestionnaire.Add(new Questionnaire(Convert.ToInt32(rdr["Id"]), rdr["Name"].ToString(),
                        Convert.ToInt32(rdr["IdCours"]), Convert.ToInt32(rdr["Permit"])));
                }
            }
            con.Close();
            return listQuestionnaire;
        }

        public List<Questionnaire> getAllQuestionnaireByPermit()
        {
            con.Open();
            string sqlString = "select * from Questionnaire q where q.Permit = 1 ;";
            SqlCommand com = new SqlCommand(sqlString, con);
            List<Questionnaire> listQuestionnaire = new List<Questionnaire>();
            using (SqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    listQuestionnaire.Add(new Questionnaire(Convert.ToInt32(rdr["Id"]), rdr["Name"].ToString(),
                        Convert.ToInt32(rdr["IdCours"]), Convert.ToInt32(rdr["Permit"])));
                }
            }
            con.Close();
            return listQuestionnaire;
        }
 
    }
}