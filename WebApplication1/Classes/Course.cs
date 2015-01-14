using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.classes
{
    public class Course
    {
        private int _Id;
        private String _Name;
        private int _LecturerID;


        public Course(int Id, String Name, int LecturerID)
        {
            _Id = Id;
            _Name = Name;
            _LecturerID = LecturerID;
            
        }

        public int getId()
        {
            return _Id;
        }

        public int getLecturerID()
        {
            return _LecturerID;
        }

        public String getName()
        {
            return _Name;
        }
    }
}