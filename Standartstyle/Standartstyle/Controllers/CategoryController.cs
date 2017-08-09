using Standartstyle.AppCode.DAL.Repository;
using Standartstyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Standartstyle.Controllers
{
    public class CategoryController : Controller
    {
        private GeneralRepository repo = new GeneralRepository();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var categories = repo.GoodsCategoryRepository.All.ToList();
            var forView = new List<GoodsCategoryModel>();
            foreach (var category in categories)
            {
                forView.Add(new GoodsCategoryModel()
                {
                    Code = category.CATEGORYCODE,
                    Name = category.NAME
                });
            }
            return PartialView("_List" ,forView);
        }

        public ActionResult Form(int? code)
        {
            return View();
        }
    }
}