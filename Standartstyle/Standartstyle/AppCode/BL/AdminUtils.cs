﻿using Standartstyle.AppCode.BL.Categories;
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

        public CatalogModel prepareCatalogModelForView(GeneralRepository repo, int categoryCode, int page, int range)
        {
            var catalog = new CatalogModel();
            catalog.Categories = categoriesLogic.createGoodsCategoryModel(repo);
            catalog.ActiveCategory = catalog.Categories.Where(cat => cat.Code == categoryCode).FirstOrDefault();
            catalog.GoodsForActiveCategory = goodsLogic.SelectRangeOfGoods(repo, catalog.ActiveCategory.Code, page, range);
            catalog.PageInfo = new PageInfoModel
            {
                CurrentPageNumber = page,
                PageSize = range,
                TotalItems = goodsLogic.GetActiveGoodsCount(repo, catalog.ActiveCategory.Code)
            };
            return catalog;
        }
    }
}