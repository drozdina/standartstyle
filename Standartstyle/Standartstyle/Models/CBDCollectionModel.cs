using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.Models
{
    public class CBDCollectionModel
    {
        public int CollectionCode { get; set; }
        public string Name { get; set; }
        public CBDManufacturerModel Manufacturer { get; set; }

        public CBDCollectionModel()
        {
            this.Manufacturer = new CBDManufacturerModel();
        }
    }
}