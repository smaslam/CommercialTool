using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using Dev.A4.Enums;
using Dev.A4.Exceptions;
using Dev.A4.DataTypes;
using Dev.A4.General;
using Dev.A4.Internal;

namespace Dev.A4.Storages
{
    public class cMSSQL:cStorage
    {
        private static List<cMSSQL> m_aDBs = new List<cMSSQL>();

        /// <summary>
        /// Returns the default(first) database storage, this will be
        /// used by default for all classes whose oStorage property has
        /// not been initialized explicitly
        /// </summary>
        /// <returns></returns>
        public static cMSSQL GetDefault()
        {
            return m_aDBs[0];
        }

        //-----------------------------------------

        protected string m_sConnectionString = null;

        public cMSSQL()
        {
            m_aDBs.Add(this);
        }

        public cMSSQL(string i_sConnectionString)
        {
            m_aDBs.Add(this);
            m_sConnectionString = i_sConnectionString;
        }

        /// <summary>
        /// Connectionstring
        /// </summary>
        public string sConnectionString
        {
            get { return m_sConnectionString; }
            set { m_sConnectionString = value; }
        }

        public void CallSPROC(string i_sSPROCName, List<SqlParameter> i_a_oParameters)
        {
            CallSPROC(i_sSPROCName, i_a_oParameters, null);
        }

