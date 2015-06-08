using Clicker.Classes;
using Clicker.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Clicker.BL
{
    public class AnswerBL
    {
        public static AnswerDAL answerBL;
        public static List<Answer> AnswerList;
        public AnswerBL()
        {
            answerBL = new AnswerDAL();
        }

        //add answer
        public void AddAnswer(int Id, String Answer, int IdQuestion ,int CorrectAnswer)
        {
            answerBL.AddAnswer(Id, Answer, IdQuestion, CorrectAnswer);
        }

        //delet answer
        public void deleteAnswer(int Id)
        {
            answerBL.deleteAnswer(Id);
        }

        //delet all answer By IdQuestion
        public void deleteAnswerByIdQuestion(int IdQuestion)
        {
            answerBL.deleteAnswerByIdQuestion(IdQuestion);
        }

        // get Answer by IdQuestion
        public List<Answer> getAllAnswerByIdQuestion(int IdQuestion)
        {
            AnswerList = answerBL.getAllAnswerByIdQuestion(IdQuestion);
            return AnswerList;
        }
        //maxIdAnswer
        public int maxIdAnswer()
        {
            return answerBL.maxIdAnswer();
        }
    }
}