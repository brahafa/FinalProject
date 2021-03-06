﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Clicker.BL;
using Clicker.Classes;

namespace Clicker.Pages
{
    public partial class c : System.Web.UI.Page
    {
        public QuestionnaireBL questionnaireBL;
        private CourseBL courseBL;
        static QuestionBL questionBL;
        public List<Questionnaire> listQuestionnaire;
        public List<Answer> listAnswer;
        public AnswerBL answerBL;
        public static List<Question> listQuestion;
        public static int idCourse = 0;
        public static int idLecturer = 0;
        private String CourseName;
        private static GlobalFunction global;
        private static IList<Course> listCourse;
        private static int questionnaireId;
        private static String questionnaireName;

        public static DisplayBL displayBL;
        const int idQuestZero = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            questionnaireBL = new QuestionnaireBL();
            courseBL = new CourseBL();
            questionBL = new QuestionBL();
            listQuestionnaire = new List<Questionnaire>();
            listQuestion = new List<Question>();
            listAnswer = new List<Answer>();
            global = new GlobalFunction();
            courseBL = new CourseBL();
            answerBL = new AnswerBL();
            listCourse = new List<Course>();
            displayBL = new DisplayBL();

            UserNameLabel.InnerText = Session["Name"].ToString();

            idLecturer = Convert.ToInt32(Session["id"]);

