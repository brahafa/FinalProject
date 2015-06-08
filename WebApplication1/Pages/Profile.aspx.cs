using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Clicker.BL;
using System.Web.UI.WebControls;
using Clicker.Classes;
using System.Text.RegularExpressions;

namespace Clicker.Pages
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        LecturerBL lecturerBL;
        StudentBL studentBL;
        Validations valid;
        GlobalFunction global;
        int indexDegree=0;

        protected void Page_Load(object sender, EventArgs e)
        {
            valid = new Validations();
            global = new GlobalFunction();
            lecturerBL = new LecturerBL();
            studentBL = new StudentBL();
            selected_Type.Disabled = true;
            Email.Disabled = true;
            Avatar.ImageUrl = Session["Image"].ToString();

            if (Session["id"] != null)
            {
                //UserName.Value = Session["Name"].ToString();
                Email.Value = Session["email"].ToString();
                Avatar.ImageUrl = Session["Image"].ToString();
               
                if (Session["userType"].ToString().Equals("0"))
                {
                    if (Session["degree"].ToString().Trim().Equals("מר/ת"))
                    {
                        indexDegree = 0;
                    }
                    else if (Session["degree"].ToString().Trim().Equals("דוקטור"))
                    {
                        indexDegree = 1;
                    }
                    else if (Session["degree"].ToString().Trim().Equals("פרופסור"))
                    {
                        indexDegree = 2;
                    }
                    //selected_Degree.Value = indexDegree.ToString();
                    selected_Degree.Style.Add("display", "inline");
                }
                else
                {
                    selected_Degree.Style.Add("display", "none");
                    selected_Type.Style.Add("display", "none");

                }
               // selected_Type.Value = Session["userType"].ToString();
            }
        


        }
    
        protected void Register_Click(object sender, EventArgs e)
        {
            String name="", email, image, password, password1;
            String degree = "";
            //save and display image///
            int contentLength = avatarUpload.PostedFile.ContentLength;//You may need it for validation
            string contentType = avatarUpload.PostedFile.ContentType;//You may need it for validation
            string fileName = avatarUpload.PostedFile.FileName;
            if (avatarUpload.PostedFile.ContentLength == 0)
            {
                fileName = Session["Image"].ToString();
                image = fileName.ToString();

            }
            else
            {
                if (IsImage(contentLength, contentType, fileName) == false)
                {
                    sendErrorMesege("הקובץ אינו תואם*");
                    return;
                }
                avatarUpload.PostedFile.SaveAs(Server.MapPath("../images/") + fileName);//save image in folder
                image = "../images/" + fileName.ToString();
                Session["Image"] = image;

            }
            name = UserName.Value.ToString().Trim();
            email = Email.Value.ToString();
            password = pass.Value.ToString();
            password1 = pass1.Value.ToString();

            if (name.Equals(""))
            {
                name = Session["Name"].ToString();
            }

            else if (valid.validName(name) == false)
            {
                sendErrorMesege("הכנס שם עד 15 תוים*");
                return;
            }


            if (valid.checkPassword(password) == false)
            {
                sendErrorMesege("סיסמה חייבת להכיל בין 5 ל 8 תוים*");
                return;
            }
            if (valid.veriftyPassword(password, password1) == false)
            {
                sendErrorMesege("אמת את סיסמתך*");
                return;
            }


           
            //if (!Session["userType"].ToString().Equals("-1"))
            //{
            //    Session["userType"] = selected_Type.Value.ToString();
            //}
            if (((String)Session["userType"]).Equals("0"))
            {
                if (selected_Degree.Value.Equals("-1"))
                {
                    sendErrorMesege("בחר תואר*");
                    return;
                }
               // selected_Degree.Value = Session["Degree"].ToString();
              degree = selected_Degree.Items.FindByValue(selected_Degree.Value.ToString()).ToString();
               // degree = selected_Degree.Items.FindByValue("0").ToString();

                // name = name.Trim() + "  " + selected_Degree.Items.FindByValue(selected_Degree.SelectedIndex.ToString());
             //  degree = selected_Degree.Value.ToString();
                lecturerBL.UpdateLecturer(Convert.ToInt32(Session["id"].ToString()), name, image, password, degree);
            
            }
            else
            {
                //index = studentBL.maxIdStudent() + 1;
                studentBL.UpdateStudent(Convert.ToInt32(Session["id"].ToString()), name, image, password);
            }
            initSessions(name, image, email, degree);

            Response.Redirect("HomePage.aspx");
        }
        public void initSessions(String name, String image,  String email, String degree)
        {
            Session["Name"] = name;
            Session["Image"] = image;
            Session["email"] = email;
            Session["degree"] = degree;
        }
        public void sendErrorMesege(String mesege)
        {
            errMesege.Visible = true;
            errMesege.InnerText = mesege;
        }

        public int sendRegisterEmail(String image, String name, String email, String password)
        {
            int flagErr = 0;
            string emailTo = Email.Value;
            string subject = "מערכת קליקר - אישור הרשמה";
            string body = "<body dir=\"rtl\"><h3>זוהי הודעה אוטומטית ממערכת קליקר</h3>";
            body += "<p>לנוחיותך מצורפים שינוי פרטיך האישיים.</p>";
            body += "<p>כתובת דואר אלקטרוני: " + Email.Value + "</p>";
            body += "<p>סיסמה: " + pass.Value + "</p></body>";
            flagErr = global.sendEmail(subject, body, emailTo);
            return flagErr;
        }

        public bool IsImage(int contentLength, string contentType, string fileName)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (contentType.ToLower() != "image/jpg" &&
                        contentType.ToLower() != "image/jpeg" &&
                        contentType.ToLower() != "image/pjpeg" &&
                        contentType.ToLower() != "image/gif" &&
                        contentType.ToLower() != "image/x-png" &&
                        contentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(fileName).ToLower() != ".jpg"
                && Path.GetExtension(fileName).ToLower() != ".png"
                && Path.GetExtension(fileName).ToLower() != ".gif"
                && Path.GetExtension(fileName).ToLower() != ".jpeg")
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!avatarUpload.PostedFile.InputStream.CanRead)
                {
                    return false;
                }

                if (avatarUpload.PostedFile.ContentLength < 512)
                {
                    return false;
                }

                byte[] buffer = new byte[512];
                avatarUpload.PostedFile.InputStream.Read(buffer, 0, 512);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using (var bitmap = new System.Drawing.Bitmap(avatarUpload.PostedFile.InputStream))
                {
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


    }
}