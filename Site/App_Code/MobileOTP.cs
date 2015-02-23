using ASPSnippets.SmsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Twilio;
using SmsClient;
using System.IO;
using System.Text;

namespace Site.App_Code
{
    public class MobileOTP
    {


        public static void SendMessage()
        {
            SendSms sms = new SendSms();
            //SMS.APIType = SMSGateway.Site2SMS;
            //SMS.MashapeKey = "IkFKCW7Gf5msh3dkgDcBuHQG5vxCp1XHYkMjsnqEKiqb729VC4";
            //SMS.Username = "9594460614";
            //SMS.Password = "dharam6990";
            //SMS.SendSms("9594460614","OTP-23456");
            string yourmobilenumber = "9594460614";
            string yourpassword = "dharmendra";
            string body = "OTP-23456";
            string recipientNo = "919594460614";
            string status = sms.send(yourmobilenumber, yourpassword, recipientNo, body);
            if (status == "1")
            {
                //("Message Send");
            }
            else if (status == "2")
            {
                //("No Internet Connection");
            }
            else
            {
                //("Invalid Login Or No Internet Connection");
            }

        }
    }


}