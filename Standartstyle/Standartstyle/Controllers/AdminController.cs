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
        private GeneralRepository repo;

        private AdminUtils adminUtils;

        public AdminController()
        {
            repo = new GeneralRepository();
            adminUtils = new AdminUtils();
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Catalog(int categoryCode = -1, int page = 1, int range = 20)
        {
            var catalog = adminUtils.prepareCatalogModelForView(repo, categoryCode, page, range);
            return View(catalog);
        }
    }
}