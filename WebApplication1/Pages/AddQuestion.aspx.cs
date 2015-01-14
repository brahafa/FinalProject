using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebApplication1.BL;
using WebApplication1.classes;

namespace WebApplication1.Pages
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        private static int maxQuestion, maxAnswer;
        private static CourseBL courseBL;
        private static QuestionnaireBL questionnaireBl;
        private static QuestionBL questionBL;
        private static AnswerBL answerBL;
        public static List<Course> listCourse;
        public static List<Questionnaire> listQuestionnarie;
        protected void Page_Load(object sender, EventArgs e)
        {
            courseBL = new CourseBL();
            answerBL = new AnswerBL();
            questionBL = new QuestionBL();
            questionnaireBl = new QuestionnaireBL();
            listCourse = new List<Course>();
            select_Course.Items.Clear();
            listCourse = courseBL.getCoursesByIdLecturer(Convert.ToInt32(Session["id"]));
            select_Course.Items.Add(new ListItem("בחר קורס", "-1"));
            foreach (Course c in listCourse)
            {
                select_Course.Items.Add(new ListItem(c.getName(), c.getId().ToString()));
            }

        }
        [WebMethod]
        public static void save_ClientClick(String AllAnsStr)
        {
             maxQuestion = questionBL.maxIdQuestion() + 1;
            maxAnswer = answerBL.maxIdAnswer() + 1;
            int maxQuestionnaire = questionnaireBl.maxIdQuestionnaire() + 1;


            int type=1;
            String[] QuestionnaireDitails=new String[3];
          
            String[] tempQuestiommaier = AllAnsStr.Split('@');
            String[] ANS = tempQuestiommaier[0].Split('#');
            String question = ANS[Convert.ToInt32(ANS.Length - 2)];
            int idQuestionnier = Convert.ToInt32(ANS[Convert.ToInt32(ANS.Length - 3)]);
            if (idQuestionnier == -2)//if checked "new questionn"
            {
                // add new questionnaire to the DB
                idQuestionnier = maxQuestionnaire;
                QuestionnaireDitails = tempQuestiommaier[1].Split('#');
                questionnaireBl.AddQuestionnaire(maxQuestionnaire, QuestionnaireDitails[0], Convert.ToInt32(QuestionnaireDitails[1]), Convert.ToInt32(QuestionnaireDitails[2]));
            }
            int numAns = Convert.ToInt32(ANS[Convert.ToInt32(ANS.Length-1)]);
            if (numAns == 11)//type is yes no quest
            {
                numAns = 1;
                type = 2;
            }
            else if (numAns == 12)//type is open quwst
            {
                numAns = 1;
                type = 3;
            }
            int correct;
            if (ANS[0].Equals("nullAns"))
            {
                correct = 0;
            }
            else
            {
                correct = Convert.ToInt32(ANS[0]);
            }
            for (int i = 1; i < numAns+1; i++)
            {
                answerBL.AddAnswer(maxAnswer, ANS[i].ToString(), maxQuestion, correct);
                maxAnswer++;
            }
            questionBL.AddQuestion(maxQuestion, question, idQuestionnier, type, "sasa");
        }
        //addNewQuestionnaire
        [WebMethod]
        public static int addNewQuestionnaire(String newQuest)
        {
            string[] QuestionnaireDitails = newQuest.Split('#');//0=name questionnaire. 1=id course. 2=permit
            int maxID=questionnaireBl.maxIdQuestionnaire() + 1;
            questionnaireBl.AddQuestionnaire(maxID, QuestionnaireDitails[0], Convert.ToInt32(QuestionnaireDitails[1]), Convert.ToInt32(QuestionnaireDitails[2]));
            return maxID;
        }


        [WebMethod]
        public static String updateSelectQuestionnaires(String SelectValue)
        {
            String QuestionnarieSTR="";
            listQuestionnarie = questionnaireBl.getAllQuestionnaireByIdCourse(Convert.ToInt32(SelectValue.ToString()));
          
            foreach (Questionnaire q in listQuestionnarie)
            {
                QuestionnarieSTR += q.getId().ToString().Trim() + "," + q.getName().Trim() + ",";
            }
            return QuestionnarieSTR;
            
        }
    }
}