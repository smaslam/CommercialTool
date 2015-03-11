using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Site.Models;
using CommTool;
using System.Data;
using Newtonsoft.Json;
using System.Web.Helpers;

namespace Site.Controllers
{
    public class FormsController : Controller
    {
        //
        // GET: /Forms/

        public ActionResult Index()
        {
            List<cForms> aobForms = cForms.Find();
            List<FormList> objFormList = new List<FormList>();
            if (aobForms.Count > 0)
            {
                foreach (var item in aobForms)
                {
                    objFormList.Add(new FormList { iID = item.iID, FormName = item.sFormName,Edit=false,Views=false });
                }
               
            }
            return View(objFormList.ToList());
        }
        [HttpPost]
        public ActionResult Index(FormCollection frmColl)
        {
            var edit = frmColl.GetValues("Edit");
            if (edit.Length > 0)
            {
                foreach (var id in edit)
                {
                    int ids = Convert.ToInt32(id.ToString());
                    List<cUserAccessForm> aobjForms = cUserAccessForm.Find(" objForms = " + ids);
                    if (aobjForms.Count > 0)
                    {
                        cForms objForm = cForms.Get_ID(ids);
                    }
                    else
                    {
                        cUserAccessForm objAccess = cUserAccessForm.Create();
                        
                    }
                    
                   


                }
            }
            var view = frmColl.GetValues("View");
            if (view.Length > 0)
            {
                foreach (var id in view)
                {
                    string ids = id.ToString();
                }
            }


            List<cForms> aobForms = cForms.Find();
            List<FormList> objFormList = new List<FormList>();
            if (aobForms.Count > 0)
            {
                foreach (var item in aobForms)
                {
                    objFormList.Add(new FormList { iID = item.iID, FormName = item.sFormName });
                }

            }
            return View(objFormList.ToList());
        }
    }
}
