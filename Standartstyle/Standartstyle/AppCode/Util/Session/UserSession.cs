using Standartstyle.AppCode.BL;
using Standartstyle.AppCode.DAL.Repository;
using Standartstyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.AppCode.Util.Session
{
    public class UserSession
    {
        public const string SESSION_NAME = "_user_session_";
        public UserModel User { get; set; }
        public DateTime LastRequestTime { get; set; }
        private DateTime _createdDate;
        private static int _expiredHours = 12;

        public object this[string key]
        {
            get
            {
                return HttpContext.Current.Session[key];
            }
            set
            {
                HttpContext.Current.Session[key] = value;
            }
        }


        public static UserSession Current
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                HttpContext ctx = HttpContext.Current;
                if (ctx != null && ctx.Session != null)
                    return (ctx.Session[SESSION_NAME] as UserSession);
                else
                    return null;
            }
        }

        public static UserSession Create(String userName, String password, bool withException = false)
        {
            GeneralRepository repo = new GeneralRepository();

            UserSession ret = null;
            UserModel userData = null;

            try
            {
                userData = repo.UsersRepository.CheckUser(userName);
                if (userData == null)
                {
                    throw new Exception("User not exist");
                }

                string hashedPass = Utils.HashedPassword(password);
                string userPass = userData.Password;
                if (!hashedPass.Equals(userPass))
                {
                    throw new Exception("Incorrect username or password");
                }
            }
            catch (Exception e)
            {
                e.ToString();
                throw new Exception("", e);
            }

            if (userData != null)
            {
                ret = new UserSession();
                ret._createdDate = DateTime.UtcNow;
                ret.LastRequestTime = DateTime.UtcNow;

                // [Create user session:]

                ret.User = userData;

                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session[SESSION_NAME] = ret;
                }
            }
            else
            {
                throw new Exception();
            }
            return ret;
        }

        public static string SessionID
        {
            get
            {
                return HttpContext.Current != null && HttpContext.Current.Session != null ? HttpContext.Current.Session.SessionID : string.Empty;
            }
        }

        public static string ClientIP
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }

        public static bool DestroyIfExpired()
        {
            if (Current != null)
            {
                if (Current._createdDate.AddHours(_expiredHours) <= DateTime.UtcNow)
                {
                    HttpContext.Current.Session.Remove(SESSION_NAME);
                    return true;
                }
            }
            return false;
        }

        public static void Remove()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}