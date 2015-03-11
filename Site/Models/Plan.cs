using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class Plan
    {
        public int iID    { get; set; }
        public string PlanName { get; set; }
        public string PlanCost { get; set; }
        public int iNoForms { get; set; }
        public int iNoCampaign { get; set; }
        public int iMemeorySpace { get; set; }
        public int iNoSubUser { get; set; }
        public int iNoInputControle { get; set; }
        public int iNoEmailSent { get; set; }
        public int iNoResponses { get; set; }
        public int iDays { get; set; }
        public bool bIsBranding { get; set; }
        public bool bIsThankuPage { get; set; }
        public bool bEmailSupport { get; set; }

    }
}