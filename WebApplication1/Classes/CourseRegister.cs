using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.classes
{
    public class CourseRegister
    {
           private int _Id;
            private int _IdCours;
            private int _IdStudent;


            public CourseRegister(int Id, int IdCours, int IdStudent, int Permit)
            {
                _Id = Id;
                _IdCours = IdCours;
                _IdStudent = IdStudent;
            }
    }
}