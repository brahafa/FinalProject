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

namespace WebApplication1.Pages
{
    public partial class WebForm2 : System.Web.UI.Page
    {

        private const int MAX_LENGTH_NAME_COURSE = 15;
        static LecturerBL lectureBL;
        static StudentBL studentBl;
        private static CourseBL courseBL;
        private static CourseRegisterBL courseRegisterBL;
        private static QuestionnaireBL questionnaireBL;
        private static QuestionBL questionBL;
        private static QuestionAskedBL questionAskedBL;
        private static List<Questionnaire> listQuestionnaire;
        private static List<Question> listQuestion;
        private static AnswerBL answerBL;
        public static List<Course> listCourse;
        static List<Student> listStudent;
        static List<Lecturer> listLecturer;
        public Color[] colorCourses;

        protected void Page_Load(object sender, EventArgs e)
        {

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

                if (Session["userType"] != null && ((String)Session["userType"]).Equals("0"))
                {

                    listCourse = courseBL.getCoursesByIdLecturer(Convert.ToInt32(Session["id"]));// get all courses of this lecturer
                }
                else if (Session["userType"] != null && (Session["userType"]).Equals("1"))
                {
                    listCourse = courseRegisterBL.getCoursesByIdStudent(Convert.ToInt32(Session["id"]));// get all courses of this student
                }

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
        public static string addCourse_click(String courseName)
        {

            String tempName = courseName;
            int userId, tempId;

            if (courseName.Length > MAX_LENGTH_NAME_COURSE)// name to long
            {
                return "שם הקורס ארוך מדי. מותר לכל היותר " + MAX_LENGTH_NAME_COURSE + " תווים.";
            }

            int maxIdCourse;

            userId = Convert.ToInt32(HttpContext.Current.Session["id"]);

            for (int i = 0; i < listCourse.Count; i++)// check if this course exist
            {
                tempName = listCourse[i].getName().Trim();
                tempId = listCourse[i].getLecturerID();

                if (tempName.Equals(courseName) && tempId == userId)
                {
                    return "שם הקורס כבר קיים";
                }

            }

            if (HttpContext.Current.Session["userType"].Equals("0"))//lecurer
            {
                // add course to DB
                maxIdCourse = courseBL.getMaxIdCourse();
                courseBL.AddCourse(maxIdCourse + 1, courseName, userId);
            }
            else//student
            {
                int courseID = 0, tempCourseID;
                tempCourseID = courseBL.getMaxIdCourse();
                try
                {
                    courseID = Convert.ToInt32(courseName);  /////////////////////// get id course by course name and lecturer id
                }
                catch (FormatException)
                {
                    return ".הכנס מספר קורס";
                }

                if (courseID > tempCourseID)
                {
                    return "הקורס לא קיים במערכת.";
                }
                maxIdCourse = courseRegisterBL.maxIdCourseRegister();
                courseRegisterBL.AddCourseRegister(maxIdCourse + 1, courseID, Convert.ToInt32(HttpContext.Current.Session["id"]));
            }

            return ".הקורס נוסף בהצלחה";
        }

        //remov course
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static String removeCourse_click(String courseName)
        {
            String tempName, nameCourse = courseName;
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
                        removeLecurerCourseFromDB(idCourse); // remove Lecurer course
                        return ".הקורס הוסר בהצלחה";
                    }
                }
            }
            else//student
            {
                try
                {
                    idCourse = Convert.ToInt32(nameCourse);
                }
                catch (FormatException)
                {
                    return "הכנס מספר קורס להסרה.";
                }
                for (int i = 0; i < listCourse.Count; i++)// check if this course exist
                {
                    int tempId;

                    tempId = listCourse[i].getId();  /////////////////////// get id course by course name and lecturer id

                    if (tempId == idCourse)
                    {
                        courseRegisterBL.deleteCourseRegisterByIdCourseAndStudent(idCourse, Convert.ToInt32(HttpContext.Current.Session["id"]));
                        return ".הקורס הוסר בהצלחה";
                    }

                }
            }

            // this course not exist
            return "הקורס לא קיים במערכת.";
        }

        //Method removes all information related to this course
        private static void removeLecurerCourseFromDB(int idCourse)
        {
            listQuestionnaire = new List<Questionnaire>();
            listQuestion = new List<Question>();
            questionnaireBL = new QuestionnaireBL();
            questionBL = new QuestionBL();
            answerBL = new AnswerBL();
            questionAskedBL = new QuestionAskedBL();

            listQuestionnaire = questionnaireBL.getAllQuestionnaireByIdCourse(idCourse);
            for (int i = 0; i < listQuestionnaire.Count; i++)
            {
                listQuestion = questionBL.getAllQuestionByQuestionnaire(listQuestionnaire[i].getId());
                for (int j = 0; j < listQuestion.Count; j++)
                {
                    questionAskedBL.deleteQuestionAskedByIdQuestion(listQuestion[j].getId()); // delete all QuestionAsked By IdQuestion
                    answerBL.deleteAnswerByIdQuestion(listQuestion[j].getId()); // delete all answer By IdQuestion
                }
                listQuestion = null;
                questionBL.deleteQuestionByQuestionnaire(listQuestionnaire[i].getId()); //delete all Question By Questionnaire
            }
            listQuestionnaire = null;
            questionnaireBL.deleteQuestionnaireByIdCours(idCourse);//delete all Questionnaire By IdCours

            courseRegisterBL.deleteCourseRegisterByIdCourse(idCourse);//delete all CourseRegister By IdCourse

            courseBL.deleteCoursesById(idCourse);//delete course from DB

            //"free" object
            questionnaireBL = null;
            questionBL = null;
            answerBL = null;
            questionAskedBL = null;
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