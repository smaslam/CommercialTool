using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dev.A4.Enums;

namespace Dev.A4.Interfaces
{
    public interface iApplication
    {
        bool bIsOffline { get; }

        void Start(string i_sConfiguration);
       
    }
    }
