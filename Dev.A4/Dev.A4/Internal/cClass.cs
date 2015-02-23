using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dev.A4.Enums;

namespace Dev.A4.Internal
{
    public class cClass
    {
        private static Dictionary<string, cClass> m_dicClasses = new Dictionary<string, cClass>();

        public static void RegisterClass(cClass i_oClass)
        {
            if (!m_dicClasses.ContainsKey(i_oClass.sFullName.ToLower()))
            {
                m_dicClasses.Add(i_oClass.sFullName.ToLower(), i_oClass);
            }
        }

        public static cClass Get(string i_sFullName)
        {
            return m_dicClasses[i_sFullName.ToLower()];
        }

        public static List<cClass> Find()
        {
            List<cClass> a = new List<cClass>();
            foreach (string s in m_dicClasses.Keys)
            {
                a.Add(m_dicClasses[s]);
            }
            return a;
        }
        //--------------------------------------------------------------------

        readonly public string sName;
        readonly public string sNamespace;
        readonly public string sDescriptiveName;
        readonly public string sDescription;
        readonly public enClassType enType;
        readonly public bool bIsSecurityEnabled;
        readonly public enStorageType enStorage;

        readonly public cProperty[] aProperties;
        private Dictionary<string, int> m_dicProperties;

        public string sFullName
        {
            get { return sNamespace + "." + sName; }
        }

        public cClass(string i_sName, string i_sNamespace, string i_sDescriptiveName, string i_sDescprition, enClassType i_enType, bool i_bIsSecurityEnabled, cProperty[] i_aProperties, enStorageType i_enStorage)
        {
            sName = i_sName;
            sNamespace = i_sNamespace;
            sDescriptiveName = i_sDescriptiveName;
            sDescription = i_sDescprition;
            enType = i_enType;
            bIsSecurityEnabled = i_bIsSecurityEnabled;
            aProperties = i_aProperties;
            m_dicProperties = new Dictionary<string, int>(aProperties.Length);
            for (int i = 0; i < aProperties.Length; i++)
            {
                m_dicProperties.Add(aProperties[i].sName, i);
            }
            enStorage = i_enStorage;
        }

        public cProperty GetProperty(string i_sName)
        {
            return aProperties[m_dicProperties[i_sName]];
        }
    }
}