using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Site.App_Code
{
    public class Mail
    {
        public static string SendEmail(string i_sTo, string i_sSubject, string i_sEmailBody, bool i_bIsHtml)
        {
            string o_sStatus = "ok";
            try
            {
                string i_sSMTPHost = ConfigurationManager.AppSettings["SMTPMail"].ToString();
                string i_sPassword = ConfigurationManager.AppSettings["MailPassword"].ToString();
                string i_sFrom = ConfigurationManager.AppSettings["MailFrom"].ToString();
                MailMessage oMail = new MailMessage();
                oMail.To.Add(i_sTo);
                oMail.From = new MailAddress(i_sFrom);
                oMail.Subject = i_sSubject;
                oMail.IsBodyHtml = true;
                oMail.Body = i_sEmailBody;
                SmtpClient smtpmail = new SmtpClient("relay-hosting.secureserver.net", 25);
                NetworkCredential Credential = new NetworkCredential(); /* New added for testing */
                Credential.UserName = i_sFrom;
                Credential.Password = i_sPassword;
                smtpmail.EnableSsl = false; /* New added for testing */
                smtpmail.UseDefaultCredentials = false;
                smtpmail.Credentials = Credential;
                smtpmail.Port = 80;/* New added for testing */
                smtpmail.Host = i_sSMTPHost;
                try
                {
                    smtpmail.Send(oMail);
                 
                }
                catch (Exception ex2)
                {
                    o_sStatus = "error2:" + ex2.Message + "\nTrace:" + ex2.StackTrace;

                }

            }
            catch (Exception ex1)
            {
                o_sStatus = "error1:" + ex1.Message + "\ntrace:" + ex1.Message;
            }
            return o_sStatus;
        }
        public static string SendEmailAttachment(string i_sTo, string i_sFrom, string i_sSubject, string i_sEmailBody, bool i_bIsHTML, string sPath)
        {
            string o_sStatus = "<br/>ok";
            try
            {
                string i_sSMTPHost = ConfigurationManager.AppSettings["HostMail"].ToString();

                string pass = ConfigurationManager.AppSettings["password"].ToString();

                MailMessage oMail = new MailMessage();
                oMail.To.Add(i_sTo);
                oMail.From = new MailAddress(i_sFrom);
                oMail.Subject = i_sSubject;

                //  Attachment attach = new Attachment(sPath, "application/pdf");
                //  oMail.Attachments.Add(attach);

                if (i_bIsHTML)
                {
                    oMail.IsBodyHtml = true;
                }
                else
                {
                    oMail.IsBodyHtml = false;
                }
                oMail.Body = i_sEmailBody;
                //foreach (Attachment m_att in i_msg.Attachments)
                //{
                //    oMail.Attachments.Add(m_att);
                //}
                SmtpClient SmtpMail = new SmtpClient("relay-hosting.secureserver.net", 25);

                NetworkCredential Credential = new NetworkCredential(); /* New added for testing */
                Credential.UserName = i_sFrom;
                Credential.Password = pass;
                SmtpMail.Port = 80; /* New added for testing */
                SmtpMail.EnableSsl = false; /* New added for testing */
                SmtpMail.UseDefaultCredentials = false;
                SmtpMail.Credentials = Credential;
                SmtpMail.Port = 80;/* New added for testing */
                SmtpMail.Host = i_sSMTPHost;

                try
                {
                    SmtpMail.Send(oMail);
                }
                catch (Exception ex2)
                {
                    // LogError("SendEmail1a", "", ex2.Message, ex2.StackTrace);
                    o_sStatus = "error2:" + ex2.Message + "\ntrace:" + ex2.StackTrace;
                    // LogErrors("cCustoMain.cs", ex2.StackTrace.ToString(), ex2.Message.ToString(), "SendEmail()");

                }
            }
            catch (Exception ex1)
            {
                o_sStatus = "error1:" + ex1.Message + "\ntrace:" + ex1.StackTrace;

            }
            return o_sStatus;
        }
        public static string SendGmail(string i_sTo, string i_sSubject, string i_sEmailBody)
        {
            string result = "ok";
            try
            {
                string i_sSMTPHost = "smtp.gmail.com";
                MailMessage oMail = new MailMessage();
                oMail.To.Add(i_sTo);
                oMail.From = new MailAddress("testinga4mail@gmail.com");
                oMail.Subject = i_sSubject;
                oMail.IsBodyHtml = true;
                oMail.Body = i_sEmailBody;
                SmtpClient smtpmail = new SmtpClient();
                smtpmail.Credentials = new NetworkCredential("testinga4mail@gmail.com", "a4technology"); /* New added for testing */
                smtpmail.Port = 25; /* New added for testing */
                smtpmail.EnableSsl = true; /* New added for testing */
                smtpmail.DeliveryMethod = SmtpDeliveryMethod.Network; /* New added for testing */
                smtpmail.Host = i_sSMTPHost;
                try
                {
                    smtpmail.Send(oMail);

                }
                catch (Exception)
                {
                    result = "Email not Sent";
                    throw;
                    
                }
             

            }
            catch (Exception ex)
            {
                result = "Email not Sent";
            }
            return result;
        }
    }
}