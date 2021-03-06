﻿using Clicker.Classes;
using Clicker.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Clicker.BL
{
    public class QuestionBL
    {
        public static QuestionDAL questionDAL;
        public static List<Question> QuestionList;
        public QuestionBL()
        {
            questionDAL = new QuestionDAL();
        }

        //add Question
        public void AddQuestion(int Id, String Question, int IdQuestionnaire, int Type, String File1)
        {
            questionDAL.AddQuestion(Id, Question, IdQuestionnaire, Type, File1);
        }

        //delet Question
        public void deleteQuestion(int Id)
        {
            questionDAL.deleteQuestion(Id);
        }

        //delet all Question By Questionnaire
        public void deleteQuestionByQuestionnaire(int IdQuestionnaire)
        {
            questionDAL.deleteQuestionByQuestionnaire(IdQuestionnaire);
        }

        //get All Question By Questionnaire
        public List<Question> getAllQuestionByQuestionnaire(int IdQuestionnaire)
        {
            QuestionList = questionDAL.getAllQuestionByQuestionnaire(IdQuestionnaire);
            return QuestionList;
        }
         public List<Question> getAllQuestionByIDAndQuestionnaire(int IdQuestionnaire, int Id)
        {
            return questionDAL.getAllQuestionByIDAndQuestionnaire(IdQuestionnaire, Id);
        }

        //get All Question By Type
        public List<Question> getAllQuestionByType(int Type)
        {
            QuestionList = questionDAL.getAllQuestionByType(Type);
            return QuestionList;
        }
        public int maxIdQuestion()
        {
            return questionDAL.maxIdQuestion();
        }
        public String getNameQuestById(int id)
        {
            return questionDAL.getNameQuestById(id);
        }
    }
}