using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using WebApplication1.BL;
using WebApplication1.classes;

namespace WebApplication1.Classes
{

    public class GlobalFunction
    {
        private static QuestionnaireBL questionnaireBL;
        private static QuestionBL questionBL;
        private static QuestionAskedBL questionAskedBL;
        private static List<Questionnaire> listQuestionnaire;
        private static List<Question> listQuestion;
        private static AnswerBL answerBL;
        private static CourseBL courseBL;
        private static CourseRegisterBL courseRegisterBL;
        public int sendEmail(String subject, String body, String emailTo)
        {
            string smtpAddress = "smtp.gmail.com";
            int portNumber = 587;
            bool enableSSL = true;

            string emailFrom = "brachafab@gmail.com";
            string emailpassword = "0503389483";
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, emailpassword);
                    smtp.EnableSsl = enableSSL;
                      try
                      {
                  
                        smtp.Send(mail);
                        return 0;
                      }
                      catch (Exception ex)
                      {
                          Console.WriteLine(ex.Message);
                          return -1;
                      }
                }


            }
        }

        //Method removes all information related to this course
        public void removeLecurerCourseFromDB(int idCourse)
        {
            listQuestionnaire = new List<Questionnaire>();
            listQuestion = new List<Question>();
            questionnaireBL = new QuestionnaireBL();
            courseRegisterBL = new CourseRegisterBL();
            courseBL = new CourseBL();
            questionBL = new QuestionBL();
            answerBL = new AnswerBL();
            questionAskedBL = new QuestionAskedBL();

            listQuestionnaire = questionnaireBL.getAllQuestionnaireByIdCourse(idCourse);
            for (int i = 0; i < listQuestionnaire.Count; i++)
            {
                listQuestion = questionBL.getAllQuestionByQuestionnaire(listQuestionnaire[i].getId());
                for (int j = 0; j < listQuestion.Count; j++)
                {
                    questionAskedBL.deleteQuestionAskedByIdQuestion(listQuestion[j].getId()); // delete all QuestionAsked By IdQuestion
                    answerBL.deleteAnswerByIdQuestion(listQuestion[j].getId()); // delete all answer By IdQuestion
                }
                listQuestion = null;
                questionBL.deleteQuestionByQuestionnaire(listQuestionnaire[i].getId()); //delete all Question By Questionnaire
            }
            listQuestionnaire = null;
            questionnaireBL.deleteQuestionnaireByIdCours(idCourse);//delete all Questionnaire By IdCours

            courseRegisterBL.deleteCourseRegisterByIdCourse(idCourse);//delete all CourseRegister By IdCourse

            courseBL.deleteCoursesById(idCourse);//delete course from DB

            //"free" object
            questionnaireBL = null;
            questionBL = null;
            answerBL = null;
            questionAskedBL = null;
        }
    }
}