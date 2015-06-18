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
using System.Threading;

namespace Clicker.Pages
{
    public partial class WebForm6 : System.Web.UI.Page
    {

        AnswerBL answerBL;
        QuestionBL questionBL;
        QuestionAskedBL questionAskedBL;
        private static QuestionnaireBL questionnaireBL;
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

            listQuestionnarie = new List<Questionnaire>();
            listQuestion = new List<Question>();
            listQuestionAsked = new List<QuestionAsked>();
            listAnswer=new List<Answer>();
            listCourse = new List<Course>();

            table1 = new Table();
            table1 = statistictTable;

            //insert course to the select
            select_Course.Items.Clear();
            listCourse = courseBL.getCoursesByIdLecturer(Convert.ToInt32(Session["id"]));
            select_Course.Items.Add(new ListItem("בחר קורס", "-1"));
            foreach (Course c in listCourse)
            {
                select_Course.Items.Add(new ListItem(c.getName(), c.getId().ToString()));
            }
            //if (Session["id"] != null)
            //{
            //    // UserNameLabel.InnerText = Session["Name"].ToString();
            //   // userImage.ImageUrl = Session["Image"].ToString();
            //}
            UserNameLabel.InnerText = "";
            UserNameLabel.InnerText += " " + Session["Name"].ToString();
          // userImage.ImageUrl = Session["Image"].ToString();

        }
        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            //TextBoxFromDate.Text = Calendar2.SelectedDate.ToShortDateString();
            TextBoxFromDate.Text = Convert.ToDateTime(Calendar2.SelectedDate, CultureInfo.GetCultureInfo("ru-RU")).ToString("dd/MM/yyyy");
    
            //dateFromDate = DateTime.Parse(TextBoxFromDate.Text.ToString().Trim(), new CultureInfo("ru-RU"));
           
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            //TextBoxToDate.Text = Calendar1.SelectedDate.ToShortDateString();
            TextBoxToDate.Text = Convert.ToDateTime(Calendar1.SelectedDate, CultureInfo.GetCultureInfo("ru-RU")).ToString("dd/MM/yyyy");
            //dateToDate=new DateTime (Calendar1.SelectedDate.Ticks);
           // dateToDate = DateTime.Parse(TextBoxToDate.Text.ToString());
            //dateToDate = DateTime.ParseExact(TextBoxToDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //dateToDate = new DateTime();
           // dateToDate = DateTime.Parse(TextBoxToDate.Text.ToString().Trim(), new CultureInfo("ru-RU"));
          
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

        //protected void CalendarFrom_SelectionChanged(System.Object sender, System.EventArgs e)
        //{
        //    TextBoxFromDate.Text = Convert.ToDateTime(Calendar2.SelectedDate, CultureInfo.GetCultureInfo("ru-RU")).ToString("MM/dd/yyyy");
        //}

        //protected void CalendarTo_SelectionChanged(System.Object sender, System.EventArgs e)
        //{
        //    if (!TextBoxFromDate.Equals(""))
        //    {
        //       TextBoxToDate.Text = Convert.ToDateTime(CalendarTo.SelectedDate, CultureInfo.GetCultureInfo("en-US")).ToString("MM/dd/yyyy");
        //    }
        //}
        protected void BtnselectStatistic_Click(System.Object sender, System.EventArgs e)
        {
            //table1 = new Table();
           // List<Questionnaire> list = new List<Questionnaire>();
            listQuestionnarie = new List<Questionnaire>();
            int idCurse;//get from textBox =none
            int idQuestionna=0;
            if (!selectQuestion.Text.Equals(""))
            {
                 idQuestionna = Convert.ToInt32(selectQuestion.Text.ToString());
            }
            //no selected course 
            if(!(selectTest.Text.ToString().Equals(""))){
                idCurse =Convert.ToInt32(selectTest.Text.ToString());
            }
            else
            {
                idCurse = -1;
            }
            if (idCurse == -1 || selectTest.Text.ToString().Equals(""))
            {
                return;
            }
            else if (idQuestionna == -1 || selectQuestion.Text.ToString().Equals(""))
            {
                //כל השאלונים מקורס מסוים
                listQuestionnarie = questionnaireBL.getAllQuestionnaireByIdCourse(idCurse);
                for (int i = listQuestionnarie.Count - 1; i >= 0; i--)
                {
                    dr = new TableRow();
                    cell = new TableCell();
                    cell.Text = "";
                    dr.Cells.Add(cell); 
                    dr.CssClass = "lineClass";
                    cell = new TableCell();
                    cell.Text = listQuestionnarie[i].getName().ToString();
                    //cell.BackColor = Color.Tan;

                    dr.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "-שם שאלון      " + "";
                    //cell.CssClass = "cellClass";

                    dr.Cells.Add(cell);
                    dr.BackColor = Color.Tan;
                    table1.Rows.Add(dr);


                    printChart(listQuestionnarie[i].getId(), "");

                }
            }
            else
            {
                printChart(idQuestionna, "");
            }

            selectQuestion.Text = "";
            selectTest.Text = "";
        }

