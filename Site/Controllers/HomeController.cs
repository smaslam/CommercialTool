using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommTool;
using Site.Models;
using Site.App_Code;
using System.IO;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Profile()
        {
            Profile objProfile = new Profile();
            if (Session["UserID"] != null)
            {
                cLoginUser objLogin = cLoginUser.Get_ID(Convert.ToInt32(Session["UserID"]));
                List<cUserProfile> aobjProfile = cUserProfile.Find(" objLoginUser = " + Convert.ToInt32(objLogin.iID));
                if (aobjProfile.Count > 0)
                {
                    objProfile.UserName = objLogin.sUserName;
                    objProfile.EmailID = objLogin.sEmailID;
                    objProfile.FirstName = aobjProfile[0].sFirstName;
                    objProfile.LastName = aobjProfile[0].sLastName;
                    objProfile.MobileNo = aobjProfile[0].sMobileNo;
                    objProfile.CompanyName = aobjProfile[0].sCompanyName;

                    //Plan Data Iterate:-
                    //List<cLogin_Plan> objPlanLogin = cLogin_Plan.Find(" objLoginUser = " + Convert.ToInt32(objLogin.iID));
                    //if (objPlanLogin.Count > 0)
                    //{
                    //    cPlan objPlan = cPlan.Get_ID(objPlanLogin[0].objPlan.iObjectID);
                    //    objProfile.PlanName = objPlan.sName;
                    //    objProfile.EndPlan = objPlan.sPlanDays;
                    //    objProfile.Submission = objPlan.sFormSubmission;
                    //    objProfile.Memory = objPlan.sMemorySpace;
                    //    objProfile.MaxForm = objPlan.sMaxCreateForms;
                    //}
                    //HomeController c = new HomeController();
                    //ActionResult result = c.SubUserLoad();
                   
                }

            }
            else
            {

                return RedirectToAction("Login", "Account");
            }

            return View(objProfile);
        }

        //Load Magen user:-
        [HttpGet]
        public ActionResult SubUser(){
            List<cAccess> aobjAccess = cAccess.Find();
            List<CheckBoxes> chkbx = new List<CheckBoxes>();
            if (aobjAccess.Count > 0)
            {
                foreach (var item in aobjAccess)
                {
                    chkbx.Add(new CheckBoxes() { Text = item.sAccessName, Checked = true, Value = item.iID.ToString() });
                }


            }

            SubUser objSubUser = new SubUser();
            objSubUser.AccessList = chkbx;
            return PartialView("_SubUserPartial", objSubUser);

        }


        [HttpPost]
        public ActionResult Profile(Profile objProf)
        {
            System.Threading.Thread.Sleep(1000);
            if (Session["UserID"] != null)
            {
                cLoginUser objLogin = cLoginUser.Get_ID(Convert.ToInt32(Session["UserID"]));
                List<cUserProfile> aobjProfile = cUserProfile.Find(" objLoginUser = " + Convert.ToInt32(objLogin.iID));
                if (aobjProfile.Count > 0)
                {
                    if (objProf.FirstName != null)
                    {
                        aobjProfile[0].sFirstName = objProf.FirstName;
                    }
                    if (objProf.LastName != null)
                    {
                        aobjProfile[0].sLastName = objProf.LastName;
                    }
                    if (objProf.MobileNo != null)
                    {
                        aobjProfile[0].sMobileNo = objProf.MobileNo;
                    }
                    if (objProf.CompanyName != null)
                    {
                        aobjProfile[0].sCompanyName = objProf.CompanyName;
                    }
                    aobjProfile[0].Save();

                }
                return Content("1");
               // return View();
                
           }
            else {
                return View();
            }
          

        }

        //call sequerity view 

        [HttpPost]
        public ActionResult SubUser(SubUser profile)
        {
            try
            {
                Boolean edit = true;
                Boolean view = true;
                Boolean delete = true;
                string ParantId = Session["UserID"].ToString();
                string SubUserMail = profile.EmailID;
                string ParantEmail = "";
                string pageName = "";
                int subUserCurID = 0;

                // find Email id of parant
                List<cLoginUser> objLogin = cLoginUser.Find(" iID = " + ParantId);
                if (objLogin.Count > 0)
                {
                    ParantEmail = objLogin[0].sEmailID;
                }

                //check if sub user id as sender id same or not

                if (ParantEmail.CompareTo(SubUserMail) == 0)
                {
                    //you can't add your self as subsuer
                }
                else
                {

                    // find sub user already exist for parant user
                    List<cSubUser> objSubUser = cSubUser.Find(" sEmailID = " + SubUserMail + " and objLoginUser = " + ParantId);
                    if (objSubUser.Count > 0)
                    {
                        // sub user already exist for that uaser
                    }
                    else
                    {
                        //Insert data into SubUserTable

                        cSubUser objSub = cSubUser.Create();
                        objSub.sEmailID = SubUserMail;
                        objSub.objLoginUser.iObjectID = Convert.ToInt32(ParantId);
                        objSub.bIsActive = false;
                        objSub.bIsVerified = false;
                        objSub.Save();

                        //find Current subuser id

                        subUserCurID = objSub.iID;


                        //Insert data into accessform

                        cUserAccessForm objUserAccess = cUserAccessForm.Create();
                        objUserAccess.objForms.iObjectID = 1;
                        objUserAccess.objSubuser.iObjectID = subUserCurID;
                        objUserAccess.bIsDeleted = delete;
                        objUserAccess.bIsView = view;
                        objUserAccess.bIsEdit = edit;
                        objUserAccess.Save();

                        sendSubUserVerificationMail("test", "test", SubUserMail, ParantEmail,ParantId, "HGASDHHJASGAGHSJASGYASGHAGSY");
                    }
                }


            }
            catch (Exception ex)
            {

                throw;
            }
            return View();
        }

        public void sendSubUserVerificationMail(string FirstName, string LastName, string SubUserEmail, string ParantUserEmail,string ParantID,string HashCode)
        {
            string strBody = "";
            string subMailId = Cryptography.Encryptdata(SubUserEmail);
            string MailUserId = Cryptography.Encryptdata(ParantUserEmail);
            string HasCodes = Cryptography.Encryptdata(HashCode);
            string parantID = Cryptography.Encryptdata(ParantID);
            string link = "http://localhost:3095/Home/SubUserVerification?SubUserEmail=" + subMailId + "&MailUserMail=" + MailUserId + "&ParantID=" + parantID + "&HashCode=" + HasCodes + "";
            string Links = "<b><a target='_blank' href=" + link + ">Click here to verify your account.</a></b>";
            StreamReader sr = new StreamReader(HttpContext.Server.MapPath(HttpContext.Request.ApplicationPath + "/App_Data/SubUserVerification.html"));
            strBody = sr.ReadToEnd();
            sr.Close();
            strBody = strBody.Replace("#Name#", FirstName + " " + LastName);
            strBody = strBody.Replace("#Link#", Links);
            Mail.SendGmail(SubUserEmail, "Verification Link", strBody);
        }

        public ActionResult SubUserVerification(string SubUserEmail, string MailUserMail,string parantID, string HashCode)
        {
            try
            {
                string subUserEmail = Cryptography.Decryptdata(SubUserEmail);
                string parantEmail = Cryptography.Decryptdata(MailUserMail);
                string hasCodes = Cryptography.Decryptdata(HashCode);
                int id =Convert.ToInt32( Cryptography.Decryptdata(parantID));
                //update cUser bIsACtive and bIsVerified

                List<cSubUser> objSubUser = cSubUser.Find(" sEmailID = "+subUserEmail +" and objLoginUser = "+id);
                if (objSubUser.Count > 0)
                {
                    objSubUser[0].bIsActive = true;
                    objSubUser[0].bIsVerified = true;
                }
                objSubUser[0].Save();

                //after visit link land according to situation
              
                List<cLoginUser> objLoginUser = cLoginUser.Find(" sEmailID = " + subUserEmail);
                if (objLoginUser.Count > 0)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    return RedirectToAction("Register", "Account", new { Email = subUserEmail });
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
           // return RedirectToAction("");
        }

    }
}
