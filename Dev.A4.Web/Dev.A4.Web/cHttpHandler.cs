using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.Runtime.Serialization;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Dev.A4.Web
{
    class cHttpHandler
    {
        public void Init(HttpApplication i_oApp)
        {
            i_oApp.BeginRequest += new EventHandler(OnBeginRequest);
        }
        public void OnBeginRequest(Object i_oApp, EventArgs i_oArgs)
        {
            HttpApplication oHttpApp = i_oApp as HttpApplication;
            try
            {
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["Offline"]))
                {
                    // Site is offline
                    string sRequestPath = oHttpApp.Context.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + oHttpApp.Context.Request.PathInfo;
                    string sRequestPathLower = sRequestPath.ToLower();
                    if (sRequestPathLower.Contains(".aspx"))
                    {
                        oHttpApp.Context.RewritePath(ConfigurationManager.AppSettings["OfflinePage"]);
                        return;
                    }
                }
                else
                {
                    // Online
                    string sRequestPath = oHttpApp.Context.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + oHttpApp.Context.Request.PathInfo;
                    string sRequestPathLower = sRequestPath.ToLower();
                    if (Convert.ToBoolean(ConfigurationManager.AppSettings["EnableHTTPSRedirect"]))
                    {
                        string[] a_sPages = ConfigurationManager.AppSettings["HTTPSPages"].ToLower().Split(',');
                        for (int i = 0; i < a_sPages.Length; i++)
                        {
                            if (sRequestPathLower.Contains(a_sPages[i]))
                            {
                                oHttpApp.Context.RewritePath("HttpsRequired.aspx?sURL=" + oHttpApp.Context.Request.Url.AbsoluteUri.Substring(5));
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oHttpApp.Context.Response.Write("Error Http Module:" + ex.Message);
            }
        }

        public void Dispose() { }
    }
}
