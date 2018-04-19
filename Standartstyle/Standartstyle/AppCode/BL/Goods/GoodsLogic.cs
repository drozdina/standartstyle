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

        public IEnumerable<GoodModel> selectRangeOfGoods(GeneralRepository repo, int categoryCode, int page, int range)
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
                var good = new GoodModel()
                {
                    GoodCode = goodFromDB.GOODCODE,
                    Name = goodFromDB.NAME
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