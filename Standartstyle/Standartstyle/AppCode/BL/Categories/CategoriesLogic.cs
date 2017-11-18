using Standartstyle.AppCode.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Standartstyle.AppCode.BL.Categories
{
    public class CategoriesLogic
    {
        #region for WEB elements
        public IEnumerable<SelectListItem> createExistingCategoriesDropdownList()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            categories.Add(new SelectListItem
            {
                Value = "0",
                Text = "Категория"
            });

            using (GeneralRepository repo = new GeneralRepository())
            {
                var categoriesFromDB = repo.GoodsCategoryRepository.All.OrderBy(elem => elem.NAME);
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
            }
            return categories;
        }
        #endregion
    }
}