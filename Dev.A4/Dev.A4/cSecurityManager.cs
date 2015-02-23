using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dev.A4.Storages;
using System.Data.SqlClient;
using System.Data;

namespace Dev.A4
{
    public static class cSecurityManager
    {
        public const int ALLOW_ALL_ACTIONS = -1000;

        /// <summary>
        /// Never use this directly, even in this class, always use oStorage
        /// </summary>
        private static cStorage _oStorage = null;
        /*CG#IF:MSSQL*/
        /// <summary>
        /// cMSSQL database used for storage
        /// </summary>
        public static cMSSQL oDB
        {
            get { return (oStorage as cMSSQL); }
        }
        /*CG#ENDIF:MSSQL*/
        public static cStorage oStorage
        {
            get
            {
                /*CG#IF:MSSQL*/
                if (_oStorage == null) oStorage = cMSSQL.GetDefault();
                /*CG#ENDIF:MSSQL*/
                return _oStorage;
            }
            set
            {
                _oStorage = value;
            }
        }

        //// MSSQL:START
        //private static string m_sDatabaseConnectionString = string.Empty;
        //public static string sDatabaseConnectionString
        //{
        //    get { return m_sDatabaseConnectionString; }
        //    set
        //    {
        //        m_sDatabaseConnectionString = value;
        //    }
        //}
        //// MSSQL:END

        // Role Management

