using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.Models
{
    public class CBDColorModel
    {
        public int ColorCode { get; set; }
        public string Name { get; set; }
        public CBDCollectionModel Collection { get; set; }
        public ImageModel Image { get; set; }

        public CBDColorModel()
        {
            this.Collection = new CBDCollectionModel();
            this.Image = new ImageModel();
        }
    }
}