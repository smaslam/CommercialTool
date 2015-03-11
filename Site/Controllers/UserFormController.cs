using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Site.App_Code;
using CommTool;
using System.Web.Services;

namespace Site.Controllers
{
    public class UserFormController : Controller
    {
        //
        // GET: /UserForm/

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public void SaveFormContent(string HTMLCode,string formName)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    cForms objForm = cForms.Create();
                    objForm.sFormName = formName;
                    objForm.objLoginUser.iObjectID = Convert.ToInt32(Session["UserID"].ToString());
                    objForm.sHTMLContent = HTMLCode;
                    objForm.sEncrytionKey = "";
                    objForm.Save();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        [HttpPost]
        public void   PageView(string formName)
        {
            string result = "";
            try
            {
                
                if (Session["UserID"] != null)
                {
                    List<cForms> objForm = cForms.Find(" objLoginUser = " + Session["UserID"].ToString() + " and sFormName = " + formName + " and bIsActive = true" );
                   if (objForm.Count == 1)
                   {
                       TempData["formContent"] = objForm[0].sHTMLContent;
                        result = objForm[0].sHTMLContent;
                        

                   }
                   
                }
              //  return View("PagePreview");
                
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
            
        }
        public ActionResult PagePreview()
        {
            return View();
        }

    }
}
