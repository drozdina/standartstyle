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
        private GeneralRepository repo;
        public GoodsLogic()
        {
            repo = new GeneralRepository();
        }
        public GoodModel SelectGoodData(int code, GoodModel model)
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
                model.IsVisible = goodFromDB.IS_VISIBLE ?? 0;
            }
            return model;
        }

        public GoodModel CreateNewGood(GoodModel newGood)
        {
            GOODS newElement = new GOODS
            {
                NAME = newGood.Name,
                CATEGORYCODE = newGood.SelectedCategoryCode,
                WIDTH = newGood.Width,
                HEIGHT = newGood.Height,
                DEPTH = newGood.Depth,
                DESCRIPTION = newGood.Description,
                IS_VISIBLE = newGood.IsVisible
            };
            repo.GoodsRepository.Create(newElement);
            if (newElement.GOODCODE > 0)
            {
                newGood.GoodCode = newElement.GOODCODE;
            }
            return newGood;
        }

        public Boolean EditExistingGood(GoodModel good)
        {
            var result = false;
            try
            {
                var goodFromDB = repo.GoodsRepository.FindById(good.GoodCode);

                goodFromDB.NAME = good.Name;
                goodFromDB.CATEGORYCODE = good.SelectedCategoryCode;
                goodFromDB.WIDTH = good.Width;
                goodFromDB.HEIGHT = good.Height;
                goodFromDB.DEPTH = good.Depth;
                goodFromDB.DESCRIPTION = good.Description;
                goodFromDB.IS_VISIBLE = good.IsVisible;

                repo.GoodsRepository.Update(goodFromDB);
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при обновлении данных о товаре." + ex.StackTrace);
            }
            return result;
        }

        public int GetActiveGoodsCount(GeneralRepository repo, int categoryCode)
        {
            var goodsFromDB = new List<GOODS>();
            if (categoryCode == -1)
            {
                goodsFromDB = repo.GoodsRepository.Get(good => good.IS_VISIBLE > 0).ToList();
            }
            else
            {
                goodsFromDB = repo.GoodsRepository.Get(good => good.IS_VISIBLE > 0 && good.CATEGORYCODE == categoryCode).ToList();
            }
            return goodsFromDB.Count;
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
            if (imagesFromDB.Count > 0)
            {
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
            }
            else
            {
                var noImage = new ImageModel()
                {
                    ImageCode = -1,
                    Name = "no-image",
                    Path = "~/Content/img/",
                    Extension = "png"
                };

                images.Add(noImage);
            }

            good.Images = images;
            good.MainImageIndex = mainImageIndex;
        }
    }
}