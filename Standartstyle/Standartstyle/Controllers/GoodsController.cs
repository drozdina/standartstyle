using Standartstyle.AppCode.BL.Categories;
using Standartstyle.AppCode.BL.Goods;
using Standartstyle.AppCode.BL.Images;
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
        private GoodsLogic goodsLogic = new GoodsLogic();
        private ImagesLogic imagesLogic = new ImagesLogic();
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
        public JsonResult SaveGood(GoodModel model)
        {
            var result = new List<object>();
            if (ModelState.IsValid)
            {
                model = goodsLogic.CreateNewGood(model);
                if (model.GoodCode > 0)
                {
                    List<string> notCopied = imagesLogic.CreateNewImage(model);

                }
                else
                {
                    result.Add(new { status = false });
                    result.Add(new { message = "Не удалось сохранить новый товар!" });
                }
            }
            else
            {
                result.Add(new { status = false });
                result.Add(new { message = "Не заполнены все обязательные поля!" });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List()
        {
            return PartialView("_List");
        }
        #endregion
    }
}