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

        AnswerBL answerBL;
        QuestionBL questionBL;
        QuestionAskedBL questionAskedBL;
        private static QuestionnaireBL questionnaireBL;
        CourseRegisterBL courseRegisterBL;
        CourseBL courseBL;

        List<Question> listQuestion;
        List<QuestionAsked> listQuestionAsked;
        List<Answer> listAnswer;
        List<Course> listCourse;
        private static List<Questionnaire> listQuestionnarie;


        Table table1;
        TableRow dr;
        TableCell cell;

        DateTime dateFromDate, dateToDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            List<Questionnaire> list = new List<Questionnaire>();



            courseBL = new CourseBL();
            questionnaireBL = new QuestionnaireBL();
            questionBL = new QuestionBL();
            answerBL = new AnswerBL();
            questionAskedBL = new QuestionAskedBL();
            courseRegisterBL = new CourseRegisterBL();

            listQuestionnarie = new List<Questionnaire>();
            listQuestion = new List<Question>();
            listQuestionAsked = new List<QuestionAsked>();
            listAnswer = new List<Answer>();
            listCourse = new List<Course>();

            table1 = new Table();
            table1 = statistictTable;

            //insert course to the select
            select_Course.Items.Clear();
            listCourse = courseRegisterBL.getCoursesByIdStudent(Convert.ToInt32(Session["id"]));
            select_Course.Items.Add(new ListItem("בחר קורס", "-1"));
            foreach (Course c in listCourse)
            {
                select_Course.Items.Add(new ListItem(c.getName(), c.getId().ToString()));
            }
            UserNameLabel.InnerText = "";
            UserNameLabel.InnerText += " " + Session["Name"].ToString();
            // userImage.ImageUrl = Session["Image"].ToString();

        }
     
        //init the select list -questionnaires
        [WebMethod]
        public static String updateSelectQuestionnaires(String SelectValue)
        {
            String QuestionnarieSTR = "";
            listQuestionnarie = questionnaireBL.getAllQuestionnaireByIdCourse(Convert.ToInt32(SelectValue.ToString()));

            foreach (Questionnaire q in listQuestionnarie)
            {
                QuestionnarieSTR += q.getId().ToString().Trim() + "," + q.getName().Trim() + ",";
            }
            return QuestionnarieSTR;

        }

        protected void BtnselectStatistic_Click(System.Object sender, System.EventArgs e)
        {
            listQuestionnarie = new List<Questionnaire>();
            int idCurse;//get from textBox =none
            int idQuestionna = 0;
            String quest = "";
            if (!selectQuestion.Text.Equals(""))
            {
                idQuestionna = Convert.ToInt32(selectQuestion.Text.ToString());
            }
            //no selected course 
            if (!(selectTest.Text.ToString().Equals("")))
            {
                idCurse = Convert.ToInt32(selectTest.Text.ToString());
            }
            else
            {
                idCurse = -1;
            }
            if (idCurse == -1 || selectTest.Text.ToString().Equals(""))
            {
                return;//send user  messege- check course 
            }
            else if (idQuestionna == -1 || selectQuestion.Text.ToString().Equals(""))
            {
                return;// send user messege for check questioniare
             
            }
            else
            {
                listQuestionAsked = questionAskedBL.getAllQuestionAskedByIdStudent(Convert.ToInt32(Session["id"]));
                for (int i = 0; i < listQuestionAsked.Count(); i++)
                {
                    // get for any questionAsck the qustion string and the answer
                    listQuestion = questionBL.getAllQuestionByIDAndQuestionnaire(idQuestionna, listQuestionAsked[i]._IdQuestion);
                    if (listQuestion.Count() > 0)
                    {
                        //questStr
                        quest = questStr(listQuestion[0].getQuestion().ToString());
                        printTableLine("-שאלה      " + "", quest, "", "cellQuestionTxt");

                        //
                        printAnswer(listQuestion[0].getId(), listQuestionAsked[i]._YN);
                    }
                }
                  //  printChart(idQuestionna, "");
            }

            selectQuestion.Text = "";
            selectTest.Text = "";
        }
        public void printAnswer(int idQuest, int idCheckAns)
        {
            String  cssclass;
            listAnswer = answerBL.getAllAnswerByIdQuestion(idQuest);
            for (int j = 0; j < listAnswer.Count; j++)
            {
                cssclass = "studentAns";
                int idAns;
                idAns = j + 1;
                if (listAnswer[j].getCorrectAnswer() != 0)
                {
                    cssclass = "trueAnsStudent";
                }
                //if the ans is wrong and the student check this ans
                if (listAnswer[j].getCorrectAnswer() == 0 && listAnswer[j].getId() == idCheckAns)
                {
                    cssclass = "wrongAnsStudent";
                }
                printTableLine("תשובה " + idAns, listAnswer[j].getAnswer().ToString(), "", cssclass);
            }
        }
        public string questStr(string quest)
        {
            char c1 = quest[quest.Length - 1];
            if (c1.Equals('?'))
            {
                quest = '?' + quest.Substring(0, quest.Length - 1);
            }
            return quest;
        }
        public void printTableLine(String tytle, String str, String percent, String cssclass)
        {
            dr = new TableRow();
            dr.CssClass = cssclass;

            cell = new TableCell();
            cell.Text = percent;
            dr.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = str;
            dr.Cells.Add(cell);



            cell = new TableCell();
            cell.Text = tytle;
            cell.Width = 77;
            dr.Cells.Add(cell);

            table1.Rows.Add(dr);

        }

        bool TimeBetween(DateTime datetime, DateTime start, DateTime end)
        {
            if (start < end)
                return start <= datetime && datetime <= end;
            // start is after end, so do the inverse comparison
            return false;// !(end < datetime && datetime < start);
        }
        protected void logout_click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("logIn.aspx");
        }

    }
    }
