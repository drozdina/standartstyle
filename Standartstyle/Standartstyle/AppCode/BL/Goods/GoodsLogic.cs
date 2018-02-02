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
                GOODS newElement = new GOODS {
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
    }
}