        public static int CreateRole(string i_sName)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("sName", SqlDbType.VarChar, 100));
            a[a.Count - 1].Value = i_sName;
            DataTable dt = new DataTable();
            dt.Columns.Add("iID");
            oDB.CallSPROC("cSecurityRole_Create", a, dt);
            return Convert.ToInt32(dt.Rows[0]["iID"]);
            // MSSQL:END
        }

        public static DataTable FindRoles()
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            DataTable dt = new DataTable();
            dt.Columns.Add("iID");
            dt.Columns.Add("sName");
            oDB.CallSPROC("cSecurityRole_Find", a, dt);
            return dt;
            // MSSQL:END
        }

        public static void DeleteRole(int i_iID)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("iID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iID;
            oDB.CallSPROC("cSecurityRole_Delete", a);
            // MSSQL:END
        }

        public static void AssignRoleToUser(int i_iUserID, int i_iRoleID)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("iRoleID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iRoleID;
            a.Add(new SqlParameter("iUserID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iUserID;
            oDB.CallSPROC("cSecurityRole_AssignToUser", a);
            // MSSQL:END
        }

        public static void ClearRolesOfUser(string i_iUserID)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("iUserID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iUserID;
            oDB.CallSPROC("cSecurityRole_ClearAllUserRoles", a);
            // MSSQL:END       
        }

        public static void RemoveRoleFromUser(int i_iUserID, int i_iRoleID)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("iRoleID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iRoleID;
            a.Add(new SqlParameter("iUserID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iUserID;
            oDB.CallSPROC("cSecurityRole_RemoveFromUser", a);
            // MSSQL:END
        }

        // User Permissions

        public static void UserPermissions_Clear(int i_iUserID)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("iUserID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iUserID;
            oDB.CallSPROC("cSecurityPermission_ClearUser", a);
            // MSSQL:END
        }

        public static void UserPermission_AddClass(int i_iUserID, string i_sClassID, int i_iAction)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("iUserID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iUserID;
            a.Add(new SqlParameter("sClassID", SqlDbType.VarChar, 100));
            a[a.Count - 1].Value = i_sClassID;
            a.Add(new SqlParameter("iActionID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iAction;
            oDB.CallSPROC("cSecurityPermission_AddUser_Class", a);
            // MSSQL:END
        }

        public static void UserPermission_ClearClass(int i_iUserID, string i_sClassID)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("iUserID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iUserID;
            a.Add(new SqlParameter("sClassID", SqlDbType.VarChar, 100));
            a[a.Count - 1].Value = i_sClassID;
            oDB.CallSPROC("cSecurityPermission_ClearUser_Class", a);
            // MSSQL:END
        }

        public static void UserPermission_AddObject(int i_iUserID, string i_sClassID, int i_iObjectID, int i_iAction)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("iUserID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iUserID;
            a.Add(new SqlParameter("sClassID", SqlDbType.VarChar, 100));
            a[a.Count - 1].Value = i_sClassID;
            a.Add(new SqlParameter("iObjectID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iObjectID;
            a.Add(new SqlParameter("iActionID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iAction;
            oDB.CallSPROC("cSecurityPermission_AddUser_Object", a);
            // MSSQL:END
        }

        public static void UserPermission_ClearObject(int i_iUserID, string i_sClassID, int i_iObjectID)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("iUserID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iUserID;
            a.Add(new SqlParameter("sClassID", SqlDbType.VarChar, 100));
            a[a.Count - 1].Value = i_sClassID;
            a.Add(new SqlParameter("iObjectID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iObjectID;
            oDB.CallSPROC("cSecurityPermission_ClearUser_Object", a);
            // MSSQL:END
        }

        public static DataTable FindUserPermissions(int i_iUserID)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("iUserID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iUserID;
            DataTable dt = new DataTable();
            dt.Columns.Add("sClassID");
            dt.Columns.Add("iObjectID");
            dt.Columns.Add("iActionID");
            dt.Columns.Add("sActionName");
            oDB.CallSPROC("cSecurityPermission_User_Find", a, dt);
            return dt;
            // MSSQL:END
        }

        // Role Permissions

        public static void RolePermissions_Clear(int i_iRoleID)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("iRoleID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iRoleID;
            oDB.CallSPROC("cSecurityPermission_ClearRole", a);
            // MSSQL:END
        }

        public static void RolePermission_AddClass(int i_iRoleID, string i_sClassID, int i_iAction)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("iRoleID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iRoleID;
            a.Add(new SqlParameter("sClassID", SqlDbType.VarChar, 100));
            a[a.Count - 1].Value = i_sClassID;
            a.Add(new SqlParameter("iActionID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iAction;
            oDB.CallSPROC("cSecurityPermission_AddRole_Class", a);
            // MSSQL:END
        }

        public static void RolePermission_ClearClass(int i_iRoleID, string i_sClassID)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("iRoleID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iRoleID;
            a.Add(new SqlParameter("sClassID", SqlDbType.VarChar, 100));
            a[a.Count - 1].Value = i_sClassID;
            oDB.CallSPROC("cSecurityPermission_ClearRole_Class", a);
            // MSSQL:END
        }

        public static void RolePermission_AddObject(int i_iRoleID, string i_sClassID, int i_iObjectID, int i_iAction)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("iRoleID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iRoleID;
            a.Add(new SqlParameter("sClassID", SqlDbType.VarChar, 100));
            a[a.Count - 1].Value = i_sClassID;
            a.Add(new SqlParameter("iObjectID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iObjectID;
            a.Add(new SqlParameter("iActionID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iAction;
            oDB.CallSPROC("cSecurityPermission_AddRole_Object", a);
            // MSSQL:END
        }

        public static void RolePermission_ClearObject(int i_iRoleID, string i_sClassID, int i_iObjectID)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("iRoleID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iRoleID;
            a.Add(new SqlParameter("sClassID", SqlDbType.VarChar, 100));
            a[a.Count - 1].Value = i_sClassID;
            a.Add(new SqlParameter("iObjectID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iObjectID;
            oDB.CallSPROC("cSecurityPermission_ClearRole_Object", a);
            // MSSQL:END
        }

        public static DataTable FindRolePermissions(int i_iRoleID)
        {
            // MSSQL:START
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("iRoleID", SqlDbType.Int, 0));
            a[a.Count - 1].Value = i_iRoleID;
            DataTable dt = new DataTable();
            dt.Columns.Add("sClassID");
            dt.Columns.Add("iObjectID");
            dt.Columns.Add("iActionID");
            dt.Columns.Add("sActionName");
            oDB.CallSPROC("cSecurityPermission_Role_Find", a, dt);
            return dt;
            // MSSQL:END
        }
    }
}