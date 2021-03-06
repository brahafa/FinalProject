﻿using System;
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
            TextBoxFromDate.Text = Convert.ToDateTime(Calendar2.SelectedDate, CultureInfo.GetCultureInfo("ru-RU")).ToString("dd/MM/yyyy");           
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            TextBoxToDate.Text = Convert.ToDateTime(Calendar1.SelectedDate, CultureInfo.GetCultureInfo("ru-RU")).ToString("dd/MM/yyyy");
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
                    //מדפיס שורה בטבלה עם שם השאלון
                    printTableLine("-שם שאלון      " + "", listQuestionnarie[i].getName().ToString(), "", "lineClass");
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
           string date1,date2;   
            int countAnswer=0;
            bool validTime=true;
            bool checkTime = true;
            string persent, tytle, str, cssclass;


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
                //עבור כל שאלה---הדפס את השאלה עצמה

                str = questStr(listQuestion[i].getQuestion().ToString());
                tytle = ":שאלה";
               

                printTableLine(tytle, str, "", "cellQuestionTxt");


                
                if (TextBoxFromDate.Text.ToString().Trim().Equals("00/00/00") || TextBoxToDate.Text.ToString().Trim().Equals("00/00/00"))
                {
                   // validTime = true;
                    checkTime = false;
                }
                if (!TextBoxToDate.Text.ToString().Trim().Equals("00/00/00"))
                {
                    date1 = TextBoxToDate.Text.ToString().Trim();
                    dateToDate = (Convert.ToDateTime(date1 , CultureInfo.GetCultureInfo("ru-RU")));
                }
                    
                if (!TextBoxFromDate.Text.ToString().Trim().Equals("00/00/00")) {
                     date2 = TextBoxFromDate.Text.ToString().Trim();
                     dateFromDate = (Convert.ToDateTime(date2, CultureInfo.GetCultureInfo("ru-RU")));
                    // Convert.ToDateTime(Calendar2.SelectedDate, CultureInfo.GetCultureInfo("ru-RU")).ToString("dd/MM/yyyy");  
                }
                //finde the true ans id

                countAnswer = 0;
                listQuestionAsked = questionAskedBL.getAllQuestionAskedByIdQuestion(listQuestion[i]._Id);
                for (int j =0; j< listQuestionAsked.Count ; j ++)
                {
                    //validTime = false;
                    DateTime between = new DateTime(Convert.ToDateTime(listQuestionAsked[j]._Date.ToString()).Ticks);

                    if (checkTime == true)
                    {
                        validTime = TimeBetween(between, dateFromDate, dateToDate);
                    }
                    if(validTime==true)
                    { 
                        countAnswer++;
                    }

                }
                double numAns;
                validTime = true;
                listAnswer = answerBL.getAllAnswerByIdQuestion(listQuestion[i].getId());
                //הדפס את כל התשובות של אותה שאלה

                for (int j = 0; j < listAnswer.Count; j++)
                {
                   // validTime = false;
                    numAns = 0;
                    for (int x =0; x < listQuestionAsked.Count ; x++)
                    {
                        DateTime between = new DateTime(Convert.ToDateTime(listQuestionAsked[x]._Date.ToString()).Ticks);

                       if (checkTime == true)
                        {
                            validTime = TimeBetween(between, dateFromDate, dateToDate);
                        }
                        if (listQuestionAsked[x]._YN == listAnswer[j].getId() && validTime == true)
                        {
                            numAns++;
                        //if (validTime == true)
                        //{
                            //countAnswer++;
                        }

                        //סופר את מספר העונים עבור כל תשובה
                        //if (listQuestionAsked[x]._YN == listAnswer[j].getId() && validTime == true)
                        //{
                        //    numAns++;
                        //}
                    }

                    if (countAnswer != 0 )
                    {
                        numAns = (numAns * 100) / countAnswer;

                    }
                    else
                    {
                        numAns = 0;
                    }



                    persent = Convert.ToString(Math.Round((decimal)numAns, 1)) + "%";
                    cssclass="answerClass";
                    str = listAnswer[j].getAnswer().ToString();
                    int idAns;
                    idAns = j + 1;
                    tytle = "תשובה "+""+idAns;

                    //אם התשובה היא נכונה
                    if (listAnswer[j].getCorrectAnswer() != 0)
                    {
                        cssclass = "truAnsClass";
                    }
                    printTableLine(tytle, str, persent, cssclass);
                }
         
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
        public void printTableLine(String tytle, String str,String percent, String cssclass)
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
            cell.Width=77;
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