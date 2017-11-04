using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.AppCode.BL.Images
{
    public class ImagesLogic
    {
        private static string _DOT = ".";
        private static string _DASH = "-";
        public static string generateFilename(string filename)
        {
            string filenameForSave = DateTime.Now.ToFileTimeUtc().ToString() + _DASH;

            var fileLexemes = filename.Split('.');
            int extIdx = fileLexemes.Length - 1;
            var extension = fileLexemes[extIdx];
            for (var idx = 0; idx < extIdx; idx++)
                filenameForSave += fileLexemes[idx];

            filenameForSave += _DOT + extension;

            return filenameForSave;
        }
    }
}