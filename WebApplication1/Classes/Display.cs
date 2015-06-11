using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicker.Classes
{
    public class Display
    {
        private int _Id;
        private int _IdQuestion;
        private int _IdQuestionnnaire;


        public Display(int Id, int IdQuestion, int IdQuestionnnaire)
        {
            _Id = Id;
            _IdQuestion = IdQuestion;
            _IdQuestionnnaire = IdQuestionnnaire;

        }

        public int getId()
        {
            return _Id;
        }

        public int getIdQuestion()
        {
            return _IdQuestion;
        }

        public int getIdQuestionnnaire()
        {
            return _IdQuestionnnaire;
        }

      
    }
}