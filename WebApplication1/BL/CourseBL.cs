using FinalProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.classes;

namespace WebApplication1.BL
{
    public class CourseBL
    {
        public static CourseDAL courseDAL;
        public static List<Course> AnswerList;
        public CourseBL()
        {
            courseDAL = new CourseDAL();
        }
        public void AddCourse(int Id, String Name, int LecturerID)
        {
            courseDAL.AddCourse(Id, Name, LecturerID);
        }
        public void deleteCoursesById(int Id)
        {
            courseDAL.deleteCoursesById(Id);
        }
        public int getMaxIdCourse()
        {
            return courseDAL.maxIdCourse();
        }
        public List<Course> getCoursesByIdLecturer(int Id)
        {
            return courseDAL.getCoursesByIdLecturer(Id);
        }
        public String getNameById(int Id)
        {
            return courseDAL.getNameById(Id);
        }

    }
}