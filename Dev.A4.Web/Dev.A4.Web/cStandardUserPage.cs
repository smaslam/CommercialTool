using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.A4.Web
{
    class cStandardUserPage:cPage
    {
        protected override void OnPreLoad(EventArgs e)
        {
   
        }

        private void ShowError()
        {
            //cMain.oObject.ShowError("This page can only be viewed by a logged-in user");
        }

        protected override void OnLoad(EventArgs e)
        {
         }
      }
    }

