﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 

namespace Site.Models
{
    public class Profile
    {
        public int iID { get;set; }
        public string UserName { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }
        public string CompanyName { get; set; }
        public string PlanName { get; set; }
        public string Submission { get; set; }
        public string Memory { get; set; }
        public string EndPlan { get; set; }
        public string MaxForm { get; set; }
       
    }

    public class CheckBoxes
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Checked { get; set; }
    }
    public class SubUser
    {
        public string EmailID { get; set; }
        public List<CheckBoxes> AccessList { get; set; }
        public int FormID { get; set; }
        public string FormName { get; set; }
    }

}