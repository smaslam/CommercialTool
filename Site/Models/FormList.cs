using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class FormList
    {
        public int iID { get; set; }
        public string FormName { get; set; }
        public bool Edit { get; set; }
        public bool Views { get; set; }
    }
}