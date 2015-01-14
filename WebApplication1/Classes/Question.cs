using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.classes
{
    public class Question
    {
        private int _Id;
        private String _Question;
        private int _IdQuestionnaire;
        private int _Type;
        private String _File;

        public Question(int Id, String Question, int IdQuestionnaire, int Type, String File)
        {
            _Id = Id;
            _Question = Question;
            _IdQuestionnaire = IdQuestionnaire;
            _Type = Type;
            _File = File;
        }

        public int getId()
        {
            return _Id;
        }

        public String getQuestion()
        {
            return _Question;
        }

        public int getIdQuestionnaire()
        {
            return _IdQuestionnaire;
        }

        public int getType()
        {
            return _Type;
        }

        public String getFile()
        {
            return _File;
        }
    }
}