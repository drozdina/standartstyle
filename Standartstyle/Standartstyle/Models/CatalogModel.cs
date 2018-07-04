using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.Models
{
    public class PageInfoModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }

    public class CatalogModel
    {
        public IEnumerable<GoodsCategoryModel> Categories { get; set; }
        public IEnumerable<GoodModel> GoodsForActiveCategory { get; set; }
        public GoodsCategoryModel ActiveCategory { get; set; }
        public PageInfoModel PageInfo { get; set; }
        public CatalogModel()
        {
            Categories = new List<GoodsCategoryModel>();
            GoodsForActiveCategory = new List<GoodModel>();
            ActiveCategory = new GoodsCategoryModel();
            PageInfo = new PageInfoModel();
        }
    }
}