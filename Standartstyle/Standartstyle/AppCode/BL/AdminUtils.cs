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


            catalog.GoodsForActiveCategory = goodsLogic.selectRangeOfGoods(repo, catalog.ActiveCategory.Code, page, range);


            return catalog;
        }

        public static CatalogModel prepareCatalogModelForView(GeneralRepository repo, )
        {
            var catalog = new CatalogModel();
            var categories = repo.GoodsCategoryRepository.Get();
            var allCategoriesElement = new GoodsCategoryModel
            {
                Code = -1,
                Name = "Весь каталог"
            };

            catalog.GoodsMap.Add(allCategoriesElement, null);
            foreach (var category in categories)
            {
                var goods = new List<GoodModel>();
                try
                {
                    var goodsFromDB = repo.GoodsRepository.Get().Where(elem => elem.CATEGORYCODE == category.CATEGORYCODE).ToList();
                    foreach (var goodFromDB in goodsFromDB)
                    {
                        var good = new GoodModel()
                        {
                            GoodCode = goodFromDB.GOODCODE,
                            Name = goodFromDB.NAME
                        };
                        SetImagesForGood(repo, good);
                        goods.Add(good);
                    }
                }
                catch (Exception ex)
                {

                }

                var goodCategory = new GoodsCategoryModel()
                {
                    Code = category.CATEGORYCODE,
                    Name = category.NAME
                };
                catalog.GoodsMap.Add(goodCategory, goods);

            }

            catalog.ActiveCategory = allCategoriesElement;

            return catalog;
        }

        private static void SetImagesForGood(GeneralRepository repo, GoodModel good)
        {
            var images = new List<ImageModel>();
            var imagesFromDB = repo.ImageRepository.Get().Where(elem => elem.GOODCODE == good.GoodCode).ToList();
            var mainImageIndex = 0;
            foreach (var imageFromDB in imagesFromDB)
            {
                var image = new ImageModel()
                {
                    ImageCode = imageFromDB.IMAGECODE,
                    Name = imageFromDB.NAME,
                    Path = imageFromDB.LOCATION
                };

                images.Add(image);

                if (imageFromDB.IS_MAIN.HasValue && imageFromDB.IS_MAIN.Value)
                {
                    mainImageIndex = images.IndexOf(image);
                }
            }

            good.Images = images;
            good.MainImageIndex = mainImageIndex;
        }
    }
}