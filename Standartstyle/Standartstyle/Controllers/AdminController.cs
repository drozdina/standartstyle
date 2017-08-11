using Standartstyle.AppCode.DAL.Repository;
using Standartstyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Standartstyle.Controllers
{
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
            var categories = repo.GoodsCategoryRepository.All;
            var catalog = new CatalogModel();
            foreach (var category in categories)
            {
                var goods = new List<GoodModel>();
                try
                {
                    var goodsFromDB = repo.GoodsRepository.All.Where(elem => elem.CATEGORYCODE == category.CATEGORYCODE).ToList();
                    foreach (var goodFromDB in goodsFromDB)
                    {
                        var images = new List<ImageModel>();
                        var imagesFromDB = repo.ImageRepository.All.Where(elem => elem.GOODCODE == goodFromDB.GOODCODE).ToList();
                        foreach (var imageFromDB in imagesFromDB)
                        {

                        }

                        var good = new GoodModel()
                        {
                            GoodCode = goodFromDB.GOODCODE,
                            Name = goodFromDB.NAME
                        };
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return View();
        }
    }
}