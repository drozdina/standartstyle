using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Standartstyle.AppCode.BL
{
    public static class Extensions
    {
        public static string HashSHA1(this string str)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(str);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            string hashedStr = Convert.ToBase64String(inArray);

            return hashedStr;
        }
    }
}