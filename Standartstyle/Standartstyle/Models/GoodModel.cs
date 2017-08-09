using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.Models
{
    public class GoodModel
    {
        public int GoodCode { get; set; }
        public string Name { get; set; }
        public int MainImageIndex { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}