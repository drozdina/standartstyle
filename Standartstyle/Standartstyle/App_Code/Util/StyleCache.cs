using System;
using System.Threading;
using System.Web.Caching;

namespace Standartstyle.App_Code.Util
{
    public class StyleCache
    {
        private static ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        private static Cache _cache
        {
            get
            {
                return (System.Web.HttpContext.Current == null) ? System.Web.HttpRuntime.Cache : System.Web.HttpContext.Current.Cache;
            }
        }

        public static object Get(string name)
        {
            cacheLock.EnterReadLock();
            try
            {
                return _cache[name];
            }
            catch (Exception e)
            {
                Logger.WriteError(e);
            }
            finally
            {
                cacheLock.ExitReadLock();
            }

            return null;
        }

        public static void Write(string name, object data, DateTime? absoluteExpiration = null, CacheItemRemovedCallback removing = null)
        {
            cacheLock.EnterWriteLock();
            try
            {
                if (_cache[name] != null)
                    _cache.Remove(name);

                if (absoluteExpiration == null)
                    absoluteExpiration = DateTime.UtcNow.AddMinutes(5);

                CacheItemRemovedCallback onRemove;
                if (removing == null)
                    onRemove = new CacheItemRemovedCallback(CIPCache.RemovedCallback);
                else
                    onRemove = removing;

                _cache.Add(name, data, null, (DateTime)absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, onRemove);
                Logger.DebugMessage("SynCache: Insert new item " + name + " to the cache");
            }
            catch (Exception e)
            {
                Logger.WriteError(e);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public static void Remove(string name)
        {
            cacheLock.EnterUpgradeableReadLock();
            try
            {
                _cache.Remove(name);
            }
            catch (Exception e)
            {
                Logger.WriteError(e);
            }
            finally
            {
                cacheLock.ExitUpgradeableReadLock();
            }
        }

        public static void RemovedCallback(String k, Object v, CacheItemRemovedReason r)
        {
            Logger.DebugMessage("SynCache: Remove item " + k + " from the cache" + r.ToString());
        }
    }

}
}