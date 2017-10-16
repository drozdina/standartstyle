using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.Models
{
    public class CBDManufacturerModel
    {
        public int ManufacturerCode { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public CBDManufacturerModel()
        {

        }
    }
}