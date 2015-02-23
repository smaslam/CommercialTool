using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Enums
{
        public enum enComparison
        {
            /// <summary>
            /// =
            /// </summary>
            Equals = 0,
            /// <summary>
            /// !=
            /// </summary>
            NotEquals = 1,
            /// <summary>
            /// Less than
            /// </summary>
            LessThan = 2,
            /// <summary>
            /// Greater than
            /// </summary>
            GreaterThan = 3,
            /// <summary>
            /// Less than or Equals
            /// </summary>
            LessThanOrEquals = 4,
            /// <summary>
            /// Greater than of Equals
            /// </summary>
            GreaterThanOrEquals = 5,
            /// <summary>
            /// Like
            /// </summary>
            Like = 6
        }
}