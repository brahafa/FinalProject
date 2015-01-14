using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace WebApplication1.Classes
{
    public class GlobalFunction
    {
        public void sendEmail(String subject, String body, String emailTo)
        {
            string smtpAddress = "smtp.gmail.com";
            int portNumber = 587;
            bool enableSSL = true;

            string emailFrom = "brachafab@gmail.com";
            string emailpassword = "0503389483";
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, emailpassword);
                    smtp.EnableSsl = enableSSL;
                      try
                      {
                  
                        smtp.Send(mail);
                      }
                      catch (Exception ex)
                      {
                          Console.WriteLine(ex.Message);
                      }
                }


            }
        }
    }
}