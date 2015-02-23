using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.DataTypes
{
    public class cObjectReference
    {
        protected string m_sClassID = string.Empty;

        public string sClassID
        {
            get
            {
                return m_sClassID;
            } 
        
        }

        protected int m_iObjectID = 0;

        public int iObjectID {

            get 
            {

                return m_iObjectID;
            }
            set 
            {
                //if object exists
                m_iObjectID = value;
            }
        }

        public cObjectReference(string i_sValue)
        {
            string[] a = i_sValue.Split(':');
            //  Check if class exists
            m_sClassID = a[0];
            if (a.Length > 1)
            {
                if (a[1].ToUpper() != "NONE")
                {
                    iObjectID = Convert.ToInt32(a[1]);
                }
            }
        }


    }
}