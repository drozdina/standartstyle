using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.Models
{
    public class CatalogModel
    {
        public Dictionary<GoodsCategoryModel, IEnumerable<GoodModel>> GoodsMap { get; set; }
        public Dictionary<GoodsCategoryModel, IEnumerable<AttributeModel>> AttributesMap { get; set; }
    }
}