using System;
using System.Collections.Generic;
using System.Configuration;
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

        public static string GetDefaultImage()
        {
            return "";
        }
    }

    public class Configuration
    {
        public static string UploadDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["UploadLocation"];
            }
        }

        public static string FileDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["FileLocation"];
            }
        }
    }
}