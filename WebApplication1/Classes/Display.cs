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


        public Display(int Id, int IdQuestion)
        {
            _Id = Id;
            _IdQuestion = IdQuestion;

        }

        public int getId()
        {
            return _Id;
        }

        public int getIdQuestion()
        {
            return _IdQuestion;
        }

      
    }
}