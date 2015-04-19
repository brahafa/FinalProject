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
using WebApplication1.BL;
using WebApplication1.classes;

namespace WebApplication1.Pages
{
    public partial class WebForm6 : System.Web.UI.Page
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
        public static List<Questionnaire> listQuestionnarie;
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
            colorCourses = new Color[9];

            select_Course.Items.Clear();
            listCourse = courseBL.getCoursesByIdLecturer(Convert.ToInt32(Session["id"]));
            select_Course.Items.Add(new ListItem("בחר קורס", "-1"));
            foreach (Course c in listCourse)
            {
                select_Course.Items.Add(new ListItem(c.getName(), c.getId().ToString()));
            }
            if (Session["id"] != null)
            {
                // UserNameLabel.InnerText = Session["Name"].ToString();
                userImage.ImageUrl = Session["Image"].ToString();
            }

            UserNameLabel.InnerText = "";
            UserNameLabel.InnerText += " " + Session["Name"].ToString();
           userImage.ImageUrl = Session["Image"].ToString();


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
        //init the select list -questionnaires
        [WebMethod]
        public static String updateSelectQuestionnaires(String SelectValue)
        {
            String QuestionnarieSTR = "";
            listQuestionnarie = questionnaireBl.getAllQuestionnaireByIdCourse(Convert.ToInt32(SelectValue.ToString()));

            foreach (Questionnaire q in listQuestionnarie)
            {
                QuestionnarieSTR += q.getId().ToString().Trim() + "," + q.getName().Trim() + ",";
            }
            return QuestionnarieSTR;

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
        protected void BtnselectStatistic_Click(System.Object sender, System.EventArgs e)
        {
            int idCurse;//get from session
            int idQuestionna=0;
            if (!selectQuestion.Text.Equals(""))
            {
                 idQuestionna = Convert.ToInt32(selectQuestion.Text.ToString());
            }
            idCurse =Convert.ToInt32(selectTest.Text.ToString());
            if (idCurse == -1 || selectTest.Text.ToString().Equals(""))
            {
                return;
            }
            else if (idQuestionna == -1 || selectQuestion.Text.ToString().Equals(""))
            {
                // s1 = cTestChart.Series["Testing"];
                //כל השאלונים מקורס מסוים
                listQuestionnaire = questionnaireBL.getAllQuestionnaireByIdCours(idCurse);
                //String[] nameQuestion = new String[listQuestionnaire.Count];
                for (int i = listQuestionnaire.Count - 1; i >= 0; i--)
                {
                    printChart(listQuestionnaire[i].getId(), "");

                }
            }
            else
            {
                printChart(idQuestionna, "");
            }

            selectQuestion.Text = "";
            selectTest.Text = "";
        }
        //סטטיסטיקה עבור שאלון בודד
        protected void test_Click(System.Object sender, System.EventArgs e)
        {
            int idQuestionnare = 0;//get fron session
            printChart(idQuestionnare, "");
           
        }
        public void printChart(int idQuestionnare, String valXd)
        {
            String valX =questionnaireBL.getNameById(idQuestionnare);
            int numOfYes = 0, numOfNo = 0;
             s1 = cTestChart.Series["Testing"];

            //מוצא שאלות שנשאלו משאלון idQuestionnare0
         
            listQuestion = questionBL.getAllQuestionByQuestionnaire(idQuestionnare);

            for (int i = listQuestion.Count - 1; i >= 0; i--)
            {
                if (!questionAskedBL.isQuestionAsk(listQuestion[i]._Id))
                {
                    listQuestion.RemoveAt(i);
                }
            }
            //סופר את העונים נכון
            for (int i = listQuestion.Count - 1; i >= 0; i--)
            {
                listQuestionAsked = questionAskedBL.getAllQuestionAskedByIdQuestion(listQuestion[i]._Id);
                for (int j = listQuestionAsked.Count - 1; j >= 0; j--)
                {
                    //if ((!TextBoxFromDate.Text.ToString().Equals("")) && (!TextBoxToDate.Text.ToString().Equals("")))
                    //{
                    //if ((DateTime.Parse(TextBoxFromDate.Text.ToString()) <= DateTime.Parse(listQuestionAsked[j]._Date.ToString().Trim()))&& (DateTime.Parse(TextBoxToDate.Text.ToString()) >= DateTime.Parse(listQuestionAsked[j]._Date.ToString().Trim())))
                    {
                        if (listQuestionAsked[j]._YN == 1)
                        {
                            numOfYes++;
                        }
                        else
                        {
                            numOfNo++;
                        }
                   // }
                    }
             
                }
            }
            s1.Points.AddXY(valX.Trim() + " " + "נכון", numOfYes);
            s1.Points[colorId].Color = colorCourses[colorId];

            s1.Points.AddXY(valX.Trim()+" "+"שגוי", numOfNo);
           // s1.Points.AddXY("", 0);


            s1.Points[colorId+1].Color = colorCourses[colorId++];
           
             //s1.BackSecondaryColor = colorCourses[colorId];
            //colorId++;
            if (colorId > 8)
            {
                colorId = 0;
            }
            //s1.Color = colorCourses[colorId];
          

        }
        protected void logout_click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("logIn.aspx");
        }

    }

     
}