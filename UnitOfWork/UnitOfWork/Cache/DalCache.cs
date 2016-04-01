using System;
using System.Runtime.Caching;
using UnitOfWork.Cache.Enum;

namespace UnitOfWork.Cache
{
    public class DalCache
    {
        private static readonly ObjectCache Cache = MemoryCache.Default;
        private CacheEntryRemovedCallback _callback;
        private CacheItemPolicy _policy;

        public void AddToMyCache(string cacheKeyName, object cacheItem, DalCachePriority myCacheItemPriority)
        {
            // 
            _callback = MyCachedItemRemovedCallback;
            _policy = new CacheItemPolicy
            {
                Priority = myCacheItemPriority == DalCachePriority.Default
                    ? CacheItemPriority.Default
                    : CacheItemPriority.NotRemovable,
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10.00),
                RemovedCallback = _callback
            };
            Cache.Set(cacheKeyName, cacheItem, _policy);
        }

        public object GetMyCachedItem(string cacheKeyName)
        {
            // 
            return Cache[cacheKeyName];
        }

        public void RemoveMyCachedItem(string cacheKeyName)
        {
            // 
            if (Cache.Contains(cacheKeyName))
            {
                Cache.Remove(cacheKeyName);
            }
        }

        private static void MyCachedItemRemovedCallback(CacheEntryRemovedArguments arguments)
        {
            // Log these values from arguments list 
            //var strLog = string.Concat("Reason: ", arguments.RemovedReason.ToString(), " | Key-Name: ",
            //    arguments.CacheItem.Key, " | Value-Object: ", arguments.CacheItem.Value.ToString());
        }
    }
}