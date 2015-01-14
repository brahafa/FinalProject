using WebApplication1.classes;
using WebApplication1.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.classes;


namespace WebApplication1.BL
{
    public class CourseRegisterBL
    {
        public static CourseRegisterDAL courseRegisterDAL;
        public static List<CourseRegister> courseRegisterList;
        public CourseRegisterBL()
        {
            courseRegisterDAL = new CourseRegisterDAL();
        }
        public void AddCourseRegister(int Id, int IdCours, int IdStudent)
        {
            courseRegisterDAL.AddCourseRegister(Id, IdCours, IdStudent);
        }

                //delet all CourseRegister By IdCourse
        public void deleteCourseRegisterByIdCourse(int IdCours)
        {
            courseRegisterDAL.deleteCourseRegisterByIdCourse(IdCours);
        }
        public List<Course> getCoursesByIdStudent(int Id)
        {
            return courseRegisterDAL.getCoursesByIdStudent(Id);
        }
        public int maxIdCourseRegister()
        {
            return courseRegisterDAL.maxIdCourseRegister();
        }
        public void deleteCourseRegisterByIdCourseAndStudent(int IdCours, int IdStudent)
        {
            courseRegisterDAL.deleteCourseRegisterByIdCourseAndStudent(IdCours, IdStudent);
        }
    }
}