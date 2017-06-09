using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmniPot.Data;
using OmniPot.Data.Models;
using System;
using OmniPot.Services;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace OmniPot.Controllers
{
    [Produces("application/json")]
    [Route("api/{tenant}/{location}/TaxGroups")]
    public class TaxGroupsController : BaseController
    {
 

        public TaxGroupsController(KindDbContext context, TenantCacheService tenantCacheService, ILoggerFactory loggerFactory)
            :base(context, tenantCacheService, loggerFactory)
        {
        }

        [HttpGet]
        public IEnumerable<TaxGroup> GetTaxGroups([FromRoute]string tenant, [FromRoute]string location)
        {
            var locationId = tenantCacheService.GetLocationId(tenant, location); 
            return context.TaxGroups.Include(e => e.TaxGroupItems).Where(e => e.State == TrackableEntityState.IsActive && e.LocationId == locationId);
        }

        
        [HttpGet("{id}", Name = "GetTaxGroup")]
        public async Task<IActionResult> GetTaxGroup([FromRoute]string tenant, [FromRoute]string location, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationId = tenantCacheService.GetLocationId(tenant, location); 
            TaxGroup taxGroup = await context.TaxGroups.Include(e => e.TaxGroupItems).SingleAsync(m => m.TaxGroupId == id && m.LocationId == locationId);

            if (taxGroup == null)
            {
                return NotFound();
            }

            return Ok(taxGroup);
        }
       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaxGroup([FromRoute]string tenant, [FromRoute]string location, [FromRoute] Guid id, [FromBody] TaxGroup taxGroup)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taxGroup.TaxGroupId)
            {
                return BadRequest();
            }

            var locationId = tenantCacheService.GetLocationId(tenant, location);
            taxGroup.LocationId = locationId; 
            context.Entry(taxGroup).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exc)
            {
                logger.LogError("TaxGroup update failed.", exc);
                if (!TaxGroupExists(id))
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
        public async Task<IActionResult> PostTaxGroup([FromRoute]string tenant, [FromRoute]string location, [FromBody] TaxGroup taxGroup)
        {

            logger.LogDebug("Entering PostTaxGroup(tenant:{0}, location:{1}, taxGroup{2})", tenant, location, taxGroup);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var locationId = tenantCacheService.GetLocationId(tenant, location);
            taxGroup.LocationId = locationId; 
            context.TaxGroups.Add(taxGroup);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException exc)
            {               
                if (TaxGroupExists(taxGroup.TaxGroupId))
                {
                    logger.LogWarning("TaxGroupExists", exc);
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    logger.LogError("Failed to add TaxGroup", exc);
                    throw;
                }
            }

            return CreatedAtRoute("GetTaxGroup", new { id = taxGroup.TaxGroupId }, taxGroup);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaxGroup([FromRoute]string tenant, [FromRoute]string location, [FromRoute] Guid id)
        {
            logger.LogDebug("Entering DeleteTaxGroup(tenant:{0}, location:{1}, id:{2})", tenant, location, id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationId = tenantCacheService.GetLocationId(tenant, location);
            TaxGroup taxGroup = await context.TaxGroups.SingleAsync(m => m.TaxGroupId == id && m.LocationId == locationId);
            if (taxGroup == null)
            {
                return NotFound();
            }

            try
            {
                taxGroup.State = TrackableEntityState.IsDeleted;
                context.Entry(taxGroup).State = EntityState.Modified; 
                await context.SaveChangesAsync();
                return Ok(taxGroup);
            }
            catch (Exception exc)
            {
                logger.LogError("Failed to delete/disable TaxGroup", exc);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        
        private bool TaxGroupExists(Guid id)
        {
            return context.TaxGroups.Count(e => e.TaxGroupId == id) > 0;
        }
    }
}