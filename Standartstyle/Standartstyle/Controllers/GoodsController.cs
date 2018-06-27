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

        #region HTTP methods
        [HttpGet]
        // GET: Goods
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        // GET: Good Form
        public ActionResult Form(int? id)
        {
            GoodModel model = createNewGoodModel();
            if (id.HasValue)
            {
                fillModelFromDB(id.Value, model);
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult SaveGood(GoodModel model)
        {
            var result = new List<object>();
            if (ModelState.IsValid)
            {
                var results = new List<object>();
                if (model.GoodCode > 0)
                {
                    results = editExistingGood(model);
                }
                else
                {
                    results = saveNewGood(model);
                }
                result.AddRange(results);

            }
            else
            {
                result.Add(new { status = false });
                result.Add(new { message = "Не заполнены все обязательные поля!" });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult List()
        {
            return PartialView("_List");
        }
        #endregion

        #region private logic
        private GoodModel createNewGoodModel()
        {
            GoodModel model = new GoodModel();
            model.Categories = categoriesLogic.createExistingCategoriesDropdownList();
            return model;
        }

        private void fillModelFromDB(int id, GoodModel model)
        {
            model = goodsLogic.SelectGoodData(id, model);
            model = imagesLogic.SelectImagesForModel(model);
        }

        private List<object> saveNewGood(GoodModel model)
        {
            var result = new List<object>();
            model = goodsLogic.CreateNewGood(model);
            if (model.GoodCode > 0)
            {
                List<string> notCopied = imagesLogic.CreateNewImage(model);
                result.Add(new { status = true });
                if (notCopied.Count > 0)
                {
                    result.Add(new { data = notCopied.ToString() });
                }
                result.Add(new { controller = "Admin" });
                result.Add(new { action = "Catalog" });
                result.Add(new { message = "Товар сохранен успешно!" });
            }
            else
            {
                result.Add(new { status = false });
                result.Add(new { message = "Не удалось сохранить новый товар!" });
            }
            return result;
        }

        private List<object> editExistingGood(GoodModel model)
        {
            var result = new List<object>();
            var isUpdated = goodsLogic.EditExistingGood(model);
            if (isUpdated)
            {
                isUpdated = imagesLogic.UpdateImageDataForGoodModel(model);
                List<string> notCopied = imagesLogic.CreateNewImage(model);
                result.Add(new { status = true });
                if (notCopied.Count > 0)
                {
                    result.Add(new { data = notCopied.ToString() });
                }
                result.Add(new { controller = "Admin" });
                result.Add(new { action = "Catalog" });
                result.Add(new { message = "Данные изменены успешно!" });
            }
            else
            {
                result.Add(new { status = false });
                result.Add(new { message = "Не удалось обновить данные для товара!" });
            }
            return result;
        }
        #endregion

        #endregion
    }
}