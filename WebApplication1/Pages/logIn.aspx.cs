using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BL;
using WebApplication1.classes;
using WebApplication1.Classes;

namespace WebApplication1.Pages
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        GlobalFunction global;
        static LecturerBL lectureBL;
        static StudentBL studentBl;
        private CourseBL courseBL;
        private CourseRegisterBL courseRegisterBL;
        public List<Course> listCourse;
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
            global = new GlobalFunction();

            colorCourses[0] = System.Drawing.Color.Plum;
            colorCourses[1] = System.Drawing.Color.Orchid;
            colorCourses[2] = System.Drawing.Color.DarkOrchid;
            colorCourses[3] = System.Drawing.Color.BlueViolet;
            colorCourses[4] = System.Drawing.Color.Purple;
            colorCourses[5] = System.Drawing.Color.DarkMagenta;
            colorCourses[6] = System.Drawing.Color.MediumOrchid;
            colorCourses[7] = System.Drawing.Color.DarkViolet;


            if (Session["id"] != null)
            {

                if (Session["userType"] != null && ((String)Session["userType"]).Equals("0"))
                {

                    listCourse = courseBL.getCoursesByIdLecturer(Convert.ToInt32((String)Session["id"]));// how i get id??
                }
                else if (Session["userType"] != null && ((String)Session["userType"]).Equals("1"))
                {
                    listCourse = courseRegisterBL.getCoursesByIdStudent(Convert.ToInt32((String)Session["id"]));// how i get id??
                }

                //Session["listCourse"] = listCourse;

            }


        }
        protected void existUser_Click(object sender, EventArgs e)
        {
            String img, name, email, password;
      
            email = Email.Value.ToString().Trim();
            password = pass.Value.ToString().Trim();
            if (email.Equals("") || password.Equals(""))
            {
                MissVall.Visible = true;
                MissVall.InnerText = "יש למלא את השדות סיסמה ודואר אלקטרוני*";
                return;
            }
            else
            {
                listStudent = studentBl.getStudentByPassword(password, email);
                listLecturer = lectureBL.getLecturerByPassword(password, email);
                if (listLecturer.Count != 0)
                {
                    Session["userType"] = "0";
                    Session["id"] = listLecturer[0]._Id.ToString();
                    name = listLecturer[0]._Name.ToString();
                    Session["Name"] = name;
                    Session["Image"] = listLecturer[0]._image.ToString();
                }
                else if (listStudent.Count != 0)
                {
                    Session["userType"] = "1";
                    Session["id"] = listStudent[0]._Id.ToString();
                    name = listStudent[0]._Name.ToString();
                    Session["Name"] = name;
                    Session["Image"] = listStudent[0]._image.ToString();

                }
                else
                {
                    MissVall.Visible = true;
                    MissVall.InnerText = "אינך קיים במערכת*";
                    return;
                }


                Response.Redirect("HomePage.aspx");
            }
        }

        protected void register_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        protected void ForgotPassword_Click(object sender, EventArgs e)
        {
            String password = "";
            String emailTo = Email.Value.ToString();
            if (emailTo.Equals(""))
            {
                MissVall.Visible = true;
                MissVall.InnerText = "חובה למלא את השדה דואר אלקטרוני*";
            }
            else
            {
                //if password exist in DB
                if (lectureBL.getLecturerByEmail(emailTo).Count > 0)
                {
                    password = lectureBL.getLecturerByEmail(emailTo)[0]._password.ToString();
                }
                else if (studentBl.getStudentByEmail(emailTo).Count > 0)
                {
                    password = studentBl.getStudentByEmail(emailTo)[0]._password.ToString();
                }
                else
                {
                    MissVall.Visible = true;
                    MissVall.InnerText = "אינך קיים במערכת*";
                    return;
                }
                string subject = "מערכת קליקר - שיחזור סיסמה";
                string body = "<body dir=\"rtl\"><h3>זוהי הודעה אוטומטית ממערכת קליקר</h3>";
                body += "<p>כתובת דואר אלקטרוני: " + Email.Value + "</p>";
                body += "<p>סיסמתך היא: "+password+"</p></body>";
                global.sendEmail(subject, body, emailTo);
            }
        }
    }
}