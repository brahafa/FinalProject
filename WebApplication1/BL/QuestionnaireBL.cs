using WebApplication1.classes;
using WebApplication1.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebApplication1.BL
{
    public class QuestionnaireBL
    {
        public static QuestionnaireDAL questionnaireDAL;
        public static List<Questionnaire> QuestionnaireList;
        public QuestionnaireBL()
        {
            questionnaireDAL = new QuestionnaireDAL();
        }

        // add Questionnaire
        public void AddQuestionnaire(int Id, String Name, int IdCours, int Permit)
        {
            questionnaireDAL.AddQuestionnaire(Id, Name, IdCours, Permit);
        }

        // delete Questionnaire by id
        public void deleteQuestionnaire(int Id)
        {
            questionnaireDAL.deleteQuestionnaire(Id);
        }

        // delete all Questionnaire by IdCours
        public void deleteQuestionnaireByIdCours(int IdCours)
        {
            questionnaireDAL.deleteQuestionnaireByIdCours(IdCours);
        }
        // get Questionnaire by Name(if permit=1)
        public List<Questionnaire> getAllQuestionnaireByName(String Name)
        {
            QuestionnaireList = questionnaireDAL.getAllQuestionnaireByName(Name);
            return QuestionnaireList;
        }

        // get Questionnaire by Lecturer(if permit=1)
        public List<Questionnaire> getAllQuestionnaireByLecturer(int IdLecturer)
        {
            QuestionnaireList = questionnaireDAL.getAllQuestionnaireByLecturer(IdLecturer);
            return QuestionnaireList;
        }

        // get Questionnaire by IdCourse
        public List<Questionnaire> getAllQuestionnaireByIdCourse(int IdCourse)
        {
            QuestionnaireList = questionnaireDAL.getAllQuestionnaireByIdCourse(IdCourse);
            return QuestionnaireList;
        }

        public int maxIdQuestionnaire()
        {
            return questionnaireDAL.maxIdQuestionnaire();
        }
        public List<Questionnaire> getAllQuestionnaireByIdCours(int IdLecturer)
        {
            QuestionnaireList = questionnaireDAL.getAllQuestionnaireByIdCours(IdLecturer);
            return QuestionnaireList;
        }

        // get Questionnaire by permit(if permit=1), for stock
        public List<Questionnaire> getAllQuestionnaireByPermit()
        {
            QuestionnaireList = questionnaireDAL.getAllQuestionnaireByPermit();
            return QuestionnaireList;
        }
    }
}