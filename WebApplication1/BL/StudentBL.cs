using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.classes;
using WebApplication1.DAL;

namespace WebApplication1.BL
{
    public class StudentBL
    {
        public static StudentDAL studentDAL;
        List<Student> accessoriesList;
        public StudentBL()
        {
            studentDAL = new StudentDAL();
        }
        public void AddNewStudent(int Id, String Name, String email, String image, String password)
        {
            studentDAL.AddNewStudent(Id, Name, email, image, password);
        }
        public void deletStudentById(int Id)
        {
            studentDAL.deleteStudentById(Id);
        }
        public int maxIdStudent()
        {
            return studentDAL.maxIdStudent();
        }
        public List<Student> getStudentByPassword(String password, String email)
        {
            return studentDAL.getStudentByPassword(password, email);
        }
        public List<Student> getStudentByEmail(String email)
        {
            return studentDAL.getStudentByEmail(email);
        }
    }
}