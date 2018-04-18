﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.Models
{
    public class CatalogModel
    {
        public IEnumerable<GoodsCategoryModel> Categories { get; set; }
        public IEnumerable<GoodModel> GoodsForActiveCategory { get; set; }
        public GoodsCategoryModel ActiveCategory { get; set; }

        public CatalogModel()
        {
            Categories = new List<GoodsCategoryModel>();
            GoodsForActiveCategory = new List<GoodModel>();
            ActiveCategory = new GoodsCategoryModel();
        }
    }
}