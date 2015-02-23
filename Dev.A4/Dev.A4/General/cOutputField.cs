using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Dev.A4.Interfaces;

namespace Dev.A4.General
{
    public class cOutputField
    {
        public string sName = string.Empty;
        public string sAlias = null;

        public cOutputField(string i_sName)
        {
            sName = i_sName;
        }

        public cOutputField(string i_sName, string i_sAlias)
        {
            sName = i_sName;
            sAlias = i_sAlias;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(sAlias))
            {
                return sAlias;
            }
            else
            {
                return sName;
            }
        }

        public string ToString(iClass_Base i_oObject)
        {
            i_oObject._ValidateSQLOutputParameter(sName);
            StringBuilder sb = new StringBuilder();
            sb.Append(sName);
            //string sTemp;
            //if (!string.IsNullOrEmpty(sAlias))
            //{
            //    sTemp = cUtility.GetValidIdentifier(sAlias);
            //    if (sTemp != sAlias) throw new cInvalidOutputParameterException("Invalid Alias " + sAlias);
            //    sb.Append(" AS " + sAlias);
            //}
            return sb.ToString();
        }
    }
}