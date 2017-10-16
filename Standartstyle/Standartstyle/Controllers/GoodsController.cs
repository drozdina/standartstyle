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
        // GET: Goods
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form()
        {
            GoodModel model = new GoodModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveGood()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }
    }
}