        public void printChart(int idQuestionnare, String valXd)
        {
            
            int countAnswer=0;
            bool validTime=false;
           // DateTime start = Convert.ToDateTime(TextBoxFromDate.Text.ToString());
            //DateTime end = Convert.ToDateTime(TextBoxToDate.Text.ToString());

            //מוצא שאלות שנשאלו משאלון idQuestionnare0
            listQuestion = questionBL.getAllQuestionByQuestionnaire(idQuestionnare);

            for (int i = listQuestion.Count - 1; i >= 0; i--)
            {


                    //if the question never ascked
                    if (!questionAskedBL.isQuestionAsk(listQuestion[i]._Id))
                    {
                        listQuestion.RemoveAt(i);
                    }
            }
            //על כל שאלה להדפיס את התשובות ששיכות לה

            for (int i = listQuestion.Count - 1; i >= 0; i--)
            {
                dr = new TableRow();
                cell = new TableCell();
                cell.Text = "";
                dr.Cells.Add(cell);

                //עבור כל שאלה---הדפס את השאלה עצמה
                dr.CssClass = "lineClass";
                cell = new TableCell();
                cell.Text =  listQuestion[i].getQuestion().ToString();
                cell.BackColor = Color.RosyBrown;
                dr.Cells.Add(cell);

                cell = new TableCell();
                dr.BackColor = Color.RosyBrown;
                cell.Text = ":שאלה      " + "";
                dr.Cells.Add(cell);
                table1.Rows.Add(dr);

                countAnswer = 0;
                if (TextBoxFromDate.Text.ToString().Trim().Equals("00/00/00") || TextBoxToDate.Text.ToString().Trim().Equals("00/00/00"))
                {
                    validTime = true;
                }
                //finde the true ans id
                string date1 = TextBoxToDate.Text.ToString().Trim();
                DateTime dateToDate = Convert.ToDateTime(date1);

                string date = TextBoxFromDate.Text.ToString().Trim();
                DateTime dateFromDate = Convert.ToDateTime(date);  

                listQuestionAsked = questionAskedBL.getAllQuestionAskedByIdQuestion(listQuestion[i]._Id);
                for (int j = listQuestionAsked.Count - 1; j >= 0; j--)
                {
                    DateTime between = new DateTime(Convert.ToDateTime(listQuestionAsked[j]._Date.ToString()).Ticks);

                    if (validTime == false)
                    {
                        validTime = TimeBetween(between, dateFromDate, dateToDate);
                    }
                    if(validTime==true)
                    { 
                        countAnswer++;
                    }

                }
                int numAns;
                listAnswer = answerBL.getAllAnswerByIdQuestion(listQuestion[i].getId());
                //הדפס את כל התשובות של אותה שאלה

                for (int j = 0; j < listAnswer.Count; j++)
                {
                    numAns = 0;
                    for (int x = listQuestionAsked.Count - 1; x >= 0; x--)
                    {
                        //סופר את מספר העונים עבור כל תשובה
                        if (listQuestionAsked[x]._YN == listAnswer[j].getId() && validTime == true)
                        {
                            numAns++;
                        }
                    }

                    if (countAnswer != 0)
                    {
                        numAns = (numAns * 100) / countAnswer;

                    }
                    else
                    {
                        numAns = 0;
                    }
                    dr = new TableRow();
                    cell = new TableCell();
                    cell.Text = Convert.ToString(numAns)+"%";
                    dr.Cells.Add(cell);

                    int idAns;
                    idAns = j + 1;
                   
                    cell = new TableCell();
                    cell.Text = listAnswer[j].getAnswer().ToString();
                    dr.CssClass = "lineClass";
                    dr.BackColor = Color.White;
                    dr.Cells.Add(cell);
                    cell = new TableCell();
                    if (listAnswer[j].getCorrectAnswer() != 0)
                    {
                        dr.CssClass = "cellClass";
                    }
                    cell.Text = "תשובה " + idAns;
                    dr.Cells.Add(cell);
                    
                    table1.Rows.Add(dr);

                }
         
            }
        }
        bool TimeBetween(DateTime datetime, DateTime start, DateTime end)
        {
            // user dont filter by time
            //if(start.ToString().Equals("00/00/00") || end.ToString().Equals("00/00/00")) {
            //    return true;
            //}
            // see if start comes before end
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