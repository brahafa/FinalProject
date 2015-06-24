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
    public class QuestionnaireDAL
    {
        //    public static string s;
        //    public SqlConnection con;
        private MySqlConnection con = null;
        private MySqlConnectionStringBuilder sb = null;
        private MySqlCommand cmd = null;
        public QuestionnaireDAL()
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

        // add Questionnaire
        public void AddQuestionnaire(int Id, String Name, int IdCours, int Permit)
        {

            string sqlString = "INSERT INTO Questionnaire (Id, Name, IdCours, Permit" +
            ") VALUES (@val1, @val2, @val3, @val4);";
            using (MySqlCommand comm = new MySqlCommand())
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
            MySqlCommand com = new MySqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        // get Questionnaire by id
        public Questionnaire getQuestionnaireById(int IdQuestionnaire)
        {

            con.Open();
            string sqlString = "select * from Questionnaire q where q.Id = " + IdQuestionnaire + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            Questionnaire questionnaire = null;
            using (MySqlDataReader rdr = com.ExecuteReader())
            {
                while (rdr.Read())
                {
                    questionnaire = new Questionnaire(Convert.ToInt32(rdr["Id"]), rdr["Name"].ToString(),
                        Convert.ToInt32(rdr["IdCours"]), Convert.ToInt32(rdr["Permit"]));
                }
            }
            con.Close();
            return questionnaire;
        }

        // delete all Questionnaire by IdCours
        public void deleteQuestionnaireByIdCours(int IdCours)
        {
            con.Open();
            string sqlString = @"DELETE FROM Questionnaire WHERE IdCours = " + IdCours + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            com.ExecuteNonQuery();
            con.Close();

        }

        // get Questionnaire by Name(if permit=1)
        public List<Questionnaire> getAllQuestionnaireByName(String Name)
        {
            con.Open();
            string sqlString = "select * from Questionnaire q where q.Name = " + Name + " AND q.Permit = 1 ;";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Questionnaire> listQuestionnaire = new List<Questionnaire>();
            using (MySqlDataReader rdr = com.ExecuteReader())
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
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Questionnaire> listQuestionnaire = new List<Questionnaire>();
            using (MySqlDataReader rdr = com.ExecuteReader())
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
            string sqlString = "select * from Questionnaire q where q.IdCours = " + IdCourse + ";";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Questionnaire> listQuestionnaire = new List<Questionnaire>();
            using (MySqlDataReader rdr = com.ExecuteReader())
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

        public List<Questionnaire> getAllQuestionnaireByIdCours(int IdCours)
        {
            con.Open();
            string sqlString = "select * from Questionnaire q where q.IdCours = " + IdCours + " AND q.Permit ="+1+" ;";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Questionnaire> listQuestionnaire = new List<Questionnaire>();
            using (MySqlDataReader rdr = com.ExecuteReader())
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
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Questionnaire> listQuestionnaire = new List<Questionnaire>();
            using (MySqlDataReader rdr = com.ExecuteReader())
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

        //for serch and copy in stock page
        public List<Questionnaire> getAllQuestionnaireByPermitExeptLecturer(int idLecturer)
        {
            con.Open();
            string sqlString = "select * from Questionnaire q where q.Permit = 1 and Not Exists (select * from Course c where q.IdCours = c.Id and c.LecturerID = " + idLecturer + " );";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Questionnaire> listQuestionnaire = new List<Questionnaire>();
            using (MySqlDataReader rdr = com.ExecuteReader())
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


        public string getNameById(int id)
        {
            
                con.Open();
                String name = "";
                string sqlString = "select * from Questionnaire q where q.Id = id ;";
                MySqlCommand com = new MySqlCommand(sqlString, con);
                using (MySqlDataReader rdr = com.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        if (Convert.ToInt32(rdr["Id"]) == id)
                        {
                            name = (rdr["Name"]).ToString();
                        }
                    }
                }
                con.Close();
                return name;
            
        }

        public List<Questionnaire> getIdQuestionnaireByIdCourseAndName(int IdCourse)
        {
            con.Open();
            string sqlString = "select * from Questionnaire q where q.IdCours = " + IdCourse + " AND q.Permit = 1 ;";
            MySqlCommand com = new MySqlCommand(sqlString, con);
            List<Questionnaire> listQuestionnaire = new List<Questionnaire>();
            using (MySqlDataReader rdr = com.ExecuteReader())
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