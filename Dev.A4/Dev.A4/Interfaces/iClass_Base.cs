using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dev.A4.Enums;

namespace Dev.A4.Interfaces
{
    public interface iClass_Base
    {
        /// <summary>
        /// Returns true if Invalid
        /// </summary>
        bool bInvalid { get; }
        /// <summary>
        /// Returns true if this object is being created, ie. Create() called but Save() not yet called
        /// </summary>
        bool bCreating { get; }
        /// <summary>
        /// Throws cInvalidObjectException if this object is invalid
        /// </summary>
        void MakeSureObjectIsValid();
        /// <summary>
        /// Discards the object
        /// </summary>
        void Discard();
        /// <summary>
        /// This is invoked on calling Save() i.e. while creation of objects
        /// Overload this function to do custom validations
        /// </summary>
        void _onValidateBeforeSaving();
        /*
        bool _onSecurityCheck(int i_iAction);
        bool _onSecurityCheck(int i_iObjectID, int i_iAction);
        string _onGetSecurityScopeFilter(string i_sFilter, int i_iAction);
        */
        string _ValidateSQLFilterParameter(string i_sPropertyName, enComparison i_enOperator, string i_sPropertyValue);
        string _ValidateSQLUpdateParameter(string i_sPropertyName, string i_sPropertyValue);
        void _ValidateSQLOutputParameter(string i_sPropertyName);
    }
}