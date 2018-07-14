using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.Models
{
    public class FormCallbackModel
    {
        public String Message { get; set; }
        public Boolean Status { get; set; }
        public List<object> Data { get; set; }
    }
}