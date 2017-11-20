using Standartstyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.AppCode.Utils
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

        public static UserSession Create(String userName, String password, bool withException)
        {
            UserSession ret = null;
            UserModel userData = null;

            try
            {
                userData = UserRepository.GetUserByLogin(userName);
                if (userData == null)
                {
                    throw new UserException(Resources.Exception.LoginOrPasswordIsWrong + " ", (int)CIP.AppCode.Entities.UserData.LoginState.ERROR);
                }

                string hashedPass = UserData.HashedPassword(password);
                string userPass = userData.Password;
                if (!hashedPass.Equals(userPass))
                {
                    throw new UserException(Resources.Exception.LoginOrPasswordIsWrong + " ", (int)CIP.AppCode.Entities.UserData.LoginState.ERROR);
                }
            }
            catch (UserException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new Exception(Resources.Exception.ServerMaintenance, e);
            }

            if (userData != null)
            {
                userData.IsUserValid(withException);
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
                throw new Exception(Resources.Exception.UserDoesNotExist);
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

        public string GetFromCache(string fileKey)
        {
            return (CIPCache.Get(fileKey) as string);
        }

        public void WriteInCache(string fileKey, string fp, DateTime? absoluteExpiration = null, CacheItemRemovedCallback removing = null)
        {
            CIPCache.Write(fileKey, fp, absoluteExpiration, removing);
        }
        private void ClearCache(string fileKey)
        {
            CIPCache.Remove(fileKey);
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