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
            var callback = new FormCallbackModel();
            if (ModelState.IsValid)
            {
                if (model.GoodCode > 0)
                {
                    callback = editExistingGood(model);
                }
                else
                {
                    callback = saveNewGood(model);
                }
            }
            else
            {
                callback.Status = false;
                callback.Message = "Не заполнены все обязательные поля!";
            }

            return Json(callback, JsonRequestBehavior.AllowGet);
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

        private FormCallbackModel saveNewGood(GoodModel model)
        {
            var callback = new FormCallbackModel();
            model = goodsLogic.CreateNewGood(model);
            if (model.GoodCode > 0)
            {
                List<string> notCopied = imagesLogic.CreateNewImage(model);
                callback.Status = true;
                callback.Message = "Товар сохранен успешно!";
                List<object> data = new List<object>();
                if (notCopied.Count > 0)
                {
                    data.Add(new { notCopied = notCopied.ToString() });
                }
                var url = new UrlHelper(this.ControllerContext.RequestContext).Action("Catalog", "Admin");
                data.Add(new { url = url });

                callback.Data = data;
            }
            else
            {
                callback.Status = false;
                callback.Message = "Не удалось сохранить новый товар!";
            }
            return callback;
        }

        private FormCallbackModel editExistingGood(GoodModel model)
        {
            var callback = new FormCallbackModel();
            var isUpdated = goodsLogic.EditExistingGood(model);
            if (isUpdated)
            {
                isUpdated = imagesLogic.UpdateImageDataForGoodModel(model);
                List<string> notCopied = imagesLogic.CreateNewImage(model);
                callback.Status = true;
                callback.Message = "Данные изменены успешно!";
                List<object> data = new List<object>();
                if (notCopied.Count > 0)
                {
                    data.Add(new { notCopied = notCopied.ToString() });
                }
                var url = new UrlHelper(this.ControllerContext.RequestContext).Action("Catalog", "Admin");
                data.Add(new { url = url });
                callback.Data = data;
            }
            else
            {
                callback.Status = false;
                callback.Message = "Не удалось сохранить новый товар!";
            }
            return callback;
        }
        #endregion

        #endregion
    }
}