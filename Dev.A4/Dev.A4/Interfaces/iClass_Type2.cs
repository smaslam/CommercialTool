using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dev.A4.Enums;

namespace Dev.A4.Interfaces
{
    public interface iClass_Type2 : iClass_Type1
    {
        /// <summary>
        /// State
        /// </summary>
        int iState { get; }
        /// <summary>
        /// State
        /// </summary>
        enState enState { get; }

    }
}