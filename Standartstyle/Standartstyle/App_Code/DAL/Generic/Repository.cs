using com.omegasoftware.DBLib;
using Standartstyle.App_Code.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace Standartstyle.App_Code.DAL.Generic
{
    public enum KeySelectorBy
    {
        SORTBY_NONE,
        SORTBY_ALLDATA,
        SORTBY_CODE,
        SORTBY_PARENTCODE
    };

    public class CachingData
    {
        public bool UseCache { get; set; }
        public int? Size { get; set; }
        public bool AllIn { get; set; }

        public object Data { get; set; }
        public KeySelectorBy LastSelector { get; set; }
    }

    public abstract class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected List<T> data;
        protected CachingData cachingData;
        protected static string messageForLogger = "";

        private KeySelectorBy _lastSelector = KeySelectorBy.SORTBY_NONE;
        public static ReaderWriterLockSlim locker = new ReaderWriterLockSlim();

        protected T BinarySearch<TKey>(Func<T, TKey> keySelector, KeySelectorBy typeSelector, TKey key)
           where TKey : IComparable<TKey>
        {
            int index;
            return BinarySearch<TKey>(keySelector, typeSelector, key, out index);
        }

        protected List<T> GetElementsByBinarySearch<TKey>(Func<T, TKey> keySelector, KeySelectorBy typeSelector, TKey key)
            where TKey : IComparable<TKey>
        {
            int index;
            BinarySearch<TKey>(keySelector, typeSelector, key, out index);

            var allElements = new List<T>();

            var currentIndex = index;
            if (currentIndex >= 0)
            {
                while (data.Count > currentIndex && keySelector(data[currentIndex]).CompareTo(key) == 0)
                {
                    allElements.Add(data[currentIndex]);
                    currentIndex++;
                }
                currentIndex = index - 1;
                while (currentIndex >= 0 && keySelector(data[currentIndex]).CompareTo(key) == 0)
                {
                    allElements.Add(data[currentIndex]);
                    currentIndex--;
                }
            }
            return allElements;
        }

        protected void orderBy<TKey>(Func<T, TKey> keySelector, KeySelectorBy typeSelector)
            where TKey : IComparable<TKey>
        {
            if (_lastSelector != typeSelector)
            {
                data = data.OrderBy(keySelector).ToList();
                _lastSelector = typeSelector;
            }
        }

        protected T BinarySearch<TKey>(Func<T, TKey> keySelector, KeySelectorBy typeSelector, TKey key, out int index)
           where TKey : IComparable<TKey>
        {
            lock (this)
            {
                orderBy(keySelector, typeSelector);

                int min = 0;
                int max = data.Count - 1;
                if (max != 0) while (min <= max)
                    {
                        int mid = (max + min) / 2;
                        T midItem = data[mid];
                        TKey midKey = keySelector(midItem);
                        int comp = midKey.CompareTo(key);
                        if (comp < 0)
                        {
                            min = mid + 1;
                        }
                        else if (comp > 0)
                        {
                            max = mid - 1;
                        }
                        else
                        {
                            index = mid;
                            return midItem;
                        }
                    }
                if (min == max
                    && data.Count > min
                    && keySelector(data[min]).CompareTo(key) == 0)
                {
                    index = min;
                    return data[min];
                }

                if (cachingData.UseCache && !cachingData.AllIn)
                {
                    ObtainData();
                    return BinarySearch(keySelector, typeSelector, key, out index);
                }
                index = -1 - min;
                return null;
            }
        }
        protected virtual void SetData(CachingData cachedData)
        {
            if (cachedData.Data is List<T>)
            {
                data = new List<T>(cachedData.Data as List<T>);
                _lastSelector = cachedData.LastSelector;
            }
            else
                data = new List<T>();
        }

        protected virtual void loadFromDatabase()
        {
            data = new List<T>();
            cachingData = new CachingData();
            ClearSortFlag();
            ObtainData();
        }

        public void ClearSortFlag()
        {
            _lastSelector = KeySelectorBy.SORTBY_NONE;
        }

        protected virtual void Init(bool useCache, int? cacheSize, bool clearCache = false)
        {

            locker.EnterUpgradeableReadLock();
            try
            {
                if (useCache)
                {
                    if (clearCache)
                    {
                        ClearCache();
                        if (data != null)
                            data.Clear();
                    }
                    if (useCache)
                    {
                        locker.EnterReadLock();
                        try
                        {
                            cachingData = GetFromChache();
                        }
                        finally
                        {
                            locker.ExitReadLock();
                        }
                    }
                    if (cachingData == null)
                    {
                        locker.EnterWriteLock();
                        try
                        {
                            loadFromDatabase();

                            if (useCache)
                            {
                                this.GetByCode(0);
                                this.cachingData.Size = cacheSize;
                                this.cachingData.UseCache = useCache;
                                this.cachingData.LastSelector = _lastSelector;
                                this.WriteInCache();
                            }
                        }
                        finally
                        {
                            locker.ExitWriteLock();
                        }
                    }
                    else
                        SetData(cachingData);
                }
                else
                {
                    locker.EnterWriteLock();
                    try
                    {
                        loadFromDatabase();
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
            finally
            {
                locker.ExitUpgradeableReadLock();
            }
        }

        public Repository() { }

        public Repository(bool useCache, int? cacheSize)
        {
            Init(useCache, cacheSize);
        }

        protected virtual void AddData(T d)
        {
            data.Add(d);
        }

        protected void RemoveData(T d)
        {
            data.Remove(d);
        }

        protected virtual CachingData GetFromChache()
        {
            try
            {
                return (StyleCache.Get(this.CacheKey) as CachingData);
            }
            catch (Exception e)
            {
                Logger.WriteError(e);
            }
            return null;
        }

        protected virtual void WriteInCache()
        {
            cachingData.UseCache = true;
            try
            {
                if (cachingData.Size != null && data.Count > cachingData.Size)
                {
                    List<T> dumpData = new List<T>();
                    dumpData.AddRange(data);
                    dumpData.RemoveRange((int)cachingData.Size, data.Count - (int)cachingData.Size);
                    cachingData.AllIn = false;
                    cachingData.Data = dumpData;
                }
                else
                {
                    cachingData.AllIn = true;
                    cachingData.Data = new List<T>(data);
                }
                StyleCache.Write(this.CacheKey, cachingData);
            }
            catch (Exception e)
            {
                Logger.WriteError(e);
            }
        }

        protected virtual void ClearCache()
        {
            locker.EnterWriteLock();
            try
            {
                StyleCache.Remove(this.CacheKey);
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }

        public virtual void Refresh()
        {
            bool useCache = this.cachingData != null ? this.cachingData.UseCache : false;
            int? casheSize = this.cachingData != null ? this.cachingData.Size : null;
            Init(useCache, casheSize, true);
        }

        protected static string GenerateInStatement<M>(List<M> codes, string columnName)
        {
            StringBuilder sb = new StringBuilder();

            int skip = 0;

            string sep = ",";
            string insep = ") or " + columnName + " in (";

            int skipsep = sep.Length, skipinsep = insep.Length;

            if (codes.Count == 0)
                return "0"; // return safe value for SQL IN() statement

            for (int i = 0; i < codes.Count; i++)
            {
                sb.Append(codes[i]);

                if ((i + 1) % 100 == 0)
                {
                    sb.Append(insep);
                    skip = skipinsep;
                }
                else
                {
                    sb.Append(sep);
                    skip = skipsep;
                }
            }
            return sb.ToString(0, sb.Length - skip);
        }


        public void BulkDataInsert(IEnumerable<T> data, int batchSize = 200, UserData user = null)
        {
            try
            {
                using (DbConnection conn = Connection.GetConnection(Constants.MAIN_CONNECTION))
                {
                    conn.BeginTransaction();

                    if (data.Count() != 0)
                    {
                        DbCommand cmd = conn.CreateCommand("");
                        var table = GetDataTable(data, user);
                        cmd.BulkInsert(table, batchSize);
                    }
                    conn.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        protected virtual DataTable GetDataTable(IEnumerable<T> data, UserData user)
        {
            throw new ApplicationException("It's necessary to override GetDataTable() before using bulk");
        }

        #region IRepository members
        public IEnumerable<T> All
        {
            get { return data; }
        }

        public List<T> List
        {
            get { return data; }
        }

        public T GetByCode(int code)
        {
            return BinarySearch(instance => instance.Code, KeySelectorBy.SORTBY_CODE, code);
        }
        #endregion

        #region Abstract Members
        protected abstract void ObtainData();
        protected abstract string CacheKey { get; }
        public object SynCache { get; private set; }
        #endregion
    }
}
