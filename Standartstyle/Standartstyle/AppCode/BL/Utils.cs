using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.AppCode.BL
{
    public class Utils
    {
        public static string HashedPassword(string pass)
        {
            return pass.HashSHA1();
        }
    }
}