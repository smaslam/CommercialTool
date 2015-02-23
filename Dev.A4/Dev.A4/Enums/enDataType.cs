using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Enums
{
    public enum enDataType
    {
        /// <summary>
        /// Boolean: true or false
        /// </summary>
        Boolean = 0,
        /// <summary>
        /// 32 bit integer
        /// </summary>
        Int32 = 1,
        /// <summary>
        /// 64 bit integer
        /// </summary>
        Int64 = 2,
        /// <summary>
        /// String
        /// </summary>
        String = 3,
        /// <summary>
        /// Date and time
        /// </summary>
        DateTime = 4,
        /// <summary>
        /// TODO: Currency
        /// </summary>
        Currency = 5,
        /// <summary>
        /// TODO: Name of a person
        /// </summary>
        NameOfPerson = 6,
        /// <summary>
        /// TODO: Email ID
        /// </summary>
        EMailID = 7,
        /// <summary>
        /// TODO: Phone number
        /// </summary>
        Phone = 8,
        /// <summary>
        /// TODO: URL
        /// </summary>
        URL = 9,
        /// <summary>
        /// Object
        /// </summary>
        ObjectReference = 10,
        /// <summary>
        /// Floating point number
        /// </summary>
        Float = 11
    }
}