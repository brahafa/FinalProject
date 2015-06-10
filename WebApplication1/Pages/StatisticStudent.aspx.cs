using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using Clicker.BL;
using Clicker.Classes;

namespace Clicker.Pages
{
    public partial class StatisticStudent : System.Web.UI.Page
    {
         


        QuestionBL questionBL;
        QuestionAskedBL questionAskedBL;
        QuestionnaireBL questionnaireBL;
        List<Question> listQuestion;
        List<Questionnaire> listQuestionnaire;
        List<QuestionAsked> listQuestionAsked;
        Series s1;
        public Color[] colorCourses;
        int colorId = 0;

        private static CourseBL courseBL;
        private static QuestionnaireBL questionnaireBl;
        public static List<Course> listCourse;
        //public static List<Questionnaire> listQuestionnarie;
        protected void Page_Load(object sender, EventArgs e)
        {

            courseBL = new CourseBL();
            questionnaireBl = new QuestionnaireBL();
            listCourse = new List<Course>();
            questionnaireBL = new QuestionnaireBL();
            questionBL = new QuestionBL();
            listQuestion = new List<Question>();
            questionAskedBL = new QuestionAskedBL();
            listQuestionAsked = new List<QuestionAsked>();
            listQuestionnaire=new List<Questionnaire>();
            colorCourses = new Color[9];

            listCourse = courseBL.getCoursesByIdLecturer(Convert.ToInt32(Session["id"]));
           
            if (Session["id"] != null)
            {
                // UserNameLabel.InnerText = Session["Name"].ToString();
               // userImage.ImageUrl = Session["Image"].ToString();
            }

            UserNameLabel.InnerText = "";
           // UserNameLabel.InnerText += " " + Session["Name"].ToString();
           //userImage.ImageUrl = Session["Image"].ToString();

            int idCourse = 4;//get from session
            int idStudent = 4;//get from session
            listQuestionnaire = questionnaireBl.getAllQuestionnaireByIdCours(idCourse);
            for (int i = 0; i < listQuestionnaire.Count; i++)
            {
                //printChart(listQuestionnaire[i].getId(), idStudent);
            }

            colorCourses[0] = System.Drawing.Color.LightSalmon;
            colorCourses[1] = System.Drawing.Color.Brown;
            colorCourses[2] = System.Drawing.Color.Coral;
            colorCourses[3] = System.Drawing.Color.Crimson;
            colorCourses[4] = System.Drawing.Color.DarkRed;
            colorCourses[5] = System.Drawing.Color.Red;
            colorCourses[6] = System.Drawing.Color.OrangeRed;
            colorCourses[7] = System.Drawing.Color.Tomato;
            colorCourses[8] = System.Drawing.Color.IndianRed;          
        }

        protected void CalendarFrom_SelectionChanged(System.Object sender, System.EventArgs e)
        {
            TextBoxFromDate.Text = Convert.ToDateTime(Calendar2.SelectedDate, CultureInfo.GetCultureInfo("en-US")).ToString("MM/dd/yyyy");
        }

        protected void CalendarTo_SelectionChanged(System.Object sender, System.EventArgs e)
        {
            if (!TextBoxFromDate.Equals(""))
            {
                TextBoxToDate.Text = Convert.ToDateTime(CalendarTo.SelectedDate, CultureInfo.GetCultureInfo("en-US")).ToString("MM/dd/yyyy");
            }
        }


        //public void printChart(int idQuestionnare, int idStudent)
        //{
        //     String valX =questionnaireBL.getNameById(idQuestionnare);
        //     int numOfYes = 0, numOfNo = 0;
        //     s1 = cTestChart.Series["Testing"];
        // String    ImageLocation = "~/images/ChartPic_#SEQ(300,3)";

        //    //מוצא שאלות שנשאלו משאלון idQuestionnare0
        //    listQuestion = questionBL.getAllQuestionByQuestionnaire(idQuestionnare);

        //    for (int i = listQuestion.Count - 1; i >= 0; i--)
        //    {
        //        if (!questionAskedBL.isQuestionAsk(listQuestion[i]._Id))
        //        {
        //            listQuestion.RemoveAt(i);//remove unuse question 
        //        }
        //    }
        //    //סופר את העונים נכון
        //    for (int i = listQuestion.Count - 1; i >= 0; i--)
        //    {
        //        listQuestionAsked = questionAskedBL.getAllQuestionAskedByIdQuestion(listQuestion[i]._Id);
        //        for (int j = listQuestionAsked.Count - 1; j >= 0; j--)
        //        {
        //            //if ((!TextBoxFromDate.Text.ToString().Equals("")) && (!TextBoxToDate.Text.ToString().Equals("")))
        //            //{
        //            //if ((DateTime.Parse(TextBoxFromDate.Text.ToString()) <= DateTime.Parse(listQuestionAsked[j]._Date.ToString().Trim()))&& (DateTime.Parse(TextBoxToDate.Text.ToString()) >= DateTime.Parse(listQuestionAsked[j]._Date.ToString().Trim())))
        //            if (listQuestionAsked[j]._IdStudent==idStudent)
        //            {
        //                if (listQuestionAsked[j]._YN == 1)
        //                {
        //                    numOfYes++;
        //                }
        //                else
        //                {
        //                    numOfNo++;
        //                }
        //           // }
        //            }
             
        //        }
        //    }
        //    s1.Points.AddXY(valX.Trim() + " " + "נכון", numOfYes);
        //    s1.Points[colorId].Color = colorCourses[colorId];

        //    s1.Points.AddXY(valX.Trim()+" "+"שגוי", numOfNo);
        //    s1.Points[colorId+1].Color = colorCourses[colorId++];
        //    if (colorId > 8)
        //    {
        //        colorId = 0;
        //    }
          

        //}
        protected void logout_click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("logIn.aspx");
        }

    }
    }
