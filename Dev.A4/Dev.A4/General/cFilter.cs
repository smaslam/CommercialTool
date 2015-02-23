using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Dev.A4.Enums;
using Dev.A4.Exceptions;
using Dev.A4.Interfaces;

namespace Dev.A4.General
{
    public class cFilter
    {
        protected List<object> f = new List<object>();

        public cFilter() { 
        
        }
        /// <summary>
        /// This constructor only supports the following limited types of filter criteria:
        /// {property} {operator} {value} 
        /// {property} {operator} {value} and {property} {operator} {value} and ...
        /// {property} {operator} {value} or {property} {operator} {value} or ...
        /// where operator can be: = not equals, less than, greater than, less than or equals, greater than or equals or like with spaces around them
        /// {value} may or maynot be enclosed in '(single quotes)
        /// NOTE: "and", "or" and "like" have to be in lowercase and also put spaces around them
        /// </summary>
        /// <param name="i_sFilterCriteria">Criteria</param>

        public cFilter(string i_sFilterCriteria)
        {
            if (!string.IsNullOrEmpty(i_sFilterCriteria))
            {
                if (i_sFilterCriteria.Contains(" and "))
                {
                    // AND caluse
                    ExtractClause(i_sFilterCriteria, enLogical.AND);
                }
                else
                {
                    if (i_sFilterCriteria.Contains(" or "))
                    {
                        // OR caluse
                        ExtractClause(i_sFilterCriteria, enLogical.OR);
                    }
                    else
                    {
                        // Single property
                        f.Add(ExtractParameter(i_sFilterCriteria));
                    }
                }
            }
        }

        private void ExtractClause(string i_sFilterCriteria, enLogical i_enLogical)
        {
            string[] aLogical = new string[1] { " " + i_enLogical.ToString().ToLower() + " " };
            string[] a = i_sFilterCriteria.Split(aLogical, StringSplitOptions.RemoveEmptyEntries);
            cFilterParameter p;
            for (int i = 0; i < a.Length; i++)
            {
                p = ExtractParameter(a[i]);
                if (i_enLogical == enLogical.AND)
                {
                    AND_Add(p);
                }
                else
                {
                    OR_Add(p);
                }
            }
        }
        private cFilterParameter ExtractParameter(string i_sFilterCriteria)
        {
            string[] a = i_sFilterCriteria.Split(new char[1] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);
            if (a.Length < 3) throw new cInvalidFilterParameterException(i_sFilterCriteria);
            a[2] = a[2].Trim();
            enComparison en;
            switch (a[1])
            {
                case "=":
                    en = enComparison.Equals;
                    break;
                case "<>":
                    en = enComparison.NotEquals;
                    break;
                case "<":
                    en = enComparison.LessThan;
                    break;
                case ">":
                    en = enComparison.GreaterThan;
                    break;
                case "<=":
                    en = enComparison.LessThanOrEquals;
                    break;
                case ">=":
                    en = enComparison.GreaterThanOrEquals;
                    break;
                case "like":
                    en = enComparison.Like;
                    if (!a[2].Contains("%")) throw new cInvalidFilterParameterException("No % :" + i_sFilterCriteria);
                    break;
                default:
                    throw new cInvalidFilterParameterException("Invalid operator " + a[1] + ": " + i_sFilterCriteria);
            }
            if (a[2].Length > 2 && a[2].StartsWith("'") && a[2].EndsWith("'"))
            {
                a[2] = a[2].Substring(1, a[2].Length - 2);
            }
            return new cFilterParameter(a[0], en, a[2]);
        }
        public cFilter(cFilterParameter i_oParam)
        {
            f.Add(i_oParam);
        }

        public void Add(cFilterParameter i_oParam)
        {
            if (f.Count < 1)
            {
                f.Add(i_oParam);
            }
            else throw new cLogicalOperatorRequiredException();
        }

        public void Add(cFilter i_oFilter)
        {
            if (f.Count < 1)
            {
                f.Add(i_oFilter);
            }
            else throw new cLogicalOperatorRequiredException();
        }