            if (Request.QueryString["IdCourse"] != null)// came from click on course
            {

                //Response.Write("<script language=javascript>alert('השדה שאתה רוצה למחוק בשימוש');</script>");
                //CourseNameLabe.InnerText = courseBL.getNameById(idCourse).ToString();

                try
                {
                    idCourse = Convert.ToInt32(Request.QueryString["IdCourse"]);
                }
                catch (FormatException)
                {

                }
                Session["IdCourse"] = idCourse;// save id course for addQuestion page.

                CourseName = courseBL.getNameById(idCourse).ToString();

                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Session["coursId"] + "');", true);
                listQuestionnaire = questionnaireBL.getAllQuestionnaireByIdCourse(idCourse);
            }
            else// seek questionnaires
            {

                CourseName = "חיפוש";
                idCourse = 0;
                //staticBtn.Style.Add("display", "none");
                removeCourseBtnFromQ.Style.Add("display", "none");
                //codeCourse.Style.Add("display", "none");
                

                //removeQuestionnaireBtn.Style.Add("display", "none");
                //copyQuestionnaireBtn.Style.Add("display", "none");
                listQuestionnaire = questionnaireBL.getAllQuestionnaireByPermitExeptLecturer(idLecturer);

                selectCourse.Items.Clear();
                listCourse = courseBL.getCoursesByIdLecturer(idLecturer);
                selectCourse.Items.Add(new ListItem("בחר קורס", "-1"));
                foreach (Course c in listCourse)
                {
                    selectCourse.Items.Add(new ListItem(c.getName(), c.getId().ToString()));
                }
            }



        }

        public String getCourseName()
        {
            return CourseName;
        }



        public void onClick_Questionnaire(object sender, EventArgs e)
        {
            stockQuestionnaire.Style.Add("display", "none");
            StockQuestion.Style.Add("display", "inline");


            //Response.Write("<script language=javascript>alert('השדה שאתה רוצה למחוק בשימוש');</script>");
            questionnaireName = QuestionnaireName.Value.Trim();
            try
            {
                questionnaireId = Convert.ToInt32(idQuestnaire.Value.Trim());
            }
            catch(FormatException){

                questionnaireId = 0;
            }


            List<Question> listQuest = new List<Question>();


            if (idCourse != 0)
            {
                removeQuestionnaireBtn.Style.Add("display", "inline");
                //classDisplayBtn.Style.Add("display", "inline");
                displayQuestFromCourseBtn.Style.Add("display", "inline");
                deletDisplayQuestFromCourseBtn.Style.Add("display", "none");

                //Response.Write("<script>document.getElementById('MainContent_displayQuestFromCourseBtn').style.display = 'inline';</script>");
                listQuest = displayBL.getDisplayQuestionsByQuestionnaire(questionnaireId);

                if (listQuest.Count != 0)
                {
                    deletDisplayQuestFromCourseBtn.Style.Add("display", "inline");
                    displayQuestFromCourseBtn.Style.Add("display", "none");
                }
                else
                {
                    deletDisplayQuestFromCourseBtn.Style.Add("display", "none");
                    displayQuestFromCourseBtn.Style.Add("display", "inline");
                }
                
                //questionnaireId = questionnaireBL.getIdQuestionnaireByIdCourseAndName(idCourse, questionnaireName);
                listQuestion = questionBL.getAllQuestionByQuestionnaire(questionnaireId);
            }
            else
            {
                removeQuestionnaireBtn.Style.Add("display", "none");
                copyQuestionnaireBtn.Style.Add("display", "inline");
                selectCourse.Style.Add("display", "inline");

                listQuestion = questionBL.getAllQuestionByQuestionnaire(questionnaireId);


            }
        }
        public void onClick_Question(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(QuestionId.Value);
            listAnswer = answerBL.getAllAnswerByIdQuestion(id);
        }


        //add questionnaire
        public void add_Question_Click(object sender, EventArgs e)
        {
            String name = getCourseName();
            Response.Redirect("AddQuestion.aspx?courseName=" + name);
            // idcourse saved in ssesion
        }


        //remove course
        [WebMethod]
        public static void removeCourse()
        {
            global.removeLecurerCourseFromDB(idCourse); // remove Lecurer course


        }

        // remove Questionnaire
        [WebMethod]
        public static String removeQuestionnaire()//String nameQuestionnaire
        {
            //local variables
            List<Question> listQuestions = new List<Question>();
            QuestionnaireBL questionnaireBL = new QuestionnaireBL();
            QuestionBL questionBL = new QuestionBL();
            AnswerBL answerBL = new AnswerBL();
            QuestionAskedBL questionAskedBL = new QuestionAskedBL();

            //int idQuestionnaire = questionnaireBL.getIdQuestionnaireByIdCourseAndName(idCourse, questionnaireName);//find idQuestionnaire

            listQuestions = questionBL.getAllQuestionByQuestionnaire(questionnaireId);
            for (int j = 0; j < listQuestions.Count; j++)
            {
                questionAskedBL.deleteQuestionAskedByIdQuestion(listQuestions[j].getId()); // delete all QuestionAsked By IdQuestion
                answerBL.deleteAnswerByIdQuestion(listQuestions[j].getId()); // delete all answer By IdQuestion
            }

            questionBL.deleteQuestionByQuestionnaire(questionnaireId); //delete all Question By Questionnaire
            questionnaireBL.deleteQuestionnaire(questionnaireId);//delet questionnaire

            //"free" object
            questionnaireBL = null;
            questionBL = null;
            answerBL = null;
            questionAskedBL = null;
            listQuestions = null;

            String strIdCourse = idCourse.ToString();
            return strIdCourse;
        }

        // copy Questionnaire
        [WebMethod]
        public static int copyQuestionnaire(String SelectValue)
        {
            QuestionnaireBL questionnaireBL = new QuestionnaireBL();
            List<Question> listQuestions = new List<Question>();
            List<Answer> listAnswers = new List<Answer>();
            QuestionBL questionBL = new QuestionBL();
            AnswerBL answerBL = new AnswerBL();
            List<Questionnaire> listQuestionnaires = new List<Questionnaire>();


            int idQuestionnaireToCopy, idQuestionnaireFromCopy, permit, courseId;
            int idQuestionToCopy, idQuestionFromCopy, idAnswerToCopy;
            String nameQuestionnaire;

            String[] strSV;
            strSV = SelectValue.Split(',');

            nameQuestionnaire = questionnaireName;
            permit = 0; //can't show in search because it is exist
            courseId = Convert.ToInt32(strSV[1]);

            idQuestionnaireFromCopy = Convert.ToInt32(strSV[0]); ;
            idQuestionnaireToCopy = questionnaireBL.maxIdQuestionnaire() + 1;

            listQuestionnaires = questionnaireBL.getAllQuestionnaireByIdCourse(courseId);
            for (int i = 0; i < listQuestionnaires.Count; i++)
            {
                if (listQuestionnaires[i].getName().Equals(nameQuestionnaire) && listQuestionnaires[i].getIdCours() == courseId)
                {
                    return courseId;
                }
            }


            questionnaireBL.AddQuestionnaire(idQuestionnaireToCopy, nameQuestionnaire, courseId, permit);//add questionnaire

            listQuestions = questionBL.getAllQuestionByQuestionnaire(idQuestionnaireFromCopy);//copy all question


            idQuestionToCopy = questionBL.maxIdQuestion() + 1;

            idAnswerToCopy = answerBL.maxIdAnswer() + 1;

            for (int j = 0; j < listQuestions.Count; j++)
            {
                idQuestionFromCopy = listQuestions[j].getId();

                questionBL.AddQuestion(idQuestionToCopy, listQuestions[j].getQuestion(), idQuestionnaireToCopy, listQuestions[j].getType(), listQuestions[j].getFile()); // add question

                listAnswers = answerBL.getAllAnswerByIdQuestion(idQuestionFromCopy);
                for (int i = 0; i < listAnswers.Count; i++)
                {
                    //idAnswerFromCopy = ;
                    answerBL.AddAnswer(idAnswerToCopy, listAnswers[i].getAnswer(), idQuestionToCopy, listAnswers[i].getCorrectAnswer()); // add answer

                    idAnswerToCopy++;
                }
                idQuestionToCopy++;

            }
            //"free" object
            questionnaireBL = null;
            questionBL = null;
            answerBL = null;
            listQuestions = null;
            listAnswers = null;

            return courseId;

        }

        // set questionnaire to display

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void displayBtnCourse_click(String idQuestionnaire)
        {

            HttpContext.Current.Session["displayType"] = 0;

            int displayType = (int)HttpContext.Current.Session["displayType"];

            int maxIdDisplay, idQ;

            maxIdDisplay = displayBL.maxIdDisplay();
            try
            {
                idQ = Convert.ToInt32(idQuestionnaire);
            }
            catch (FormatException)
            {
                idQ = 0;
            }
            

            displayBL.AddNewDisplay(maxIdDisplay+1, idQuestZero, idQ);

        }

        // delet questionnaire from display

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void deletDisplayBtnCourse_click(String idQuestionnaire)
        {

            int idQ;

            try
            {
                idQ = Convert.ToInt32(idQuestionnaire);
            }
            catch (FormatException)
            {
                idQ = 0;
            }

            displayBL.deleteDisplayByIdQuestionnaire(idQ);

        }
        
        


        //free session and redirect to login page.
        protected void logout_click(object sender, EventArgs e)
        {
            int displayType = Convert.ToInt32(Session["displayType"]);

            Session.Abandon();

            Session["displayType"] = displayType;
            Response.Write("<script>alert('"+displayType+"');</script>");

            Response.Redirect("logIn.aspx");
        }



    }
}