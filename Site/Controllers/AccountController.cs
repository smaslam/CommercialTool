using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommTool;
using System.IO;
using Site.Models;
using Site.App_Code;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace Site.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        //-----------Register  New Account------------//
        public ActionResult Register(string Email)
        {
            Register objReg = new Register();
            if (Email != null)
            {

                objReg.EmailID = Email.ToString();
            }

            return View(objReg);
        }
        [HttpPost]
        public ActionResult Create(Register register)
        {
            try
            {

                cLoginUser objLogin = cLoginUser.Create();
                objLogin.sEmailID = register.EmailID;
                objLogin.sPassword = Cryptography.Encryptdata(register.Password);
                objLogin.sUserName = register.UserID;
                objLogin.bFirstTime = false;
                objLogin.Save();

                cUserProfile objProfile = cUserProfile.Create();
                objProfile.sFirstName = register.FirstName;
                objProfile.sLastName = register.LastName;
                objProfile.sMobileNo = register.Mobile;
                objProfile.objLoginUser.iObjectID = objLogin.iID;
                objProfile.Save();

                cVerfication objVerification = cVerfication.Create();
                objVerification.bEmailVerified = false;
                objVerification.bMobileVerified = false;
                objVerification.objLoginUser.iObjectID = objLogin.iID;
                objVerification.Save();
                SaveFreePlan(Convert.ToInt32(objLogin.iID));
                RegisterVerificationMail(register.FirstName, register.LastName, register.EmailID, "HGASDHHJASGAGHSJASGYASGHAGSY");



            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("VeificationMessage");

        }

        public ActionResult VeificationMessage()
        {
            return View();
        }
        public void RegisterVerificationMail(string FirstName, string LastName, string Email, string HashCode)
        {
            string strBody = "";
            string mailId = Cryptography.Encryptdata(Email);
            string HasCodes = Cryptography.Encryptdata(HashCode);
            string link = "http://182.74.6.162/CommTool/Account/AccountVerification?Email=" + mailId + "&HashCode=" + HasCodes + "";
            string Links = "<b><a target='_blank' href=" + link + ">Click here to verify your account.</a></b>";
            StreamReader sr = new StreamReader(HttpContext.Server.MapPath(HttpContext.Request.ApplicationPath + "/App_Data/RegisterVerification.html"));
            strBody = sr.ReadToEnd();
            sr.Close();
            strBody = strBody.Replace("#Name#", FirstName + " " + LastName);
            strBody = strBody.Replace("#Link#", Links);
            Mail.SendGmail(Email, "Verification Link", strBody);

        }
        public ActionResult AccountVerification(string Email, string HashCode)
        {

            string sEmail = Cryptography.Decryptdata(Email);
            string sHashCode = Cryptography.Decryptdata(HashCode);

            List<cLoginUser> objLogin = cLoginUser.Find(" sEmailID = " + sEmail);
            if (objLogin.Count > 0)
            {
                List<cVerfication> objVerify = cVerfication.Find(" objLoginUser = " + objLogin[0].iID.ToString());
                if (objVerify.Count > 0)
                {
                    if (objVerify[0].bEmailVerified == false)
                    {
                        objVerify[0].bEmailVerified = true;
                        objVerify[0].Save();

                        return RedirectToAction("Login");
                    }
                    else
                    {
                        //Email is not verified yet please check your mail.
                    }
                }
            }
            else
            {
                //Email id does not exist please register first.
            }
            return RedirectToAction("Error", new { ErrorCode = "2" });
        }

        //-----------Login & Logout Action------------//

        [HttpGet]
        public ActionResult Login()
        {
            // MobileOTP.SendMessage();
            return View();

        }
        [HttpPost]
        public ActionResult Login(Login login)
        {
            System.Threading.Thread.Sleep(1000);
            List<cLoginUser> objLogin = cLoginUser.Find(" sEmailID = " + login.EmailID.ToString());
            if (objLogin.Count > 0)
            {
                List<cVerfication> objVerify = cVerfication.Find(" objLoginUser = " + objLogin[0].iID.ToString());
                if (objVerify.Count > 0)
                {
                    if (objVerify[0].bEmailVerified == true)
                    {
                        if (Cryptography.Decryptdata(objLogin[0].sPassword.ToString()) == login.Password.ToString().Trim())
                        {
                            Session["UserID"] = objLogin[0].iID;
                            if (objLogin[0].bFirstTime == true)
                            {
                                return Content("1");
                            }
                            else
                            {
                                return Content("5");
                            }


                        }
                        else
                        {
                            //Emailid or password does not match.  
                            //return Json("2");
                            return Content("Email id or password does not match.");
                        }
                    }
                    else
                    {
                        // return Json("3");
                        //Email is not verified yet please check your mail.
                        return Content("Email is not verified yet please check your mail.");
                    }
                }
            }
            else
            {
                //Email id does not exist please register first.
                return Content("Email id does not exist please register first.");
            }
            return View();
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            Session["userSession"] = null;
            return RedirectToAction("Login", "Account");
        }

        //-----------Forgot Password Action------------//

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword forgot)
        {
            string Result = "";
            string url = "";
            try
            {
                if (forgot.EmailID != "" && forgot.EmailID != null)
                {

                    List<cLoginUser> objLogin = cLoginUser.Find(" sEmailID = " + forgot.EmailID.ToString().Trim());
                    if (objLogin.Count > 0)
                    {
                        List<cUserProfile> aobProfile = cUserProfile.Find(" objLoginUser = " + objLogin[0].iID);
                        if (aobProfile.Count > 0)
                        {
                            Result = ForgotPasswordMail(aobProfile[0].sFirstName.ToString(), aobProfile[0].sLastName.ToString(), forgot.EmailID.ToString().Trim(), "GSDHGSD");
                        }

                        if (Result == "ok")
                        {

                            return Content("1");
                        }
                        else
                        {
                            return Content("0");
                        }
                    }
                }
                if (forgot.Mobile != "" && forgot.Mobile != null)
                {


                    List<cUserProfile> aobProfile = cUserProfile.Find(" sMobileNo = " + forgot.Mobile.ToString().Trim());
                    if (aobProfile.Count > 0)
                    {
                        Result = MobileOTP.GetOTP(forgot.Mobile.Trim());
                    }

                    if (Result != "")
                    {
                        Session["OTPMail"] = aobProfile[0].objLoginUser.iObjectID;
                        return Content("3");
                    }
                    else
                    {

                        return Content("4");
                    }

                }
                if (forgot.OTPText.Trim() != "")
                {


                    Result = MobileOTP.VerifyOTP(forgot.OTPText.Trim());
                    if (Result != "ok")
                    {

                        return Content("5");
                    }
                    else
                    {

                        return Content("6");
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }
        public string ForgotPasswordMail(string FirstName, string LastName, string Email, string HashCode)
        {
            string strBody = "";
            string getEmail = Cryptography.Encryptdata(Email);
            string link = "http://localhost:3095/Account/ChangePasswordVerification?Email=" + getEmail + "&HashCode=" + Cryptography.Encryptdata(HashCode) + "&SendDate=" + Cryptography.Encryptdata((System.DateTime.Now).ToString()) + "";
            string Links = "<b><a target='_blank' href=" + link + ">Click here to reset your password.</a></b>";
            StreamReader sr = new StreamReader(HttpContext.Server.MapPath(HttpContext.Request.ApplicationPath + "/App_Data/PasswordVerification.html"));
            strBody = sr.ReadToEnd();
            sr.Close();
            strBody = strBody.Replace("#Name#", FirstName + " " + LastName);
            strBody = strBody.Replace("#Link#", Links);
            string result = Mail.SendGmail(Email, "Change Password Link", strBody);
            return result;
        }
        public ActionResult ChangePasswordVerification(string Email, string HashCode, string SendDate)
        {

            string sEmail = Cryptography.Decryptdata(Email);
            string sHashCode = Cryptography.Decryptdata(HashCode);
            DateTime getDate = Convert.ToDateTime(Cryptography.Decryptdata(SendDate));
            DateTime curDate = DateTime.Now;
            int diff = curDate.Subtract(getDate).Seconds;
            if (diff < 1)
            {
                return RedirectToAction("Error", new { ErrorCode = "1" });
            }
            else
            {
                List<cLoginUser> objLogin = cLoginUser.Find(" sEmailID = " + sEmail);
                if (objLogin.Count > 0)
                {
                    Session["emailSession"] = sEmail;
                    return RedirectToAction("ResetPassword");

                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            //return RedirectToAction("ChangePassword");
        }
        public ActionResult ResetPassword()
        {

            return View();
        }

        //----------- Change Password Action------------//

        [HttpGet]
        [ChildActionOnly]
        public ActionResult ChangePassword()
        {
            return PartialView("_ChangePassword");
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePassword changepassword)
        {
            if (Session["UserID"] != null)
            {
                string UserID = Session["UserID"].ToString();
                cLoginUser objLogin = cLoginUser.Get_ID(Convert.ToInt32(UserID));
                objLogin.sPassword = Cryptography.Encryptdata(changepassword.NewPassword);
                objLogin.Save();
                return RedirectToAction("Profile","Home");

            }
            else
            {
                if (Session["emailSession"] != null)
                {
                    string email = Session["userSession"].ToString();
                    List<cLoginUser> objLogin = cLoginUser.Find(" sEmailID = " + email);
                    if (objLogin.Count > 0)
                    {
                        objLogin[0].sPassword = Cryptography.Encryptdata(changepassword.NewPassword);
                        objLogin[0].Save();
                        Session["emailSession"] = null;
                        return Content("7");
                    }
                }
                else
                {
                    if (Session["OTPMail"] != null)
                    {
                        cLoginUser objLogin = cLoginUser.Get_ID(Convert.ToInt32(Session["OTPMail"].ToString()));
                        objLogin.sPassword = Cryptography.Encryptdata(changepassword.NewPassword);
                        objLogin.Save();
                        Session["OTPMail"] = null;
                        return Content("1");

                    }
                }

            }
            return View();
        }

        //-----------Check MailId or Mobile No. Already Exist------------//

        [HttpGet]
        public JsonResult CheckEmailExist(string EmailID)
        {
            List<cLoginUser> objLogin = cLoginUser.Find(" sEmailID = " + EmailID.ToString().Trim());
            if (objLogin.Count > 0)
            {
                return Json("Email Id already exist.", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult CheckMobileExist(string Mobile)
        {
            List<cUserProfile> objUserprofile = cUserProfile.Find(" sMobileNo = " + Mobile.ToString().Trim());
            if (objUserprofile.Count > 0)
            {
                return Json("Mobile no. already exist.", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult CheckEmailNotExist(string EmailID)
        {
            List<cLoginUser> objLogin = cLoginUser.Find(" sEmailID = " + EmailID.ToString().Trim());
            if (objLogin.Count <= 0)
            {
                return Json("Email Id not exist.", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult CheckMobileNotExist(string Mobile)
        {
            List<cUserProfile> objUserprofile = cUserProfile.Find(" sMobileNo = " + Mobile.ToString().Trim());
            if (objUserprofile.Count <= 0)
            {
                return Json("Mobile no. not exist.", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult CheckUserName(string UserID)
        {
            List<cLoginUser> objUserprofile = cLoginUser.Find(" sUserName = " + UserID.ToString().Trim());
            if (objUserprofile.Count > 0)
            {
                return Json("User Name already exist.", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }



        //-----------Error message Action------------//

        public ActionResult Error()
        {
            return View();
        }

        //-----------Add Free Plan To User after verification------------//

        public void SaveFreePlan(int LoginId)
        {
            //try
            //{
            //    cPlan objPlan = cPlan.Get_Name("Starter");
            //    cLogin_Plan objloginPlan = cLogin_Plan.Create();
            //    objloginPlan.objLoginUser.iObjectID = LoginId;
            //    objloginPlan.objPlan.iObjectID = objPlan.iID;
            //    objloginPlan.dtPlanStartDate = DateTime.Now;
            //    objloginPlan.dtPlanEndDate = DateTime.Now.AddDays(Convert.ToInt32(objPlan.sPlanDays));
            //    objloginPlan.Save();
            //}
            //catch (Exception ex)
            //{

            //}

        }
    }
}
