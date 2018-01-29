using Standartstyle.App_Start.Filters;
using Standartstyle.AppCode.DAL.Repository;
using Standartstyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Standartstyle.Controllers
{
    [SessionExpireFilter]
    public class AdminController : Controller
    {
        private GeneralRepository repo = new GeneralRepository();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Catalog()
        {
            var categories = repo.GoodsCategoryRepository.Get();
            var allCategoriesElement = new GoodsCategoryModel
            {
                Code = -1,
                Name = "Весь каталог"
            };

            var catalog = new CatalogModel();

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
                        SetImagesForGood(good);
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

            return View(catalog);
        }

        private void SetImagesForGood(GoodModel good)
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