        public void Add(enLogical i_enOperator, cFilterParameter i_oParam)
        {
            if (f.Count > 0)
            {
                f.Add(i_enOperator);
            }
            f.Add(i_oParam);
        }

        public void Add(enLogical i_enOperator, cFilter i_oFilter)
        {
            if (f.Count > 0)
            {
                f.Add(i_enOperator);
            }
            f.Add(i_oFilter);
        }

        public void AND_Add(cFilterParameter i_oParam)
        {
            if (f.Count > 0)
            {
                f.Add(enLogical.AND);
            }
            f.Add(i_oParam);
        }

        public void OR_Add(cFilterParameter i_oParam)
        {
            if (f.Count > 0)
            {
                f.Add(enLogical.OR);
            }
            f.Add(i_oParam);
        }

        public void AND_Add(enLogical i_enOperator, cFilter i_oFilter)
        {
            if (f.Count > 0)
            {
                f.Add(enLogical.AND);
            }
            f.Add(i_oFilter);
        }

        public void OR_Add(enLogical i_enOperator, cFilter i_oFilter)
        {
            if (f.Count > 0)
            {
                f.Add(enLogical.OR);
            }
            f.Add(i_oFilter);
        }

        public string ToString(iClass_Base i_oObject)
        {
            StringBuilder sb = new StringBuilder();
            bool bOperator = false;
            sb.Append('(');
            for (int i = 0; i < f.Count; i++)
            {
                if (bOperator)
                {
                    sb.Append(' ');
                    switch ((enLogical)f[i])
                    {
                        case enLogical.AND:
                            sb.Append("AND");
                            break;
                        case enLogical.OR:
                            sb.Append("OR");
                            break;
                    }
                    sb.Append(' ');
                }
                else
                {
                    if (f[i] is cFilter)
                    {
                        sb.Append((f[i] as cFilter).ToString(i_oObject));
                    }
                    else
                    {
                        sb.Append((f[i] as cFilterParameter).ToString(i_oObject));
                    }
                }
                bOperator = !bOperator;
            }
            sb.Append(')');
            return sb.ToString();
        }

        public object ToSQL_WHERE_Clause(iClass_Base i_oObject)
        {
            if (f.Count < 1) return string.Empty;
            return " WHERE " + ToString(i_oObject);
        }

    }
    public class cFilterParameter
    {
        public bool bNegate = false;
        public string sName = string.Empty;
        public enComparison enOperator = enComparison.Equals;
        public string sValue = string.Empty;

        public cFilterParameter()
        {
        }

        public cFilterParameter(string i_sName, string i_sValue)
        {
            sName = i_sName;
            enOperator = enComparison.Equals;
            sValue = i_sValue;
        }

        public cFilterParameter(string i_sName, enComparison i_enOperator, string i_sValue)
        {
            sName = i_sName;
            enOperator = i_enOperator;
            sValue = i_sValue;
        }

        public cFilterParameter(bool i_bNegate, string i_sName, enComparison i_enOperator, string i_sValue)
        {
            bNegate = i_bNegate;
            sName = i_sName;
            enOperator = i_enOperator;
            sValue = i_sValue;
        }

        public string ToString(iClass_Base i_oObject)
        {
            string sValue1 = i_oObject._ValidateSQLFilterParameter(sName, enOperator, sValue);
            StringBuilder sb = new StringBuilder();
            if (bNegate) sb.Append("NOT ");
            sb.Append('(');
            sb.Append(sName);
            sb.Append(' ');
            switch (enOperator)
            {
                case enComparison.Equals:
                    sb.Append('=');
                    break;
                case enComparison.NotEquals:
                    sb.Append("<>");
                    break;
                case enComparison.LessThan:
                    sb.Append('<');
                    break;
                case enComparison.GreaterThan:
                    sb.Append('>');
                    break;
                case enComparison.LessThanOrEquals:
                    sb.Append("<=");
                    break;
                case enComparison.GreaterThanOrEquals:
                    sb.Append(">=");
                    break;
                case enComparison.Like:
                    sb.Append("LIKE");
                    break;
            }
            sb.Append(' ');
            sb.Append(sValue1);
            sb.Append(')');
            return sb.ToString();
        }
    }
}