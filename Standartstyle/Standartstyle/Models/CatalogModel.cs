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
        public GoodsCategoryModel ActiveCategory { get; set; }

        public CatalogModel()
        {
            GoodsMap = new Dictionary<GoodsCategoryModel, IEnumerable<GoodModel>>();
            AttributesMap = new Dictionary<GoodsCategoryModel, IEnumerable<AttributeModel>>();
            ActiveCategory = new GoodsCategoryModel();
        }
    }
}