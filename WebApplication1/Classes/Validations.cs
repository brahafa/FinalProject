using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WebApplication1.BL;

namespace WebApplication1.Classes
{
    public class Validations
    {

        public bool checkPassword(string password)
        {
            if (password.Length < 6 || password.Length > 8)
            {
                return false;
            } 
            else
            {
                return true;
            }
        }


        public bool veriftyPassword(String password,String password1)
        {
            if (password.Equals("") == true || password1.Equals("") == true || password1.Equals(password) == false)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool validName(string name)
        {
            if (name.Length > 16)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool validEmail(String Email)
        {
            if (Email.Length == 0 || !(Regex.IsMatch(Email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$")))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool existEmail(String email)
        {
            LecturerBL lectureBl = new LecturerBL();
            StudentBL studentBL = new StudentBL();
            if (lectureBl.getLecturerByEmail(email).Count > 0 || (studentBL.getStudentByEmail(email).Count > 0))
            {
                return false;
            }
            return true;
        }
  }
}
