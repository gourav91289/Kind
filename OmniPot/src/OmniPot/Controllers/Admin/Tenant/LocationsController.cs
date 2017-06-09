using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmniPot.Data;
using OmniPot.Data.Models;
using OmniPot.Services;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace OmniPot.Controllers
{
    [Produces("application/json")]
    [Route("api/{tenant}/Locations")]
    [Authorize(Roles = "SuperAdmin")]
    public class LocationsController : BaseController
    {

        public LocationsController(KindDbContext context, TenantCacheService tenantCacheService, ILoggerFactory loggerFactory)
            :base(context, tenantCacheService, loggerFactory)
        {
        }

        [HttpGet]
        public IEnumerable<Location> GetLocations([FromRoute]string tenant)
        {
            var tenantId = tenantCacheService.GetId(tenant);
            return context.Locations.Include(e => e.Address).Where(l => l.TenantId == tenantId && l.State == TrackableEntityState.IsActive);
        }

        [HttpGet("GetPaged/{page}/{pageSize}")]
        public IEnumerable<Location> GetPaged([FromRoute]string tenant, [FromRoute]int page = 0, [FromRoute]int pageSize = 20)
        {
            var tenantId = tenantCacheService.GetId(tenant);
            return context.Locations
                .Include(e => e.Address)
                .Where(l => l.TenantId == tenantId && l.State == TrackableEntityState.IsActive)
                .Skip(page * pageSize)
                .Take(pageSize); 
        }
                
        [HttpGet("{id}", Name = "GetLocation")]
        public async Task<IActionResult> GetLocation([FromRoute]string tenant, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenantId = tenantCacheService.GetId(tenant);
            Location location = await context.Locations.Include(e => e.Address).SingleAsync(m => m.LocationId == id && m.TenantId == tenantId);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }
                
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation([FromRoute]string tenant, [FromRoute] Guid id, [FromBody] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.LocationId)
            {
                return BadRequest();
            }

            var tenantId = tenantCacheService.GetId(tenant);

            location.TenantId = tenantId;
            context.Entry(location).State = EntityState.Modified;            
            context.Entry(location.Address).State = EntityState.Modified;

            try
            {
                var resultCount = await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
                
        [HttpPost]
        public async Task<IActionResult> PostLocation([FromRoute]string tenant, [FromBody] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenantId = tenantCacheService.GetId(tenant);
            location.TenantId = tenantId;
            context.Locations.Add(location);
            context.Addresses.Add(location.Address);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LocationExists(location.LocationId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetLocation", new { id = location.LocationId }, location);
        }
                
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation([FromRoute]string tenant, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenantId = tenantCacheService.GetId(tenant); 
            Location location = await context.Locations.SingleAsync(m => m.LocationId == id && m.TenantId == tenantId);

            if (location == null)
            {
                return NotFound();
            }

            location.State = TrackableEntityState.IsDeleted;
            context.Entry(location).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return Ok(location);
        }
        
        private bool LocationExists(Guid id)
        {
            return context.Locations.Count(e => e.LocationId == id) > 0;
        }
    }
}