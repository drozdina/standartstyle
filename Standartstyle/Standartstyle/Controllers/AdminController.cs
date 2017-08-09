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

                }
                catch (Exception ex)
                {

                }
                finally
                {

                }

            }
            return View();
        }
    }
}