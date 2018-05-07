using Standartstyle.AppCode.BL.Categories;
using Standartstyle.AppCode.BL.Goods;
using Standartstyle.AppCode.DAL.Repository;
using Standartstyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.AppCode.BL
{
    public class AdminUtils
    {
        private CategoriesLogic categoriesLogic;
        private GoodsLogic goodsLogic;

        public AdminUtils()
        {
            categoriesLogic = new CategoriesLogic();
            goodsLogic = new GoodsLogic();
        }

        public CatalogModel prepareCatalogModelForView(GeneralRepository repo, int? categoryCode, int? page, int? range)
        {
            var catalog = new CatalogModel();
            catalog.Categories = categoriesLogic.createGoodsCategoryModel(repo);
            if (!categoryCode.HasValue)
            {
                catalog.ActiveCategory = catalog.Categories.Where(cat => cat.Code == -1).FirstOrDefault();
            }
            else
            {
                catalog.ActiveCategory = catalog.Categories.Where(cat => cat.Code == categoryCode.Value).FirstOrDefault();
            }


            if (!page.HasValue)
            {
                page = 1;
            }
            if (!range.HasValue)
            {
                range = 20;
            }

            catalog.GoodsForActiveCategory = goodsLogic.SelectRangeOfGoods(repo, catalog.ActiveCategory.Code, page.Value, range.Value);
            return catalog;
        }
    }
}