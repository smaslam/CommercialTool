using System;using System.Collections.Generic;using System.Text;using Dev.A4;using Dev.A4.Enums;using Dev.A4.Exceptions;using Dev.A4.General;using Dev.A4.Interfaces;using Dev.A4.Internal;using Dev.A4.Storages;using Dev.A4.DataTypes;namespace CommTool{        /// <summary>    /// Verfication    ///     /// 2/17/2015 10:11:40 AM    /// </summary>    public class cVerfication : cVerfication_Base    {        //public override void _onValidateBeforeSaving()        //{        //    // This is invoked on calling Save() i.e. while creation and updation        //    base._onValidateBeforeSaving();        //    // If validations are not successful throw new cValidationException(sReason);        //    if (m_bCreating)        //    {        //        // Validations while creating a new object        //        // TODO:         //    }        //    else        //    {        //        // Validations while updating a new object        //        // TODO:         //    }        //}        //protected override bool onSecurityCheck(int i_iAction)        //{        //    base.onSecurityCheck(i_iAction);        //    // Perform any additional security checks that may be required before allowing the action to proceed        //    // If security checks are not successful then throw new cInsufficientRightsException(CLASS_ID + ":" + i_iAction + "<-" + i_oToken.ToString());        //    switch ((enVerfication_Action)i_iAction)        //    {        //        case enVerfication_Action.Create:        //            // TODO:         //            break;        //        case enVerfication_Action.Delete:        //            // TODO:         //            break;        //        case enVerfication_Action.Find:        //            // TODO:         //            break;        //        case enVerfication_Action.Update:        //            // TODO:         //            break;        //        case enVerfication_Action.Get:        //            // TODO:         //            break;        //        default:        //            throw new cUnsupportedActionInvokedException("Unknown action: " + m_oClass.sFullName + ":" + i_iAction.ToString());        //    }        //    return true;        //}        //protected override bool onSecurityCheck(string i_sObjectID, int i_iAction)        //{        //    base.onSecurityCheck(i_sObjectID, i_iAction);        //    // Perform any additional security checks that may be required before allowing the action to proceed on the specified object        //    // If security checks are not successful then throw new cInsufficientRightsException(CLASS_ID + ":" + i_iAction + "<-" + i_oToken.ToString());        //    switch ((enVerfication_Action)i_iAction)        //    {        //        case enVerfication_Action.Create:        //            // TODO:         //            break;        //        case enVerfication_Action.Delete:        //            // TODO:         //            break;        //        case enVerfication_Action.Find:        //            // TODO:         //            break;        //        case enVerfication_Action.Update:        //            // TODO:         //            break;        //        case enVerfication_Action.Get:        //            // TODO:         //            break;        //        default:        //            throw new cUnsupportedActionInvokedException("Unknown action: " + m_oClass.sFullName + ":" + i_iAction.ToString());        //    }        //    return true;        //}        //protected override string onGetSecurityScopeFilter(string i_sFilter, int i_iAction)        //{        //    //base.onGetSecurityScopeFilter(i_sFilter, i_iAction);        //    // Return any additional scope filter (which will be used as a part of WHERE clause)        //    // The returned filter condition will be added to any existing filter using AND        //    // TODO:        //    return string.Empty;        //}            }}