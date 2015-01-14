using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.classes
{
    public class Student
    {
            public int _Id;
            public String _Name;
            public String _email;
            public String _image;
            public String _password;



            public Student(int Id, String Name, String email, String image, String password)
            {
                _Id = Id;
                _Name = Name;
                _email = email;
                _image = image;
                _password=password;

            }
    }
}