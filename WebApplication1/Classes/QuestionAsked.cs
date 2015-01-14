using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.classes
{
    public class QuestionAsked
    {
        private int _Id;
        private int _IdQuestion;
        private int _IdStudent;
        private DateTime _Date;
        private int _YN;


        public QuestionAsked(int Id, int IdQuestion, int IdStudent, DateTime Date, int YN)
        {
            _Id = Id;
            _IdQuestion = IdQuestion;
            _IdStudent = IdStudent;
            _Date = Date;
            _YN = YN;
        }
    }
}