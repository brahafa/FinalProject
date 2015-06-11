using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
        protected List<Question> listQuestion;

        protected int idCourse;
        protected String courseName;
        protected String questionName;

        private  GlobalFunction global;
        private IList<Course> listCourse;
       // private int questionnaireId;
        private String questionnaireName;

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

            int questionId = 0;

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

            
            questionName = "שם שאלון";
            questionName = questionnaireBL.getNameById(questionId);


            //questionTitle.Text = "בדיקה לכותרת";

            listQuestion = questionBL.getAllQuestionByQuestionnaire(questionId);

            
            if (listQuestion.Count != 0)
            {
                if (listQuestion[0]._Type == 2)//yes no question
                {
                    //questionTitle.Text = "שאלת כן או לא";

                    yesNoDiv.Style.Add("display", "inline");
                }
                else if (listQuestion[0]._Type == 3)//open question
                {
                    //questionTitle.Text = "שאלה פתוחה";
                    OpenDiv.Style.Add("display", "inline");
                }
                else// american question
                {
                    //questionTitle.Text = "שאלת ריבו תשובות";
                    Americananswer.Style.Add("display", "inline");
                }
            
            }
            
            }

        protected void logout_click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("logIn.aspx");
        }
    }
}