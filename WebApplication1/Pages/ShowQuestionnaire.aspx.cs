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
        public static List<Answer> listAnswer;
        public static AnswerBL answerBL;
        public static QuestionAskedBL questionAskedBL;
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

        public static int numAnswer, maxIdQuestionAsked, idQuestion, idStudent, idAns;
        public static String dateNow;

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
            Boolean findRightCourse = false;
            

            //1-one question. 0-questionnaire
           
            int displayType;
            try{

              displayType  = (int)Session["displayType"];
            }
            catch(NullReferenceException){

                displayType = -1;
                //Response.Write("<script>alert('asas');</script>");
            }
            

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
                            findRightCourse = true;
                            break;
                        }
                    }

                    if (findRightCourse)
                    {
                        questionnaireId = questionnaire.getId();
                        
                        questionName = questionnaireBL.getNameById(questionnaireId);
                        questionName += " :שאלון";

                        //get question by questionnaire
                        listQuestion = questionBL.getAllQuestionByQuestionnaire(questionnaireId);

                        indexQuestion = 0;

                    }
                    else
                    {
                       
                        questionName = "המתן עד להצגת השאלה :)";
                        //Response.Write("<script>document.getElementById('MainContent_waitQuestion').style.display = 'inline';</script>");
                    }
          
                }
                else
                {
                   
                    questionName = "המתן עד להצגת השאלה :)";
                    //Response.Write("<script>document.getElementById('MainContent_waitQuestion').style.display = 'inline';</script>");
                }
                   
                    
            }


            
            }


         [System.Web.Services.WebMethod(EnableSession = true)]
        public static void updateDbQuesionAsked_click(String checkAns)
        {

            questionAskedBL = new QuestionAskedBL();

           
            try
            {
                //index array start with index=0
                numAnswer = Convert.ToInt32(checkAns)-1;
                
            }
            catch (FormatException)
            {
                numAnswer = -1;              
            }

           

            maxIdQuestionAsked = questionAskedBL.maxIdquestionAsk();
            idQuestion = listQuestion[indexQuestion].getId();
             idStudent = Convert.ToInt32(HttpContext.Current.Session["id"]);
             dateNow = DateTime.Now.ToString("dd/MM/yyyy"); 
             //american and yesqno question
             idAns = listAnswer[numAnswer].getId();
             
             //open question

             //insert new row to questionAsked table in DB
             questionAskedBL.AddNewQuestionAsked(maxIdQuestionAsked + 1, idQuestion, idStudent, dateNow, idAns);


        }
        

     [WebMethod]
        public static String displayNextQuestion_click()
        {

            String strReturn = "";

            if (listQuestion.Count-1 > indexQuestion)
            {
                indexQuestion++;
            }
            else// finish to pass list
            {
                indexQuestion = 0;
                return null;
            }

            if (listQuestion[indexQuestion] != null && listQuestion[indexQuestion]._Type == 3)// open question
            {

                return "3#" + listQuestion[indexQuestion]._Question.ToString();
            }
            else// 1/2
            {
                listAnswer = answerBL.getAllAnswerByIdQuestion(listQuestion[indexQuestion].getId());

                if (listAnswer == null)
                {
                    return null;
                }
                // strReturn = index1,#,index2,#, index3
                //index1=3 for openQuestion else =2
                //index2= question string
                //index3= all answers
                switch (listAnswer.Count)
                {
                    case 1:
                        strReturn = "2#" + listQuestion[indexQuestion]._Question.ToString() + "#1#"
                            + listAnswer[0].getAnswer().ToString() +"";
                        break;
                    case 2:
                        strReturn = "2#" + listQuestion[indexQuestion]._Question.ToString() + "#2#" + listAnswer[0].getAnswer().ToString() + "#"
                            + listAnswer[1].getAnswer().ToString();
                        break;
                    case 3:
                        strReturn = "2#" + listQuestion[indexQuestion]._Question.ToString() + "#3#" + listAnswer[0].getAnswer().ToString() + "#"
                            + listAnswer[1].getAnswer().ToString() + "#" + listAnswer[2].getAnswer().ToString();
                        break;
                    case 4:
                        strReturn = "2#" + listQuestion[indexQuestion]._Question.ToString() + "#4#" + listAnswer[0].getAnswer().ToString() + "#"
                            + listAnswer[1].getAnswer().ToString() + "#" + listAnswer[2].getAnswer().ToString() + "#" + listAnswer[3].getAnswer().ToString();
                        break;
                    case 5:
                        strReturn = "2#" + listQuestion[indexQuestion]._Question.ToString() + "#5#" + listAnswer[0].getAnswer().ToString() + "#"
                            + listAnswer[1].getAnswer().ToString() + "#" + listAnswer[2].getAnswer().ToString() + "#" + listAnswer[3].getAnswer().ToString() +"#"
                            + listAnswer[4].getAnswer().ToString();
                        break;
                    case 6:
                        strReturn = "2#" + listQuestion[indexQuestion]._Question.ToString() + "#6#" + listAnswer[0].getAnswer().ToString() + "#"
                             + listAnswer[1].getAnswer().ToString() + "#" + listAnswer[2].getAnswer().ToString() + "#" + listAnswer[3].getAnswer().ToString() + "#"
                             + listAnswer[4].getAnswer().ToString() + "#" + listAnswer[5].getAnswer().ToString();
                        break;
                }

                return strReturn;
                
            }


        }

        protected void logout_click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("logIn.aspx");
        }
    }
}