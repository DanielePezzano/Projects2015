using System;
using System.Runtime.Caching;
using AutUow.Cache.Enum;

namespace AutUow.Cache
{
    public class DalCache
    {
        private static readonly ObjectCache Cache = MemoryCache.Default;
        private CacheEntryRemovedCallback _callback;
        private CacheItemPolicy _policy;

        public void AddToMyCache(string cacheKeyName, object cacheItem, DalCachePriority myCacheItemPriority)
        {
            _callback = MyCachedItemRemovedCallback;
            _policy = new CacheItemPolicy();
            _policy.Priority = (myCacheItemPriority == DalCachePriority.Default)
                ? CacheItemPriority.Default
                : CacheItemPriority.NotRemovable;
            _policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10.00);
            _policy.RemovedCallback = _callback;
            Cache.Set(cacheKeyName, cacheItem, _policy);
        }

        public object GetMyCachedItem(string cacheKeyName)
        {
            return Cache[cacheKeyName];
        }

        public void RemoveMyCachedItem(string cacheKeyName)
        {
            if (Cache.Contains(cacheKeyName))
            {
                Cache.Remove(cacheKeyName);
            }
        }

        private void MyCachedItemRemovedCallback(CacheEntryRemovedArguments arguments)
        {
            // Log these values from arguments list 
            //var strLog = string.Concat("Reason: ", arguments.RemovedReason.ToString(), " | Key-Name: ",
            //    arguments.CacheItem.Key, " | Value-Object: ", arguments.CacheItem.Value.ToString());
        }
    }
}