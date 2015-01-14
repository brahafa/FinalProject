using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using WebApplication1.BL;
using System.Web.UI.WebControls;

namespace WebApplication1.Pages
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        LecturerBL lecturerBL;
        String FileName;
        String passwordStr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                UserNameLabel.InnerText = Session["Name"].ToString();


                FileName = Session["Image"].ToString();
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/images/"));
                List<ListItem> files = new List<ListItem>();
                string fileName;
                foreach (string filePath in filePaths)
                {
                    fileName = Path.GetFileName(filePath);
                    ImagePath.Value = FileName.ToString();
                    InputPassword.Value = pass.Value.ToString();
                    //fileName = FileUpload1.GetRouteUrl();
                    files.Add(new ListItem(FileName, FileName));
                    break;
                }

                GridView1.DataSource = FileName;
               // GridView1.DataBind();

            }
            lecturerBL = new LecturerBL();
            if (pass.Value.ToString().Equals(""))
            {
                pass.Value = InputPassword.Value.ToString();
                passwordStr = pass.Value.ToString();
            }
            else
            {
                passwordStr = InputPassword.Value.ToString();
            }

        }
    
        protected void Register_Click(object sender, EventArgs e)
        {
            String name, email, image;
            int index;
            image = "~/images/" + ImagePath.Value.ToString();
            index = lecturerBL.maxIdLecturer() + 1;
            name = contactName.Value.ToString();
            email = Email.Value.ToString();


            lecturerBL.AddNewLecturer(index, name, email, image, passwordStr);
        }

        protected void Upload(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/images/") + FileName);

                string[] filePaths = Directory.GetFiles(Server.MapPath("~/images/"));
                List<ListItem> files = new List<ListItem>();
                string fileName;
                foreach (string filePath in filePaths)
                {
                    fileName = Path.GetFileName(filePath);
                    ImagePath.Value = FileName.ToString();
                    InputPassword.Value = pass.Value.ToString();
                    //fileName = FileUpload1.GetRouteUrl();
                    files.Add(new ListItem(FileName, "~/images/" + FileName));
                    break;
                }
                GridView1.DataSource = files;
                GridView1.DataBind();

                if (pass.Value != null)
                {
                    InputPassword.Value = pass.Value.ToString();
                }
         
        
            }
        }

    }
}