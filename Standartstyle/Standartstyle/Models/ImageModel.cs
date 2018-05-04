using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.Models
{
    public class ImageModel
    {
        public static string _DOT = ".";

        public int ImageCode { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public bool MainImageFlag { get; set; }

        public string GetFullFilenameForTemporaryFile()
        {
            return this.Name + _DOT + this.Extension;
        }

        public string GetFullFilenameForStoredFile()
        {
            return this.ImageCode + _DOT + this.Extension;
        }
    }
}