using Standartstyle.App_Start.Filters;
using Standartstyle.AppCode.BL;
using Standartstyle.AppCode.DAL.Repository;
using Standartstyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Standartstyle.Controllers
{
    [SessionExpireFilter]
    public class AdminController : Controller
    {
        private GeneralRepository repo = new GeneralRepository();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Catalog(int? categoryCode, int? page)
        {
            var catalog = AdminUtils.prepareCatalogModelForView(repo, categoryCode, page);
            return View(catalog);
        }

       
    }
}