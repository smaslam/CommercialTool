using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dev.A4.General;
using Dev.A4.Enums;

namespace Dev.A4.Internal
{
    public class cProperty
    {
        readonly public string sName;
        private string m_sDescriptiveName;
        public string sDescriptiveName
        {
            get
            {
                if (string.IsNullOrEmpty(m_sDescriptiveName))
                {
                    int idx = 0;
                    for (int i = 0; i < sName.Length; i++)
                    {
                        if (cUtility.UPPERCASE.Contains(sName[i].ToString()))
                        {
                            idx = i;
                            break;
                        }
                    }
                    return sName.Substring(idx);
                }
                else return m_sDescriptiveName;
            }
        }
        readonly public string sDescription;
        readonly public enDataType enType;
        readonly public int iSize;
        readonly public string sDefaultValue;
        readonly public bool bIsUnique;
        readonly public bool bIsSearchable;

        public cProperty(string i_sName, string i_sDescriptiveName, string i_sDescprition, enDataType i_enType, int i_iSize, bool i_bIsUnique, bool i_bIsSearchable)
        {
            sName = i_sName;
            m_sDescriptiveName = i_sDescriptiveName;
            sDescription = i_sDescprition;
            enType = i_enType;
            iSize = i_iSize;
            bIsUnique = i_bIsUnique;
            bIsSearchable = i_bIsSearchable;
        }
    }
}