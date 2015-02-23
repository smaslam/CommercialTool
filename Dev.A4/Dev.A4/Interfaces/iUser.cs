using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dev.A4.Enums;

namespace Dev.A4.Interfaces
{
    public interface iUser : iClass_Type1
    {
        /// <summary>
        /// Login ID
        /// </summary>
        string sLoginID { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        string sPassword { set; }
        /// <summary>
        /// User Type
        /// </summary>
        enUserType enType { get; set; }
        /// <summary>
        /// True if this user is external (say from LDAP)
        /// </summary>
        bool bExternal { get; set; }
        /// <summary>
        /// External User ID (say LDAP path)
        /// </summary>
        string sExternalUserID { get; set; }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="i_sLoginID">User ID</param>
        /// <param name="i_sPassword">Password</param>
        /// <param name="i_sOptionalParameters">Other parameters which can be considered for Login (like source IP)</param>
        /// <param name="o_iSessionID">Session ID</param>
        /// <returns>User Object</returns>
        iUser Login(string i_sLoginID, string i_sPassword, string i_sOptionalParameters, out int o_iSessionID);

    }
}