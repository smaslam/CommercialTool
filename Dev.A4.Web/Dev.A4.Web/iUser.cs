using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.A4.Web
{
   public interface  iUser
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="i_sLoginID">User ID</param>
        /// <param name="i_sPassword">Password</param>
        /// <returns>iUser</returns>
        iUser Login(string i_sLoginID, string i_sPassword);
    }
}