        public void CallSPROC(string i_sSPROCName, List<SqlParameter> i_a_oParameters, DataTable i_dt)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(m_sConnectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(i_sSPROCName, con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i < i_a_oParameters.Count; i++)
                        {
                            command.Parameters.Add(i_a_oParameters[i]);
                        }
                        SqlDataReader dr = command.ExecuteReader(CommandBehavior.SingleResult);
                        if (i_dt != null)
                        {
                            if (dr.HasRows)
                            {
                                DataRow r;
                                while (dr.Read())
                                {
                                    r = i_dt.NewRow();
                                    for (int i = 0; i < dr.FieldCount; i++)
                                    {
                                        if (i_dt.Columns.Contains(dr.GetName(i)))
                                        {
                                            r[dr.GetName(i)] = dr.GetValue(i);
                                        }
                                    }
                                    i_dt.Rows.Add(r);
                                }
                            }
                        }
                    }
                    con.Close();
                }
            }
            catch (SqlException ex)
            {
                StringBuilder sbErr = new StringBuilder();
                foreach (SqlError er in ex.Errors)
                {
                    sbErr.Append(er.Message + "\n");
                }
               // cSystem.oApplication.LogError("cMSSQL", "CallSPROC", "SqlException:" + sbErr.ToString(), ex.StackTrace, "");
                throw;
            }
        }

        public static string RemoveUnacceptableSQLCharacters(string i_sInput)
        {
            i_sInput = i_sInput.Replace(";", " ").Replace("'", "''").Replace("--", " ").Replace("]", "]]");
            string sTemp = i_sInput.ToLower();
            if (sTemp.Contains(" create ") || sTemp.Contains(" alter ") || sTemp.Contains(" drop ") || sTemp.Contains(" update ") || sTemp.Contains(" insert ") || sTemp.Contains(" select ") || sTemp.Contains(" delete "))
                return string.Empty;
            return i_sInput;
        }

        public static string ValidateSQLFilterParameter(enDataType i_enDataType, string i_sPropertyName, enComparison i_enOperator, string i_sPropertyValue)
        {
            string sValue = cMSSQL.RemoveUnacceptableSQLCharacters(i_sPropertyValue);
            if (i_sPropertyValue != sValue) throw new cInvalidFilterParameterValueException("Suspicious Characters/SQL: " + i_sPropertyName + " = " + i_sPropertyValue);
            string sCode = string.Empty;
            switch (i_enDataType)
            {
                case enDataType.Boolean:
                    sCode = Convert.ToBoolean(i_sPropertyValue) ? "1" : "0";
                    break;
                case enDataType.Int32:
                    sCode = Convert.ToInt32(i_sPropertyValue).ToString();
                    break;
                case enDataType.Int64:
                    sCode = Convert.ToInt64(i_sPropertyValue).ToString();
                    break;
                case enDataType.DateTime:
                    sCode = "'" + Convert.ToDateTime(i_sPropertyValue).ToString() + "'";
                    break;
                case enDataType.Currency:
                    sCode = Convert.ToSingle(i_sPropertyValue).ToString();
                    break;
                case enDataType.String:
                case enDataType.NameOfPerson:
                case enDataType.EMailID:
                case enDataType.Phone:
                case enDataType.URL:
                    {
                        if (i_enOperator == enComparison.Like)
                        {
                            if (!i_sPropertyValue.Contains("%")) throw new cInvalidFilterParameterValueException("'LIKE' without '%' : " + i_sPropertyName + " = " + i_sPropertyValue);
                        }
                        else
                        {
                            if (i_sPropertyValue.Contains("%")) throw new cInvalidFilterParameterValueException("not 'LIKE' but '%' found : " + i_sPropertyName + " = " + i_sPropertyValue);
                        }
                        sCode = "'" + i_sPropertyValue + "'";
                    }
                    break;
                case enDataType.ObjectReference:
                    sCode = Convert.ToInt64(i_sPropertyValue).ToString();
                    break;
                case enDataType.Float:
                    sCode = Convert.ToSingle(i_sPropertyValue).ToString();
                    break;
                default:
                    throw new cInvalidFilterParameterValueException("Unknown datatype");
            }
            return sCode;
        }

        //public void InitializeClass(string i_sClassID, string i_sClassName, string i_sNamespace, string i_sDescription)
        //{
        //    List<SqlParameter> a = new List<SqlParameter>();
        //    a.Add(new SqlParameter("sID", SqlDbType.VarChar, 100));
        //    a[a.Count - 1].Value = i_sClassID;
        //    a.Add(new SqlParameter("sName", SqlDbType.VarChar, 100));
        //    a[a.Count - 1].Value = i_sClassName;
        //    a.Add(new SqlParameter("sNamespace", SqlDbType.VarChar, 100));
        //    a[a.Count - 1].Value = i_sNamespace;
        //    a.Add(new SqlParameter("sDescription", SqlDbType.VarChar, 2000));
        //    a[a.Count - 1].Value = i_sDescription;
        //    CallSPROC("cClass_Register", a);
        //}

        //public void InitializeClassActions(string i_sClassID, List<cObjectProxy> i_aActions)
        //{
        //    for (int i = 0; i < i_aActions.Count; i++)
        //    {
        //        List<SqlParameter> a = new List<SqlParameter>();
        //        a.Add(new SqlParameter("sClassID", SqlDbType.VarChar, 100));
        //        a[a.Count - 1].Value = i_sClassID;
        //        a.Add(new SqlParameter("iActionID", SqlDbType.Int, 0));
        //        a[a.Count - 1].Value = Convert.ToInt32(i_aActions[i].sID);
        //        a.Add(new SqlParameter("sActionName", SqlDbType.VarChar, 100));
        //        a[a.Count - 1].Value = i_aActions[i].sName;
        //        CallSPROC("cClassAction_Register", a);
        //    }
        //}

        public static string ValidateSQLUpdateParameter(enDataType i_enDataType, string i_sPropertyName, string i_sPropertyValue)
        {
            string sValue = cMSSQL.RemoveUnacceptableSQLCharacters(i_sPropertyValue);
            if (i_sPropertyValue != sValue) throw new cInvalidFilterParameterValueException("Suspicious Characters/SQL: " + i_sPropertyName + " = " + i_sPropertyValue);
            string sCode = string.Empty;
            switch (i_enDataType)
            {
                case enDataType.Boolean:
                    sCode = Convert.ToBoolean(i_sPropertyValue) ? "1" : "0";
                    break;
                case enDataType.Int32:
                    sCode = Convert.ToInt32(i_sPropertyValue).ToString();
                    break;
                case enDataType.Int64:
                    sCode = Convert.ToInt64(i_sPropertyValue).ToString();
                    break;
                case enDataType.DateTime:
                    sCode = "'" + Convert.ToDateTime(i_sPropertyValue).ToString() + "'";
                    break;
                case enDataType.Currency:
                    sCode = Convert.ToSingle(i_sPropertyValue).ToString();
                    break;
                case enDataType.String:
                case enDataType.NameOfPerson:
                case enDataType.EMailID:
                case enDataType.Phone:
                case enDataType.URL:
                    {
                        if (i_sPropertyValue.Contains("%")) throw new cInvalidFilterParameterValueException("'%' found : " + i_sPropertyName + " = " + i_sPropertyValue);
                        sCode = "'" + i_sPropertyValue + "'";
                    }
                    break;
                case enDataType.ObjectReference:
                    sCode = Convert.ToInt64(i_sPropertyValue).ToString();
                    break;
                case enDataType.Float:
                    sCode = Convert.ToSingle(i_sPropertyValue).ToString();
                    break;
                default:
                    throw new cInvalidFilterParameterValueException("Unknown datatype");
            }
            return sCode;
        }
    }
}