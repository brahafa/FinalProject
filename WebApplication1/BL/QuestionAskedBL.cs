using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clicker.Classes;
using Clicker.DAL;

namespace Clicker.BL
{
    public class QuestionAskedBL
    {
        private QuestionAskedDAL questionAskedDAL;

        public QuestionAskedBL()
        {
            questionAskedDAL = new QuestionAskedDAL();
        }
        public void AddNewQuestionAsked(int Id, int IdQuestion, int IdStudent, String Date, int YN)
        {
            questionAskedDAL.AddNewQuestionAsked(Id,IdQuestion, IdStudent, Date, YN);
        }
        //get All QuestionAsked By IdQuestion
        public List<QuestionAsked> getAllQuestionAskedByIdQuestion(int IdQuestion)
        {
            return questionAskedDAL.getAllQuestionAskedByIdQuestion(IdQuestion);
        }

        //delet all QuestionAsked By Id Question
        public void deleteQuestionAskedByIdQuestion(int IdQuestion)
        {
            questionAskedDAL.deleteQuestionAskedByIdQuestion(IdQuestion);
        }
        public Boolean isQuestionAsk(int idQuestion){
            List<QuestionAsked> listQuestionAsk = new List<QuestionAsked>();
            listQuestionAsk=questionAskedDAL.getAllQuestionAskedByIdQuestion(idQuestion);
            if (listQuestionAsk.Count > 0)
            {
                return true;
            }
            return false;
        }
        public int maxIdquestionAsk()
        {
            return questionAskedDAL.maxIdquestionAsk();
        }
       

    }
}