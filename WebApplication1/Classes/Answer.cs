using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.classes
{
    public class Answer
    {
        private int _Id;
        private String _Answer;
        private int _IdQuestion;
        private int _CorrectAnswer;

        public Answer(int Id, String Answer, int IdQuestion,int correctAnswer)
        {
            _Id = Id;
            _Answer = Answer;
            _IdQuestion = IdQuestion;
            _CorrectAnswer = correctAnswer;
        }

        public int getId()
        {
            return _Id;
        }

        public String getAnswer()
        {
            return _Answer;
        }

        public int getIdQuestion()
        {
            return _IdQuestion;
        }
        public int getCorrectAnswer()
        {
            return _CorrectAnswer;
        }

    }
}