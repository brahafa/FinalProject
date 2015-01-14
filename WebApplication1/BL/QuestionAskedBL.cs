using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.classes;
using WebApplication1.DAL;

namespace WebApplication1.BL
{
    public class QuestionAskedBL
    {
        private QuestionAskedDAL questionAskedDAL;

        public QuestionAskedBL()
        {
            questionAskedDAL = new QuestionAskedDAL();
        }

        //get All QuestionAsked By IdQuestion
        public List<QuestionAsked> getAllQuestionAskedByIdCourse(int IdQuestion)
        {
            return questionAskedDAL.getAllQuestionAskedByIdQuestion(IdQuestion);
        }

        //delet all QuestionAsked By Id Question
        public void deleteQuestionAskedByIdQuestion(int IdQuestion)
        {
            questionAskedDAL.deleteQuestionAskedByIdQuestion(IdQuestion);
        }

    }
}