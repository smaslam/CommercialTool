using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dev.A4.Enums;

namespace Dev.A4.Interfaces
{
    public interface iClass_Type4 : iClass_Type3
    {
        /// <summary>
        /// CMS State
        /// </summary>
        enCMSState enCMSState { get; }
    }
}