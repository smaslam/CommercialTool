using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class UserRecovery
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }

        public int Question1 { get; set; }
        public string Answer1 { get; set; }

        public int Question2 { get; set; }
        public string Answer2 { get; set; }

        public int Question3 { get; set; }
        public string Answer3 { get; set; }
    }
}