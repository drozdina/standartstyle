using Standartstyle.AppCode.DAL.Repository;
using Standartstyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Standartstyle.AppCode.BL.Categories
{
    public class CategoriesLogic
    {
        private GeneralRepository repo;
        public CategoriesLogic()
        {
            repo = new GeneralRepository();
        }
        #region for WEB elements
        public IEnumerable<SelectListItem> createExistingCategoriesDropdownList()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            categories.Add(new SelectListItem
            {
                Value = "0",
                Text = "Категория",
                Disabled = true
            });

            var categoriesFromDB = repo.GoodsCategoryRepository.Get().OrderBy(elem => elem.NAME);
            if (categoriesFromDB != null && categoriesFromDB.Count() > 0)
            {
                foreach (var category in categoriesFromDB)
                {
                    categories.Add(new SelectListItem
                    {
                        Value = category.CATEGORYCODE.ToString(),
                        Text = category.NAME
                    });
                }
            }

            return categories;
        }

        public IEnumerable<GoodsCategoryModel> createGoodsCategoryModel(GeneralRepository repo)
        {
            var categories = new List<GoodsCategoryModel>();
            var allCategoriesElement = new GoodsCategoryModel
            {
                Code = -1,
                Name = "Весь каталог"
            };


            var categoriesFromDB = repo.GoodsCategoryRepository.Get();
            foreach (var category in categoriesFromDB)
            {
                var goodCategory = new GoodsCategoryModel()
                {
                    Code = category.CATEGORYCODE,
                    Name = category.NAME
                };
                categories.Add(goodCategory);
            }

            categories = categories.OrderBy(cat => cat.Name).ToList();
            categories.Insert(0, allCategoriesElement);

            return categories;
        }
        #endregion        
    }
}