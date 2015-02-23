using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Models
{
    
    public class SecurityQuestion
    {
        public int QuestionValue1 { get; set; }
        public string QuestionText1 { get; set; }
        public string Answer1 { get; set; }
        public SelectList ddl1 { get; set; }

        public int QuestionValue2 { get; set; }
        public string QuestionText2 { get; set; }
        public string Answer2 { get; set; }
        public SelectList ddl2 { get; set; }

        public int QuestionValue3 { get; set; }
        public string QuestionText3 { get; set; }
        public string Answer3 { get; set; }
        public SelectList ddl3 { get; set; }

    }
}