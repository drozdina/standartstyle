using Standartstyle.AppCode.DAL.Model;
using Standartstyle.AppCode.DAL.Repository;
using Standartstyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.AppCode.BL.Goods
{
    public class GoodsLogic
    {

        public GoodModel SelectGoodData(int code, GoodModel model)
        {
            using (GeneralRepository repo = new GeneralRepository())
            {
                var goodFromDB = repo.GoodsRepository.Get(good => good.GOODCODE == code).FirstOrDefault();
                if (goodFromDB != null)
                {
                    model.GoodCode = goodFromDB.GOODCODE;
                    model.Name = goodFromDB.NAME;
                    model.SelectedCategoryCode = goodFromDB.CATEGORYCODE.Value;
                    model.Width = goodFromDB.WIDTH;
                    model.Height = goodFromDB.HEIGHT;
                    model.Depth = goodFromDB.DEPTH;
                    model.Description = goodFromDB.DESCRIPTION;
                }
            }
            return model;
        }

        public GoodModel CreateNewGood(GoodModel newGood)
        {
            using (GeneralRepository repo = new GeneralRepository())
            {
                GOODS newElement = new GOODS
                {
                    NAME = newGood.Name,
                    CATEGORYCODE = newGood.SelectedCategoryCode,
                    WIDTH = newGood.Width,
                    HEIGHT = newGood.Height,
                    DEPTH = newGood.Depth,
                    DESCRIPTION = newGood.Description
                };
                repo.GoodsRepository.Create(newElement);
                if (newElement.GOODCODE > 0)
                {
                    newGood.GoodCode = newElement.GOODCODE;
                }
            }
            return newGood;
        }

        public IEnumerable<GoodModel> SelectRangeOfGoods(GeneralRepository repo, int categoryCode, int page, int range)
        {
            var goods = new List<GoodModel>();
            var goodsFromDB = new List<GOODS>();

            var skipRange = (page - 1) * range;
            if (categoryCode == -1)
            {
                goodsFromDB = repo.GoodsRepository.Get().OrderBy(good => good.GOODCODE).Skip(skipRange).Take(range).ToList();
            }
            else
            {
                goodsFromDB = repo.GoodsRepository.Get(good => good.CATEGORYCODE == categoryCode).Skip(skipRange).Take(range).ToList();
            }

            foreach (var goodFromDB in goodsFromDB)
            {
                var categoryFromDB = repo.GoodsCategoryRepository.Get(cat => cat.CATEGORYCODE == goodFromDB.CATEGORYCODE).FirstOrDefault();
                var category = new GoodsCategoryModel();
                if (categoryFromDB != null)
                {
                    category = new GoodsCategoryModel()
                    {
                        Code = categoryFromDB.CATEGORYCODE,
                        Name = categoryFromDB.NAME
                    };
                }

                var good = new GoodModel()
                {
                    GoodCode = goodFromDB.GOODCODE,
                    Name = goodFromDB.NAME,
                    SelectedCategory = category
                };
                SetImagesForGood(repo, good);
                goods.Add(good);
            }
            return goods;
        }

        private void SetImagesForGood(GeneralRepository repo, GoodModel good)
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
                    Path = imageFromDB.LOCATION,
                    Extension = imageFromDB.EXTENSION
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