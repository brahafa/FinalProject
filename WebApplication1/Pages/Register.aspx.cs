using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BL;
using WebApplication1.Classes;

namespace WebApplication1.Pages
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        LecturerBL lecturerBL;
        StudentBL studentBL;
        GlobalFunction global;
        Validations valid;
        protected void Page_Load(object sender, EventArgs e)
        {
            lecturerBL = new LecturerBL();
            studentBL = new StudentBL();
            global = new GlobalFunction();
            valid = new Validations();
        }

        protected void Register_Click(object sender, EventArgs e)
        {
            String name, email, image, password, password1;
            int index;
            //save and display image///
            int contentLength = avatarUpload.PostedFile.ContentLength;//You may need it for validation
            string contentType = avatarUpload.PostedFile.ContentType;//You may need it for validation
            string fileName = avatarUpload.PostedFile.FileName;
            if (avatarUpload.PostedFile.ContentLength == 0)
            {
                fileName = "profil.jpg";
            }
            else
            {
                if (IsImage(contentLength, contentType, fileName) == false)
                {
                        sendErrorMesege("הקובץ אינו תואם*");
                        return;
                    
                }
            }
            avatarUpload.PostedFile.SaveAs(Server.MapPath("~/images/") + fileName);//save image in folder
            image = "~/images/" + fileName.ToString();
            name = contactName.Value.ToString();
            email = Email.Value.ToString();
            password = pass.Value.ToString();
            password1 = pass1.Value.ToString();

            if (valid.checkPassword(password) == false)
            {
                sendErrorMesege("סיסמה חייבת להכיל בין 5 ל 8 תוים*");
                return;
            }
            if (valid.veriftyPassword(password,password1) == false)
            {
                sendErrorMesege("אמת את סיסמתך*");
                return;
            }
            if (valid.validName(name) == false)
            {
                sendErrorMesege("הכנס שם עד 15 תוים*");
                return;
            }
            if(valid.existEmail(email)==false){
                sendErrorMesege("כתובת דואר אלקטרוני לא תקין*");
                return;
            }
            if (selected_Type.Value.Equals("-1"))
            {
                sendErrorMesege("בחר סוג משתמש*");
                return;
            }
            Session["userType"] = selected_Type.Value.ToString();
            if(((String)Session["userType"]).Equals("0"))
            {
                index = lecturerBL.maxIdLecturer() + 1;
                name= name + "  "+selected_Degree.Items.FindByValue(selected_Degree.SelectedIndex.ToString());
                lecturerBL.AddNewLecturer(index, name, email, image, password);
            }
            else
            {
                index = studentBL.maxIdStudent() + 1;
                studentBL.AddNewStudent(index, name, email, image, password);


            }
            initSessions(name, image, index);
            //send email 
            sendRegisterEmail(image, name,email,password);
            Response.Redirect("HomePage.aspx");
        }

        public void initSessions(String name , String image, int index )
        {
            Session["id"] = index;
            Session["Name"] = name;
            Session["Image"] = image;
        }

        //send email whith the register ditayls
        public void sendRegisterEmail(String image, String name, String email, String password)
        {
            string emailTo = Email.Value;
            string subject = "מערכת קליקר - אישור הרשמה";
            string body = "<body dir=\"rtl\"><h3>זוהי הודעה אוטומטית ממערכת קליקר</h3>";
            body += "<p>לנוחיותך מצורפים פרטי ההתחברות.</p>";
            body += "<p>כתובת דואר אלקטרוני: " + Email.Value + "</p>";
            body += "<p>סיסמה: " + pass.Value + "</p></body>";
            global.sendEmail(subject, body, emailTo);
        }
        public void sendErrorMesege(String mesege)
        {
                errMesege.Visible = true;
                errMesege.InnerText = mesege;
        }

        public bool IsImage(int contentLength, string contentType, string fileName)  
    {
        //-------------------------------------------
        //  Check the image mime types
        //-------------------------------------------
        if (contentType.ToLower() != "image/jpg" &&
                    contentType.ToLower() != "image/jpeg" &&
                    contentType.ToLower() !=  "image/pjpeg" &&
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