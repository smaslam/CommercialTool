using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommTool;
using Site.Models;
using Site.App_Code;

namespace Site.Controllers
{

    public class PricingController : Controller
    {
        //
        // GET: /Pricing/

        public ActionResult Index()
        {
            List<Plan> aobjPlanData = new List<Plan>();
            try
            {
                List<cPlan> aobjPlan = cPlan.Find();
                if (aobjPlan.Count > 0)
                {
                    foreach (var item in aobjPlan)
                    {
                        aobjPlanData.Add(new Plan() { iID = item.iID, PlanName = item.sPlanName, iNoSubUser = item.iNoSubUser, iNoResponses = item.iNoResponses, iNoForms = item.iNoForms, PlanCost = item.fCost.ToString(), iDays = item.iDays, iMemeorySpace = item.iMemeorySpace,iNoCampaign=item.iNoCampaign,iNoInputControle=item.iNoInputControle,iNoEmailSent=item.iNoEmailSent,bIsBranding=item.bIsBranding,bIsThankuPage=item.bIsThankuPage,bEmailSupport=item.bEmailSupport });
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return View(aobjPlanData.ToList());
        }


        public ActionResult UpgradeAccount(string id)
        {
            cOrder objOrder = cOrder.Create();
            if (Session["UserID"] != null)
            {
                objOrder.fOrderAmount = float.Parse("228.0");
                objOrder.objOrderStatus.iObjectID = Convert.ToInt32(General.enOrderStatus.NewOrder);
                objOrder.objLoginUser.iObjectID = Convert.ToInt32(Session["UserID"].ToString());
                objOrder.objPlan.iObjectID = Convert.ToInt32(id);
                objOrder.sPaymentMode = "N/A";
                objOrder.Save();
                string OrderNo = cOrder.Get_ID(objOrder.iID).sOrderNo;
                TempData["PlanID"]=id;
                Session["OrderNo"] = OrderNo;
                return RedirectToAction("PaymentGateway", "Pricing");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }




        }
        public ActionResult PaymentGateway()
        {
            return View();
        }
        public ActionResult Success()
        {

            try
            {
                if (Session["OrderNo"] != null)
                {
                    List<cOrder> a_ocOrder = cOrder.Find(" sOrderNo = " + Session["OrderNo"].ToString());
                    a_ocOrder[0].objOrderStatus.iObjectID = Convert.ToInt32(General.enOrderStatus.Success);
                    a_ocOrder[0].Save();

                    int PlanID = Convert.ToInt32(TempData["PlanID"].ToString());

                    //Code The Configuration of package To user ID
                    cPlan objPlan = cPlan.Get_ID(PlanID);

                    cPlanPurchase objPurchase = cPlanPurchase.Create();
                    objPurchase.sPlanName = objPlan.sPlanName;
                    objPurchase.fPlanCost = objPlan.fCost;
                    objPurchase.iDays = objPlan.iDays;
                    objPurchase.iNoForms = objPlan.iNoForms;
                    objPurchase.iSubUser = objPlan.iNoSubUser;
                    objPurchase.iMemorySpace = objPlan.iMemeorySpace;
                    objPurchase.iNoCampaign = objPlan.iNoCampaign;
                    objPurchase.iNoResponses = objPlan.iNoResponses;
                    objPurchase.iNoControle = objPlan.iNoInputControle;
                    objPurchase.bIsBranding = objPlan.bIsBranding;
                    objPurchase.iNoEmailSent = objPlan.iNoEmailSent;
                    objPurchase.bIsThankuPage = objPlan.bIsThankuPage;
                    objPurchase.bIsEmailSupport = objPlan.bEmailSupport;
                    objPurchase.iDays = objPlan.iDays;
                    objPurchase.dtStartDate = System.DateTime.Today;
                    objPurchase.dtEndDate = System.DateTime.Today.AddDays(objPlan.iDays);
                    objPurchase.bIsActive = true;
                    objPurchase.Save();

                    // Mail Will Be Send To  Specific User

                    ViewBag.OrderNo = Session["OrderNo"].ToString();
                    Session["OrderNo"] = null;
                    return View();
                }
                else
                {
                    return RedirectToAction("Error", "Account");
                }
            }
            catch (Exception)
            {
                
                throw;
            }

        }
        public ActionResult Failure()
        {
            if (Session["OrderNo"] != null)
            {
                List<cOrder> a_ocOrder = cOrder.Find(" sOrderNo = " + Session["OrderNo"].ToString());
                a_ocOrder[0].objOrderStatus.iObjectID = Convert.ToInt32(General.enOrderStatus.Failure);
                a_ocOrder[0].Save();

             
                // Mail Will Be Send To  Specific User For Failue
                ViewBag.OrderNo = Session["OrderNo"].ToString();
                Session["OrderNo"] = null;
                return View();
            }
            else
            {
                return RedirectToAction("Error","Account");
            }
        }
    }
}
