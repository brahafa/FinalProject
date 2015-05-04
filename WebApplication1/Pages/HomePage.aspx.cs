using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BL;
using WebApplication1.classes;
using WebApplication1.Classes;

namespace WebApplication1.Pages
{
    public partial class WebForm2 : System.Web.UI.Page
    {

        private const int MAX_LENGTH_NAME_COURSE = 15;
        static LecturerBL lectureBL;
        static StudentBL studentBl;
        private static CourseBL courseBL;
        private static CourseRegisterBL courseRegisterBL;
        private static GlobalFunction global;

        public static List<Course> listCourse;
        static List<Student> listStudent;
        static List<Lecturer> listLecturer;
        public Color[] colorCourses;

        protected void Page_Load(object sender, EventArgs e)
        {
            global = new GlobalFunction();
            lectureBL = new LecturerBL();
            studentBl = new StudentBL();
            listStudent = new List<Student>();
            listLecturer = new List<Lecturer>();
            courseBL = new CourseBL();
            courseRegisterBL = new CourseRegisterBL();
            listCourse = new List<Course>();
            colorCourses = new Color[9];

            colorCourses[0] = System.Drawing.Color.LightSalmon;
            colorCourses[1] = System.Drawing.Color.Brown;
            colorCourses[2] = System.Drawing.Color.Coral;
            colorCourses[3] = System.Drawing.Color.Crimson;
            colorCourses[4] = System.Drawing.Color.DarkRed;
            colorCourses[5] = System.Drawing.Color.Red;
            colorCourses[6] = System.Drawing.Color.OrangeRed;
            colorCourses[7] = System.Drawing.Color.Tomato;
            colorCourses[8] = System.Drawing.Color.IndianRed;



            if (Session["id"] != null)
            {

                if (Session["userType"] != null && (Session["userType"]).Equals("0"))
                {

                    listCourse = courseBL.getCoursesByIdLecturer(Convert.ToInt32(Session["id"]));// get all courses of this lecturer
                }
                //else if (Session["userType"] != null && (Session["userType"]).Equals("1"))
                //{
                //    listCourse = courseRegisterBL.getCoursesByIdStudent(Convert.ToInt32(Session["id"]));// get all courses of this student
                //}

                UserNameLabel.InnerText = Session["Name"].ToString();
                userImage.ImageUrl = Session["Image"].ToString();


            }
            else // if no one connected
            {
                addCourseBtn.Style.Add("display", "none");
                removeCourseBtn.Style.Add("display", "none");
                logoutBtn.Style.Add("display", "none");
            }



        }

        // on click course button
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void CourseButton_click(String idCourse)
        {
            // Response.Redirect("StockQuestionnaires.aspx?IdCourse=" + idCourse.ToString());
        }


        //free session and redirect to login page.
        protected void logout_click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("logIn.aspx");
        }

        //add new course 
       [System.Web.Services.WebMethod(EnableSession = true)]
        public static string addCourse_click(String courseInput)
        {

            if (courseInput.Length > MAX_LENGTH_NAME_COURSE)// name to long
            {
                return "שם הקורס ארוך מדי. מותר לכל היותר " + MAX_LENGTH_NAME_COURSE + " תווים.";
            }

            //student(courseInput=courseCode) or lecturer(courseInput=courseName)
            int userId;
            String courseName, userType;

            userType = (String)HttpContext.Current.Session["userType"];
            userId = Convert.ToInt32(HttpContext.Current.Session["id"]);

            if (userType.Equals("0"))// lecturer
            {
                try
                {
                    courseName = courseInput;
                }
                catch (FormatException)
                {
                    return "נסה שם קורס אחר.";
                }

                String tempName;
                int maxIdCourse;

                for (int i = 0; i < listCourse.Count; i++)// check if this course exist
                {

                    tempName = listCourse[i].getName().Trim();
                    if (tempName.Equals(courseInput))
                    {
                        return "שם הקורס כבר קיים";
                    }
                }
                // add course to DB
                maxIdCourse = courseBL.getMaxIdCourse();
                courseBL.AddCourse(maxIdCourse + 1, courseInput, userId);
            }
            //else// student
            //{
            //    try
            //    {
            //        courseCode = Convert.ToInt32(courseInput);
            //    }
            //    catch (FormatException)
            //    {
            //        return "הכנס קוד קורס.";
            //    }

            //    int maxCourseId, maxCourseIdRegister;

            //    //lecturerId = courseBL.getIdLecturerByCourseId(courseCode);
            //    maxCourseId = courseBL.getMaxIdCourse();
            //    if (courseCode > maxCourseId)
            //    {
            //        return "הקורס לא קיים במערכת.";
            //    }
            //    else// this course exist in courses table
            //    {
            //        for (int i = 0; i < listCourse.Count; i++)// check if this course exist
            //        {
            //            if (listCourse[i].getId().ToString().Equals(courseInput))
            //            {
            //                return "שם הקורס כבר קיים";
            //            }
            //        }
            //        //add course to courseRegister table
            //        maxCourseIdRegister = courseRegisterBL.maxIdCourseRegister();
            //        courseRegisterBL.AddCourseRegister(maxCourseIdRegister + 1, courseCode, userId);
            //    }
            //}

            return ".הקורס נוסף בהצלחה";
        }

        //remov course
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static String removeCourse_click(String courseInput)
        {
            String tempName, nameCourse = courseInput;
            int idCourse;

            if (HttpContext.Current.Session["userType"].Equals("0"))//lecurer
            {
                for (int i = 0; i < listCourse.Count; i++)// check if this course exist
                {
                    tempName = listCourse[i].getName().Trim();

                    if (nameCourse.Equals(tempName))
                    {
                        idCourse = listCourse[i].getId();
                        listCourse.RemoveAt(i);
                        global.removeLecurerCourseFromDB(idCourse); // remove Lecurer course
                        return ".הקורס הוסר בהצלחה";
                    }
                }
            }
            //else//student
            //{
            //    try
            //    {
            //        idCourse = Convert.ToInt32(nameCourse);
            //    }
            //    catch (FormatException)
            //    {
            //        return "הכנס מספר קורס להסרה.";
            //    }
            //    for (int i = 0; i < listCourse.Count; i++)// check if this course exist
            //    {
            //        int tempId;

            //        tempId = listCourse[i].getId();//courseBL.getIdByIdLecturerAndCourseName(Convert.ToInt32(HttpContext.Current.Session["id"]), nameCourse);  /////////////////////// get id course by course name and lecturer id

            //        if (tempId == idCourse)
            //        {
            //            courseRegisterBL.deleteCourseRegisterByIdCourseAndStudent(idCourse, Convert.ToInt32(HttpContext.Current.Session["id"]));
            //            return ".הקורס הוסר בהצלחה";
            //        }

            //    }
            //}

            // this course not exist
            return "הקורס לא קיים במערכת.";
        }



        // [System.Web.Services.WebMethod(EnableSession = true)]
        public void goStock_Click(object sender, EventArgs e)
        {
            String id = courseId.Value;
            //HttpContext.Current.Response.Redirect("StockQuestionnaires.aspx?IdCourse=" + id);
            Response.Redirect("StockQuestionnaires.aspx?IdCourse=" + id);
        }

    }

}