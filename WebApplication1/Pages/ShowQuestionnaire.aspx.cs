using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Clicker.BL;
using Clicker.Classes;

namespace Clicker.Pages
{
    public partial class ShowQuestionnaire : System.Web.UI.Page
    {
        private QuestionnaireBL questionnaireBL;
        private CourseBL courseBL;
        private QuestionBL questionBL;
        private List<Questionnaire> listQuestionnaire;
        protected List<Answer> listAnswer;
        protected AnswerBL answerBL;
        public static List<Question> listQuestion;

        protected int idCourse;
        public static int indexQuestion;
        protected String courseName;
        protected String questionName;

        private  GlobalFunction global;
        private IList<Course> listCourse;
       // private int questionnaireId;
        private String questionnaireName;

        private DisplayBL displayBL;
        protected List<Display> listDisplay;
        private Questionnaire questionnaire;
        //private List<Display> listDisplayQuestionnaire;

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
            listDisplay = new List<Display>();
            
            //listDisplayQuestionnaire = new List<Display>();

            int questionnaireId = 0;
            

            //1-one question. 0-questionnaire
            Session["displayType"] = 0;
            int displayType = (int)Session["displayType"];

            if (Request.QueryString["IdCourse"] != null)// came from click on course
            {

                try
                {
                    idCourse = Convert.ToInt32(Request.QueryString["IdCourse"]);
                    courseName = courseBL.getNameById(idCourse);
                }
                catch (FormatException)
                {
                    idCourse = 0;
                }
            }

            if (displayType == 1)// display on question
            {
                //

                //get all display question
                listDisplay = displayBL.getDisplayByQuestion();

            }
            else// display questionnaire
            {
                //get all display questionnnaire
                listDisplay = displayBL.getDisplayByQuestionnaire();

                if(listDisplay.Count != 0)
                {
                    //find right questionnnaire by idCourse
                    for (int i = 0; i < listDisplay.Count; i++)
                    {

                        questionnaire = questionnaireBL.getQuestionnaireById(listDisplay[i].getIdQuestionnnaire());
                        if (questionnaire.getIdCours() == idCourse)
                        {
                            break;
                        }
                    }

                    questionnaireId = questionnaire.getId();
                    questionName = "שם שאלון";
                    questionName = questionnaireBL.getNameById(questionnaireId);

                    //get question by questionnaire
                    listQuestion = questionBL.getAllQuestionByQuestionnaire(questionnaireId);

                    indexQuestion = 0;

                    //for (int i = 0; i < listDisplay.Count; i++)
                    //{
                    //if (listQuestion.Count != 0)
                    //{
                    //    if (listQuestion[0]._Type == 2)//yes no question
                    //    {
                    //        //questionTitle.Text = "שאלת כן או לא";

                    //        yesNoDiv.Style.Add("display", "inline");
                    //    }
                    //    else if (listQuestion[0]._Type == 3)//open question
                    //    {
                    //        //questionTitle.Text = "שאלה פתוחה";
                    //        OpenDiv.Style.Add("display", "inline");
                    //    }
                    //    else// american question
                    //    {
                    //        //questionTitle.Text = "שאלת ריבו תשובות";
                    //        Americananswer.Style.Add("display", "inline");
                    //    }

                    //}
                    //}
             
                }
                else
                {
                    questionName = "המתן עד להצגת השאלה :)";
                }
                   
                    
            }


            
            }

     [WebMethod]
        public static String displayNextQuestion_click()
        {
            if (listQuestion.Count-1 > indexQuestion)
            {
                indexQuestion++;
            }
            else// finish to pass list
            {
                return null;
            }

            if (listQuestion[indexQuestion] != null && listQuestion[indexQuestion]._Type == 3)// open question
            {

                return "3#" + listQuestion[indexQuestion]._Question.ToString();
            }
            else// 1/2
            {
                return "2#" + listQuestion[indexQuestion]._Question.ToString();
            }


        }

        protected void logout_click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("logIn.aspx");
        }
    }
}