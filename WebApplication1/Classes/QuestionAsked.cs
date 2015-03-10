using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.classes
{
    public class QuestionAsked
    {
        public int _Id;
        public int _IdQuestion;
        public int _IdStudent;
        public String _Date;
        public int _YN;


        public QuestionAsked(int Id, int IdQuestion, int IdStudent, String Date, int YN)
        {
            _Id = Id;
            _IdQuestion = IdQuestion;
            _IdStudent = IdStudent;
            _Date = Date;
            _YN = YN;
        }
    }
}