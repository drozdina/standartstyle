using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.App_Code.Util
{
    public class Constants
    {
        public const string MAIN_CONNECTION = "CipData";

        public static DateTime UNIX_TIME
        {
            get { return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); }
        }
    }
}