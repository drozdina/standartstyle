using Standartstyle.AppCode.BL.Categories;
using Standartstyle.AppCode.Util.Session;
using Standartstyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Standartstyle.Controllers
{
    public class GoodsController : Controller
    {
        #region Variables
        private CategoriesLogic categoriesLogic = new CategoriesLogic();
        #endregion

        #region Actions
        // GET: Goods
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form()
        {
            UserSession.Create("admin", "admin");
            GoodModel model = new GoodModel();
            model.Categories = categoriesLogic.createExistingCategoriesDropdownList();
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveGood(GoodModel model)
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }
        #endregion
    }
}