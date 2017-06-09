using Microsoft.Extensions.Caching.Memory;
using OmniPot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Services
{
    public class TenantCacheService
    {
        private readonly KindDbContext kindContext;
        private readonly IMemoryCache memoryCache;
        private const string tenantFormat = "CacheKey-{0}";
        private const string locationFormat = "CacheKey-{0}-{1}";
        public TenantCacheService(KindDbContext kindContext, IMemoryCache memoryCache)
        {
            this.kindContext = kindContext;
            this.memoryCache = memoryCache; 
        }

        public Guid GetId(string routeName)
        {
            var key = string.Format(tenantFormat, routeName.ToLower());
            Guid retVal = Guid.Empty; 

            if (!memoryCache.TryGetValue(key, out retVal))
            {
                retVal = kindContext.Tenants.Where(t => t.RouteName == routeName.ToLower()).DefaultIfEmpty(new Data.Models.Tenant { TenantId = Guid.Empty }).Single().TenantId;
                memoryCache.Set(key, retVal); 
            }

            return retVal; 
        }
        
        public Guid GetLocationId(string tenant, string location)
        {
            var tenantId = GetId(tenant);
            if (tenantId == Guid.Empty)
                return tenantId;
            var key = string.Format(locationFormat, tenant.ToLower(), location.ToLower());
            Guid retVal = Guid.Empty; 

            if (!memoryCache.TryGetValue(key, out retVal))
            {
                retVal = kindContext.Locations.Where(t => t.TenantId == tenantId && t.RouteName == location.ToLower()).DefaultIfEmpty(new Data.Models.Location { LocationId = Guid.Empty }).Single().LocationId;
                memoryCache.Set(key, retVal);
            }

            return retVal; 
        }

        /// <summary>
        /// Intended only to mock up route/id pairs or to reset a tenant cache object that may have been requested before it 
        /// was added to the datastore. 
        /// </summary>
        /// <param name="routeName">The tenant route name to store the id for</param>
        /// <param name="tenantId">The tenant id as it exists in the database</param>
        /// <remarks>This should always be internal. I've cached Guid.Empty for non-exisitent tenants so that broken api calls won't create a problem at the database.</remarks>
        internal void SetId(string routeName, Guid tenantId)
        {
            var key = string.Format(tenantFormat, routeName.ToLower());
            memoryCache.Set(key, tenantId);
        }

    }
}
