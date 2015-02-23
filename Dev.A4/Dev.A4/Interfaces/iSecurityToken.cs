using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Interfaces
{
    public interface iSecurityToken
    {
        /// <summary>
        /// User ID
        /// </summary>
        int iUserID { get; }
        /// <summary>
        /// Session ID
        /// </summary>
        int iSessionID { get; }
        /// <summary>
        /// Security Check
        /// </summary>
        /// <param name="i_sClassID"></param>
        /// <param name="i_sObjectID"></param>
        /// <param name="i_iAction"></param>
        void SecurityCheck(string i_sClassID, string i_sObjectID, int i_iAction);
        /// <summary>
        /// Security Check
        /// </summary>
        /// <param name="i_sClassID"></param>
        /// <param name="i_iAction"></param>
        void SecurityCheck(string i_sClassID, int i_iAction);
        /// <summary>
        /// Get Scope Filter
        /// </summary>
        /// <param name="i_sClassID"></param>
        /// <param name="i_sFilter"></param>
        /// <param name="i_iAction"></param>
        /// <returns></returns>
        string onGetSecurityScopeFilter(string i_sClassID, string i_sFilter, int i_iAction);
        /// <summary>
        /// Returns an encrypted string version (serialized) of this object
        /// </summary>
        /// <returns></returns>
        string ToEnCryptedString();
        /// <summary>
        /// Initializes this object from the given string
        /// </summary>
        /// <param name="i_sValue"></param>
        void FromString(string i_sValue);
        /// <summary>
        /// Returns an string version (serialized) of this object
        /// </summary>
        /// <returns></returns>
        string ToString();
        /// <summary>
        /// Initializes this object from the given encrypted string
        /// </summary>
        /// <param name="i_sValue"></param>
        void FromEncryptedString(string i_sValue);
    }
}