using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Dev.A4.Exceptions;
using Dev.A4.Enums;
using Dev.A4.Interfaces;

namespace Dev.A4.General
{
    public class cFilterOutput
    {
        public List<cOutputField> aOutputs = new List<cOutputField>();

        public cFilterOutput()
        {
        }

        /// <summary>
        /// Format should be:
        /// {property} as {alias}, ...
        /// NOTE: "as" has to be in lowercase and with spaces around it
        /// </summary>
        /// <param name="i_sOutputFields">Output fields</param>
        public cFilterOutput(string i_sOutputFields)
        {
            string[] a = i_sOutputFields.Split(',');
            string[] b;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i].Contains(" as "))
                {
                    b = a[i].Split(new string[1] { " as " }, StringSplitOptions.None);
                    if (b.Length == 2)
                    {
                        aOutputs.Add(new cOutputField(b[0].Trim(), b[1].Trim()));
                    }
                    else throw new cInvalidOutputParameterException(a[i]);
                }
                else
                {
                    if (!string.IsNullOrEmpty(a[i].Trim()))
                    {
                        aOutputs.Add(new cOutputField(a[i].Trim()));
                    }
                }
            }
        }

        public cFilterOutput(cOutputField i_oParam)
        {
            aOutputs.Add(i_oParam);
        }

        public void Add(cOutputField i_oParam)
        {
            aOutputs.Add(i_oParam);
        }

        public string ToString(iClass_Base i_oObject)
        {
            if (aOutputs.Count < 1) return "*";
            StringBuilder sb = new StringBuilder();
            sb.Append(' ');
            for (int i = 0; i < aOutputs.Count; i++)
            {
                if (i > 0) sb.Append(", \n");
                sb.Append(aOutputs[i].ToString(i_oObject));
            }
            sb.Append(' ');
            return sb.ToString();
        }
    }
}