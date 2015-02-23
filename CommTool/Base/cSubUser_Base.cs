using System;using System.Collections.Generic;using System.Text;using System.Data;using System.Data.SqlClient;using Dev.A4;using Dev.A4.Enums;using Dev.A4.Exceptions;using Dev.A4.General;using Dev.A4.Interfaces;using Dev.A4.Internal;using Dev.A4.Storages;using Dev.A4.DataTypes;namespace CommTool{        /// <summary>    /// Sub User - Abstract Base Class    ///     /// 2/17/2015 10:11:40 AM    /// </summary>    public abstract class cSubUser_Base : iClass_Type1    {        /// <summary>        /// Class definition, for internal use only        /// </summary>        protected static cClass m_oClass;        /// <summary>        /// Class Definition        /// </summary>        public static cClass oClass        {            get { return m_oClass; }        }        /// <summary>        /// Never use this directly, even in this class, always use oStorage        /// </summary>        private static cStorage _oStorage = null;                /// <summary>        /// cMSSQL database used for storage        /// </summary>        public static cMSSQL oDB        {            get { return (oStorage as cMSSQL); }        }                public static cStorage oStorage        {            get            {                                if (_oStorage == null) oStorage = cMSSQL.GetDefault();                                return _oStorage;            }            set            {                _oStorage = value;                            }        }        static cSubUser_Base()        {            // Register the class                        cProperty[] aProperties = new cProperty[4];                        aProperties[0] = new cProperty("sEmailID", "Email ID", "", enDataType.String, 100, false, false);            aProperties[1] = new cProperty("objLoginUser", "Login User", "", enDataType.ObjectReference, 0, false, false);            aProperties[2] = new cProperty("bIsActive", "Is Active", "", enDataType.Boolean, 0, false, false);            aProperties[3] = new cProperty("bIsVerified", "Is Verified", "", enDataType.Boolean, 0, false, false);                        m_oClass = new cClass("cSubUser", "CommTool", "Sub User", "", enClassType.Standard, true, aProperties, enStorageType.MSSQL);                        cClass.RegisterClass(m_oClass);        }        /// <summary>        /// This is for internal use        /// </summary>        /// <returns></returns>        public static bool _IsSecurityEnabled()        {                        return true;        }        protected static void SecurityCheck(int i_iAction)        {            /*            cSubUser oObj = new cSubUser();            if (!oObj.onSecurityCheck(i_iAction))            {                //i_oSecurity.SecurityCheck(m_oClass.sFullName, i_iAction);            }            */         }        protected static void SecurityCheck(int i_iObjectID, int i_iAction)        {            /*            cSubUser oObj = new cSubUser();            if (!oObj.onSecurityCheck(i_iObjectID.ToString(), i_iAction))            {                //i_oSecurity.SecurityCheck(m_oClass.sFullName, i_iObjectID.ToString(), i_iAction);            }            */         }        protected static string GetSecurityScopeFilter(string i_sFilter, int i_iAction)        {                        /*            string sFilter = string.Empty;            cSubUser oObj = new cSubUser();            sFilter = oObj.onGetSecurityScopeFilter(i_sFilter, i_iAction);            if (string.IsNullOrEmpty(sFilter))            {                sFilter = i_oSecurity.onGetSecurityScopeFilter(m_oClass.sFullName, i_sFilter, i_iAction);            }            if (sFilter != string.Empty)            {                if (string.IsNullOrEmpty(i_sFilter))                {                    return "WHERE " + sFilter;                }                else                {                    return i_sFilter.Replace("WHERE ", "WHERE (") + ") AND (" + sFilter + ")";                }            }            else            */                                return i_sFilter;        }                protected int m_iID = -1;        /// <summary>        /// ID or Primary Key        /// </summary>        public virtual int iID        {            get { return m_iID; }        }        protected string m_sName = string.Empty;        /// <summary>        /// Name        /// </summary>        public virtual string sName        {            get { return m_sName; }            set { m_sName = cUtility.LimitLength(value, 100); }        }          protected DateTime m_dtCreatedOn = DateTime.Now;        /// <summary>        /// Created On        /// </summary>        public virtual DateTime dtCreatedOn        {            get { return m_dtCreatedOn; }        }        protected DateTime m_dtLastUpdatedOn = DateTime.Now;        /// <summary>        /// Last Updated On        /// </summary>        public virtual DateTime dtLastUpdatedOn        {            get { return m_dtLastUpdatedOn; }        }              protected bool m_bInvalid = true;        public bool bInvalid        {            get { return m_bInvalid; }        }        protected bool m_bCreating = false;        public bool bCreating        {            get { return m_bCreating; }        }        /// <summary>        /// Throws cInvalidObjectException if this object is invalid        /// </summary>        public void MakeSureObjectIsValid()        {            if (m_bInvalid) throw new cInvalidObjectException(string.Empty);        }        public virtual void Discard()        {            m_bCreating = false;            m_bInvalid = true;        }        protected virtual bool onSecurityCheck(int i_iAction)        {            return false;        }        protected virtual bool onSecurityCheck(string i_sObjectID, int i_iAction)        {            return false;        }        protected virtual string onGetSecurityScopeFilter(string i_sFilter, int i_iAction)        {            return string.Empty;        }        /// <summary>        /// Email ID        ///         /// </summary>        protected string m_sEmailID = cUtility.LimitLength("", 100);        public virtual string sEmailID        {            get { return m_sEmailID; }            set { m_sEmailID = cUtility.LimitLength(value, 100); }        }                /// <summary>        /// Login User        ///         /// </summary>        protected cObjectReference m_objLoginUser = new cObjectReference("CommTool.cLoginUser:NONE");        public virtual cObjectReference objLoginUser        {            get { return m_objLoginUser; }            set { m_objLoginUser = value; }        }                /// <summary>        /// Is Active        ///         /// </summary>        protected bool m_bIsActive = false;        public virtual bool bIsActive        {            get { return m_bIsActive; }            set { m_bIsActive = value; }        }                /// <summary>        /// Is Verified        ///         /// </summary>        protected bool m_bIsVerified = false;        public virtual bool bIsVerified        {            get { return m_bIsVerified; }            set { m_bIsVerified = value; }        }                protected cSubUser_Base()        {           // cSystem.MakeSureApplicationIsActive();        }        /// <summary>        /// Ensures that an object with the specified name exists, while creating other properties are set to their default values        /// </summary>        /// <param name="i_sName">Name</param>        /// <returns>cSubUser object</returns>        public static cSubUser CreateIfRequiredAndGet(string i_sName)        {            cSubUser oObj = cSubUser.Get_Name(i_sName);            if (oObj == null)            {                oObj = cSubUser.Create();                oObj.sName = i_sName;                oObj.Save();            }            return oObj;        }        /// <summary>        /// Creates a cSubUser object. It will be saved in permanent storage only        /// on calling Save()        /// </summary>        /// <returns>cSubUser object</returns>        public static cSubUser Create()        {            cSubUser oObj = new cSubUser();                        SecurityCheck((int)enSubUser_Action.Create);                        // Create an object in memory, will be saved to storage on calling Save()            oObj.m_bCreating = true;            oObj.m_bInvalid = false;            return oObj;        }        /// <summary>        /// Get the object with the given ID        /// </summary>        /// <param name="i_iID">ID</param>        /// <returns>Object or null</returns>        public static cSubUser Get_ID(int i_iID)        {            List<cSubUser> a = Find(new cFilter(new cFilterParameter("iID", i_iID.ToString())));            if (a.Count > 0)            {                                SecurityCheck(a[0].iID, (int)enSubUser_Action.Get);                return a[0];            }            else return null;        }        /// <summary>        /// Get the object with the given Name        /// </summary>        /// <param name="i_sName">Name</param>        /// <returns>Object or null</returns>        public static cSubUser Get_Name(string i_sName)        {            List<cSubUser> a = Find(new cFilter(new cFilterParameter("sName", i_sName)));            if (a.Count > 0)            {                                SecurityCheck(a[0].iID, (int)enSubUser_Action.Get);                return a[0];            }            else return null;        }        /// <summary>        /// Returns a DataTable of all objects        /// </summary>        /// <returns>DataTable</returns>        public static DataTable Find_DataTable()        {            return Find_DataTable(new cFilter(), new cFilterOutput());        }        /// <summary>        /// Returns a DataTable of all objects with desc name as column headers        /// </summary>        /// <returns>DataTable</returns>        //public static DataTable Find_DataTable_DescriptiveNameColumns()        //{        //    return Find_DataTable_DescriptiveNameColumns(new cFilter());        //}        ///// <summary>        ///// Returns a DataTable of all objects with desc name as column headers        ///// </summary>        ///// <param name="i_sFilter">Filter criteria (WHERE clause)</param>        ///// <returns>DataTable</returns>        //public static DataTable Find_DataTable_DescriptiveNameColumns(string i_sFilter)        //{        //    return Find_DataTable_DescriptiveNameColumns(new cFilter(i_sFilter));        //}        ///// <summary>        ///// Returns a DataTable of all objects with desc name as column headers        ///// </summary>        ///// <param name="i_oFilter">Filter criteria (WHERE clause)</param>        ///// <returns>DataTable</returns>        //public static DataTable Find_DataTable_DescriptiveNameColumns(cFilter i_oFilter)        //{        //    cFilterOutput oOutputs = new cFilterOutput();        //    for (int i = 0; i < m_oClass.aProperties.Length; i++)        //    {        //        oOutputs.Add(new cOutputField(m_oClass.aProperties[i].sName, m_oClass.aProperties[i].sDescriptiveName));        //    }        //    return Find_DataTable(new cFilter(), oOutputs);        //}        /// <summary>        /// Returns a DataTable of records matching the specified criteria        /// </summary>        /// <param name="i_sFilter">Filter criteria (WHERE clause)</param>        /// <param name="i_sOutputFields">Fields to return (SELECT clause), pass null to return all fields(SELECT *)</param>        /// <returns>DataTable</returns>        public static DataTable Find_DataTable(string i_sFilter, string i_sOutputFields)        {            return Find_DataTable(new cFilter(i_sFilter), new cFilterOutput(i_sOutputFields));        }        /// <summary>        /// Returns a DataTable of deleted records matching the specified criteria        /// </summary>        /// <param name="i_oFilter">Filter criteria (WHERE clause)</param>        /// <param name="i_oOutputFields">Fields to return (SELECT clause), pass null to return all fields(SELECT *)</param>        /// <returns>DataTable</returns>        public static DataTable Find_DataTable(cFilter i_oFilter, cFilterOutput i_oOutputFields)        {                        SecurityCheck((int)enSubUser_Action.Find);                        List<SqlParameter> a = new List<SqlParameter>();            a.Add(new SqlParameter("@sFilter", SqlDbType.VarChar, 8000));            if (i_oFilter == null)            {                a[a.Count - 1].Value = "";            }            else            {                a[a.Count - 1].Value = i_oFilter.ToSQL_WHERE_Clause(new cSubUser());            }                        a[a.Count - 1].Value = GetSecurityScopeFilter(a[a.Count - 1].Value.ToString(), (int)enSubUser_Action.Find);                        a.Add(new SqlParameter("@sSelect", SqlDbType.VarChar, 8000));            if (i_oOutputFields == null)            {                a[a.Count - 1].Value = "*";            }            else            {                a[a.Count - 1].Value = i_oOutputFields.ToString(new cSubUser());            }            DataTable dt = new DataTable();            bool bNoFilterOutput = i_oOutputFields == null;            if (!bNoFilterOutput)            {                bNoFilterOutput = i_oOutputFields.aOutputs.Count < 1;            }            if (bNoFilterOutput)            {                dt.Columns.Add("iID");                dt.Columns.Add("sName");                dt.Columns.Add("dtCreatedOn");                dt.Columns.Add("dtLastUpdatedOn");                                dt.Columns.Add("sEmailID");                dt.Columns.Add("objLoginUser");                dt.Columns.Add("bIsActive");                dt.Columns.Add("bIsVerified");            }            else            {                for (int i = 0; i < i_oOutputFields.aOutputs.Count; i++)                {                    dt.Columns.Add(i_oOutputFields.aOutputs[i].sName);                }            }            oDB.CallSPROC(m_oClass.sName + "_Find", a, dt);            if (!bNoFilterOutput)            {                for (int i = 0; i < i_oOutputFields.aOutputs.Count; i++)                {                    dt.Columns[i_oOutputFields.aOutputs[i].sName].ColumnName = i_oOutputFields.aOutputs[i].ToString();                }            }            return dt;                    }        /// <summary>        /// Finds and return all cSubUser objects        /// </summary>        /// <returns>cSubUser objects</returns>        public static List<cSubUser> Find()        {            return Find(new cFilter());        }        /// <summary>        /// Finds and return cSubUser objects matching the specified criteria        /// </summary>        /// <param name="i_sFilter">Filter criteria (WHERE clause)</param>        /// <returns>cSubUser objects</returns>        public static List<cSubUser> Find(string i_sFilter)        {            return Find(new cFilter(i_sFilter));        }        /// <summary>        /// Finds and return cSubUser objects matching the specified criteria        /// </summary>        /// <param name="i_oFilter">Filter criteria (WHERE clause)</param>        /// <returns>cSubUser objects</returns>        public static List<cSubUser> Find(cFilter i_oFilter)        {                        DataTable dt = Find_DataTable(i_oFilter, null);            List<cSubUser> l = new List<cSubUser>();            cSubUser oObj;            for (int i = 0; i < dt.Rows.Count; i++)            {                oObj = new cSubUser();                oObj.m_iID = Convert.ToInt32(dt.Rows[i]["iID"]);                oObj.m_sName = dt.Rows[i]["sName"].ToString();                oObj.m_dtCreatedOn = Convert.ToDateTime(dt.Rows[i]["dtCreatedOn"]);                oObj.m_dtLastUpdatedOn = Convert.ToDateTime(dt.Rows[i]["dtLastUpdatedOn"]);                                oObj.m_sEmailID = Convert.ToString(dt.Rows[i]["sEmailID"]);                oObj.m_objLoginUser.iObjectID = Convert.ToInt32(dt.Rows[i]["objLoginUser"].ToString());                oObj.m_bIsActive = Convert.ToBoolean(dt.Rows[i]["bIsActive"]);                oObj.m_bIsVerified = Convert.ToBoolean(dt.Rows[i]["bIsVerified"]);                oObj.m_bInvalid = false;                l.Add(oObj);            }            return l;                    }        /// <summary>        /// Returns the total number of objects        /// </summary>        /// <returns>Total number of objects</returns>        public static int Count()        {            return Count(new cFilter());        }        /// <summary>        /// Returns the count of object matching the specified criteria        /// </summary>        /// <param name="i_sFilter">Filter condition (WHERE clause)</param>        /// <returns>Total number of objects found matching the specified criteria</returns>        public static int Count(string i_sFilter)        {            return Count(new cFilter(i_sFilter));        }        /// <summary>        /// Returns the count of object matching the specified criteria        /// </summary>        /// <param name="i_oFilter">Filter condition (WHERE clause)</param>        /// <returns>Total number of objects found matching the specified criteria</returns>        public static int Count(cFilter i_oFilter)        {                        SecurityCheck((int)enSubUser_Action.Find);                        List<SqlParameter> a = new List<SqlParameter>();            a.Add(new SqlParameter("@sFilter", SqlDbType.VarChar, 8000));            if (i_oFilter == null)            {                a[a.Count - 1].Value = "";            }            else            {                a[a.Count - 1].Value = i_oFilter.ToSQL_WHERE_Clause(new cSubUser());            }                        DataTable dt = new DataTable();            dt.Columns.Add("iCount");            oDB.CallSPROC(m_oClass.sName + "_Count", a, dt);            return Convert.ToInt32(dt.Rows[0]["iCount"]);                    }        /*        /// <summary>        /// Deletes objects        /// </summary>        /// <param name="i_sFilter">Filter condition(WHERE clause)</param>        public static void Delete(string i_sFilter)        {            Delete(new cFilter(i_sFilter));        }        */        /// <summary>        /// Deletes objects        /// </summary>        /// <param name="i_iID">ID of the object to be deleted</param>        public static void Delete(int i_iID)        {                        SecurityCheck((int)enSubUser_Action.Delete);            SecurityCheck(i_iID, (int)enSubUser_Action.Delete);                        List<SqlParameter> a = new List<SqlParameter>();            a.Add(new SqlParameter("@iID", SqlDbType.Int));            a[a.Count - 1].Value = i_iID;                        oDB.CallSPROC(m_oClass.sName + "_Delete_ID", a);                    }        /// <summary>        /// Deletes all objects!        /// </summary>        public static void DeleteAll()        {            List<cSubUser> a_oAll = Find();            for (int i = 0; i < a_oAll.Count; i++)            {                Delete(a_oAll[i].iID);            }        }        /// <summary>        /// Saves the newly created object        /// </summary>        public virtual void Save()        {            MakeSureObjectIsValid();            if (m_bCreating)            {                // Create                _onValidateBeforeSaving();                                List<SqlParameter> a = new List<SqlParameter>();                a.Add(new SqlParameter("@iID", SqlDbType.Int));                a[a.Count - 1].Direction = ParameterDirection.Output;                a.Add(new SqlParameter("@sName", SqlDbType.VarChar, 100));                a[a.Count - 1].Value = m_sName;                a.Add(new SqlParameter("@sEmailID", SqlDbType.VarChar, 100));                a[a.Count-1].Value = m_sEmailID;                a.Add(new SqlParameter("@objLoginUser", SqlDbType.BigInt, 0));                a[a.Count-1].Value = m_objLoginUser.iObjectID;                a.Add(new SqlParameter("@bIsActive", SqlDbType.Bit, 0));                a[a.Count-1].Value = m_bIsActive;                a.Add(new SqlParameter("@bIsVerified", SqlDbType.Bit, 0));                a[a.Count-1].Value = m_bIsVerified;                                oDB.CallSPROC(m_oClass.sName + "_Create", a);                m_iID = Convert.ToInt32(a[0].Value);                                m_bCreating = false;            }            else            {                // Update                                SecurityCheck(m_iID, (int)enSubUser_Action.Update);                _onValidateBeforeSaving();                                List<SqlParameter> a = new List<SqlParameter>();                a.Add(new SqlParameter("@iID", SqlDbType.Int));                a[a.Count - 1].Value = m_iID;                a.Add(new SqlParameter("@sName", SqlDbType.VarChar, 100));                a[a.Count - 1].Value = m_sName;                a.Add(new SqlParameter("@sEmailID", SqlDbType.VarChar, 100));                a[a.Count-1].Value = m_sEmailID;                a.Add(new SqlParameter("@objLoginUser", SqlDbType.BigInt, 0));                a[a.Count-1].Value = m_objLoginUser.iObjectID;                a.Add(new SqlParameter("@bIsActive", SqlDbType.Bit, 0));                a[a.Count-1].Value = m_bIsActive;                a.Add(new SqlParameter("@bIsVerified", SqlDbType.Bit, 0));                a[a.Count-1].Value = m_bIsVerified;                                oDB.CallSPROC(m_oClass.sName + "_Update_ID", a);                            }        }        /// <summary>        /// This is invoked on calling Save() i.e. while creation of objects        /// Overload this function to do custom validations        /// </summary>        public virtual void _onValidateBeforeSaving()        {            // Throw cValidationException here if validation is not successful        }        /// <summary>        /// For Internal Use Only        /// </summary>        /// <param name="i_sPropertyName"></param>        /// <param name="i_enOperator"></param>        /// <param name="i_sPropertyValue"></param>        /// <returns></returns>        public string _ValidateSQLFilterParameter(string i_sPropertyName, enComparison i_enOperator, string i_sPropertyValue)        {                        switch (i_sPropertyName)            {                case "iID":                    return cMSSQL.ValidateSQLFilterParameter(enDataType.Int32, i_sPropertyName, i_enOperator, i_sPropertyValue);                case "sName":                    return cMSSQL.ValidateSQLFilterParameter(enDataType.String, i_sPropertyName, i_enOperator, i_sPropertyValue);                case "iVersion":                    return cMSSQL.ValidateSQLFilterParameter(enDataType.Int32, i_sPropertyName, i_enOperator, i_sPropertyValue);                case "dtCreatedOn":                    return cMSSQL.ValidateSQLFilterParameter(enDataType.DateTime, i_sPropertyName, i_enOperator, i_sPropertyValue);                case "iCreatedBy":                    return cMSSQL.ValidateSQLFilterParameter(enDataType.String, i_sPropertyName, i_enOperator, i_sPropertyValue);                case "dtLastUpdatedOn":                    return cMSSQL.ValidateSQLFilterParameter(enDataType.DateTime, i_sPropertyName, i_enOperator, i_sPropertyValue);                case "iLastUpdatedBy":                    return cMSSQL.ValidateSQLFilterParameter(enDataType.String, i_sPropertyName, i_enOperator, i_sPropertyValue);                        case "sEmailID":                return cMSSQL.ValidateSQLFilterParameter(enDataType.String, i_sPropertyName, i_enOperator, i_sPropertyValue);            case "objLoginUser":                return cMSSQL.ValidateSQLFilterParameter(enDataType.ObjectReference, i_sPropertyName, i_enOperator, i_sPropertyValue);            case "bIsActive":                return cMSSQL.ValidateSQLFilterParameter(enDataType.Boolean, i_sPropertyName, i_enOperator, i_sPropertyValue);            case "bIsVerified":                return cMSSQL.ValidateSQLFilterParameter(enDataType.Boolean, i_sPropertyName, i_enOperator, i_sPropertyValue);            }            throw new cInvalidFilterParameterException(i_sPropertyName);        }        /// <summary>        /// For Internal Use Only        /// </summary>        /// <param name="i_sPropertyName"></param>        /// <param name="i_sPropertyValue"></param>        /// <returns></returns>        public string _ValidateSQLUpdateParameter(string i_sPropertyName, string i_sPropertyValue)        {                        switch (i_sPropertyName)            {                case "iID":                    return cMSSQL.ValidateSQLUpdateParameter(enDataType.Int32, i_sPropertyName, i_sPropertyValue);                case "sName":                    return cMSSQL.ValidateSQLUpdateParameter(enDataType.String, i_sPropertyName, i_sPropertyValue);                case "iVersion":                    return cMSSQL.ValidateSQLUpdateParameter(enDataType.Int32, i_sPropertyName, i_sPropertyValue);                case "dtCreatedOn":                    return cMSSQL.ValidateSQLUpdateParameter(enDataType.DateTime, i_sPropertyName, i_sPropertyValue);                case "iCreatedBy":                    return cMSSQL.ValidateSQLUpdateParameter(enDataType.String, i_sPropertyName, i_sPropertyValue);                case "dtLastUpdatedOn":                    return cMSSQL.ValidateSQLUpdateParameter(enDataType.DateTime, i_sPropertyName, i_sPropertyValue);                case "iLastUpdatedBy":                    return cMSSQL.ValidateSQLUpdateParameter(enDataType.String, i_sPropertyName, i_sPropertyValue);                        case "sEmailID":                return cMSSQL.ValidateSQLUpdateParameter(enDataType.String, i_sPropertyName, i_sPropertyValue);            case "objLoginUser":                return cMSSQL.ValidateSQLUpdateParameter(enDataType.ObjectReference, i_sPropertyName, i_sPropertyValue);            case "bIsActive":                return cMSSQL.ValidateSQLUpdateParameter(enDataType.Boolean, i_sPropertyName, i_sPropertyValue);            case "bIsVerified":                return cMSSQL.ValidateSQLUpdateParameter(enDataType.Boolean, i_sPropertyName, i_sPropertyValue);            }            throw new cInvalidUpdateParameterException(i_sPropertyName);        }        /// <summary>        /// For Internal Use Only        /// </summary>        /// <param name="i_sPropertyName"></param>        public void _ValidateSQLOutputParameter(string i_sPropertyName)        {                        switch (i_sPropertyName)            {                case "iID":                case "sName":                case "iVersion":                case "dtCreatedOn":                case "iCreatedBy":                case "dtLastUpdatedOn":                case "iLastUpdatedBy":                        case "sEmailID":            case "objLoginUser":            case "bIsActive":            case "bIsVerified":             return;            }            throw new cInvalidOutputParameterException(i_sPropertyName);        }    }    /// <summary>    /// The properties of cSubUser objects    /// Never use the integer values of these, only the names should be used    /// </summary>    public enum enSubUser_Property    {        iID = 0,        sName = 1,        iVersion = 2,        dtCreatedOn = 3,        iCreatedBy = 4,        dtLastUpdatedOn = 5,        iLastUpdatedBy = 6                    ,sEmailID = 1000,        objLoginUser = 1001,        bIsActive = 1002,        bIsVerified = 1003    }    /// <summary>    /// The actions of cSubUser objects    /// Permissions can be assigned for these actions only    /// </summary>    public enum enSubUser_Action    {        Create = 0,        Find = 1,        Delete = 2,        Update = 3,        Get = 4    }}