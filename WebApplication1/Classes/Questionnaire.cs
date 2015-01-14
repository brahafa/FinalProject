using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.classes
{
    public class Questionnaire
    {
           private int _Id;
           private String _Name;
           private int _IdCours;
           private int _Permit;


            public Questionnaire(int Id, String Name, int  IdCours, int Permit)
            {
                _Id = Id;
                _Name = Name;
                _IdCours = IdCours;
                _Permit = Permit;
            }

            public int getId()
            {
                return _Id;
            }

            public String getName()
            {
                return _Name;
            }

            public int getIdCours()
            {
                return _IdCours;
            }

            public int getPermit()
            {
                return _Permit;
            }
    }
}