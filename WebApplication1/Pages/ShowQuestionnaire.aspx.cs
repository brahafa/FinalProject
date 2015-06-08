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
        private List<Answer> listAnswer;
        private AnswerBL answerBL;
        protected List<Question> listQuestion;
        protected int idCourse;
        protected String courseName;
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

            try
            {
                idCourse = Convert.ToInt32(Session["IdCourse"]);
                courseName = courseBL.getNameById(idCourse);
            }
            catch (FormatException)
            {

            }

            int questionId = 1;

            listQuestion = questionBL.getAllQuestionByQuestionnaire(questionId);
            if (listQuestion.Count != 0)
            {
                if (listQuestion[0]._Type == 11)//yes no question
                {
                    questTitle.Value = "שאלת כן או לא";
                    yesNoDiv.Style.Add("display", "inline");
                }
                else if (listQuestion[0]._Type == 12)//open question
                {
                    questTitle.Value = "שאלה פתוחה";
                    OpenDiv.Style.Add("display", "inline");
                }
                else// american question
                {
                    questTitle.Value = "שאלת ריבו תשובות";
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