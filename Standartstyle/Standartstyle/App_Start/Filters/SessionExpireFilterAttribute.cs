using Standartstyle.AppCode.Util.Session;
using Standartstyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Standartstyle.App_Start.Filters
{
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // check if session is supported
            UserModel objCurrentCustomer = new UserModel();
            UserSession currentSession = UserSession.Current;
            if (currentSession == null)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }
            else if (currentSession.User == null)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}