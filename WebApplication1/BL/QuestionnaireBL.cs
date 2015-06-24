using Clicker.Classes;
using Clicker.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Clicker.BL
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

        // get Questionnaire by id
        public Questionnaire getQuestionnaireById(int IdQuestionnaire)
        {
            return questionnaireDAL.getQuestionnaireById(IdQuestionnaire);
            
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
        public String getNameById(int id){
            return questionnaireDAL.getNameById(id);
        }

        public int getIdQuestionnaireByIdCourseAndName(int IdCourse, String Name)
        {
            QuestionnaireList = questionnaireDAL.getIdQuestionnaireByIdCourseAndName(IdCourse);
            for (int i = 0; i < QuestionnaireList.Count; i++)
                if (Name.Trim().Equals(QuestionnaireList[i].getName().Trim()))
                    return QuestionnaireList[i].getId();
            return 0;
        }

                //for serch and copy in stock page
        public List<Questionnaire> getAllQuestionnaireByPermitExeptLecturer(int idCourse)
        {
            return questionnaireDAL.getAllQuestionnaireByPermitExeptLecturer(idCourse);
        }
    }
}