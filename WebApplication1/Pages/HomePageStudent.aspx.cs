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
using Clicker.BL;
using Clicker.Classes;


namespace Clicker.Pages
{
    public partial class HomePageStudent : System.Web.UI.Page
    {
        private const int MAX_LENGTH_NAME_COURSE = 15;
        static LecturerBL lectureBLs;
        static StudentBL studentBls;
        private static CourseBL courseBL;
        private static CourseRegisterBL courseRegisterBLs;
        private static GlobalFunction globals;

        public static List<Course> listCourses;
        static List<Student> listStudents;
        static List<Lecturer> listLecturers;
        public Color[] colorCourses;

        protected void Page_Load(object sender, EventArgs e)
        {
            globals = new GlobalFunction();
            lectureBLs = new LecturerBL();
            studentBls = new StudentBL();
            listStudents = new List<Student>();
            listLecturers = new List<Lecturer>();
            courseBL = new CourseBL();
            courseRegisterBLs = new CourseRegisterBL();
            listCourses = new List<Course>();
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

                //if (Session["userType"] != null && (Session["userType"]).Equals("0"))
                //{

                //    listCourses = courseBL.getCoursesByIdLecturer(Convert.ToInt32(Session["id"]));// get all courses of this lecturer
                //}
                //else 
                    if (Session["userType"] != null && (Session["userType"]).Equals("1"))
                {
                    listCourses = courseRegisterBLs.getCoursesByIdStudent(Convert.ToInt32(Session["id"]));// get all courses of this student
                    sessionInput.Value = Session["userType"].ToString();
                    
                }

                    UserNameLabels.InnerText = Session["Name"].ToString();
                //IMAGE--------------
                //userImages.ImageUrl = Session["Image"].ToString();


            }
            else // if no one connected
            {
                addCourseBtn.Style.Add("display", "none");
                removeCourseBtn.Style.Add("display", "none");
                logoutBtns.Style.Add("display", "none");
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
            int courseCode, userId;
            String userType;

            userType = (String)HttpContext.Current.Session["userType"];
            userId = Convert.ToInt32(HttpContext.Current.Session["id"]);

            //if (userType.Equals("0"))// lecturer
            //{
            //    try
            //    {
            //        courseName = courseInput;
            //    }
            //    catch (FormatException)
            //    {
            //        return "נסה שם קורס אחר.";
            //    }

            //    String tempName;
            //    int maxIdCourse;

            //    for (int i = 0; i < listCourses.Count; i++)// check if this course exist
            //    {

            //        tempName = listCourses[i].getName().Trim();
            //        if (tempName.Equals(courseInput))
            //        {
            //            return "שם הקורס כבר קיים";
            //        }
            //    }
            //    // add course to DB
            //    maxIdCourse = courseBL.getMaxIdCourse();
            //    courseBL.AddCourse(maxIdCourse + 1, courseInput, userId);
            //}
            //else
            if (userType.Equals("1"))// student
            {
                try
                {
                    courseCode = Convert.ToInt32(courseInput);
                }
                catch (FormatException)
                {
                    return "הכנס קוד קורס.";
                }

                int maxCourseId, maxCourseIdRegister;

                //lecturerId = courseBL.getIdLecturerByCourseId(courseCode);
                maxCourseId = courseBL.getMaxIdCourse();
                if (courseCode > maxCourseId)
                {
                    return "הקורס לא קיים במערכת.";
                }
                else// this course exist in courses table
                {
                    for (int i = 0; i < listCourses.Count; i++)// check if this course exist
                    {
                        if (listCourses[i].getId().ToString().Equals(courseInput))
                        {
                            return "שם הקורס כבר קיים";
                        }
                    }
                    //add course to courseRegister table
                    maxCourseIdRegister = courseRegisterBLs.maxIdCourseRegister();
                    courseRegisterBLs.AddCourseRegister(maxCourseIdRegister + 1, courseCode, userId);
                }
            }

            return ".הקורס נוסף בהצלחה";
        }

        //remov course
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static String removeCourse_click(String courseInput)
        {
            String nameCourse = courseInput;
            int idCourse;

            //if (HttpContext.Current.Session["userType"].Equals("0"))//lecurer
            //{
            //    for (int i = 0; i < listCourses.Count; i++)// check if this course exist
            //    {
            //        tempName = listCourses[i].getName().Trim();

            //        if (nameCourse.Equals(tempName))
            //        {
            //            idCourse = listCourses[i].getId();
            //            listCourses.RemoveAt(i);
            //            globals.removeLecurerCourseFromDB(idCourse); // remove Lecurer course
            //            return ".הקורס הוסר בהצלחה";
            //        }
            //    }
            //}
            //else
            if (HttpContext.Current.Session["userType"].Equals("1"))//student
            {
                try
                {
                    idCourse = Convert.ToInt32(nameCourse);
                }
                catch (FormatException)
                {
                    return "הכנס מספר קורס להסרה.";
                }
                for (int i = 0; i < listCourses.Count; i++)// check if this course exist
                {
                    int tempId;

                    tempId = listCourses[i].getId();//courseBL.getIdByIdLecturerAndCourseName(Convert.ToInt32(HttpContext.Current.Session["id"]), nameCourse);  /////////////////////// get id course by course name and lecturer id

                    if (tempId == idCourse)
                    {
                        courseRegisterBLs.deleteCourseRegisterByIdCourseAndStudent(idCourse, Convert.ToInt32(HttpContext.Current.Session["id"]));
                        return ".הקורס הוסר בהצלחה";
                    }

                }
            }

            // this course not exist
            return "הקורס לא קיים במערכת.";
        }



         [System.Web.Services.WebMethod(EnableSession = true)]
        public void goStock_Click(object sender, EventArgs e)
        {
            String id = courseId.Value;
            //HttpContext.Current.Response.Redirect("StockQuestionnaires.aspx?IdCourse=" + id);
            Response.Redirect("ShowQuestionnaire.aspx?IdCourse=" + id);
        }

    }
}