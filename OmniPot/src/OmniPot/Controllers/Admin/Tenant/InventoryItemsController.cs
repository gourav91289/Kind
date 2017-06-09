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
    [Route("api/{tenant}/InventoryItems")]
    //TODO: Determine roles. 
    [Authorize]
    public class InventoryItemsController : BaseController
    {

        public InventoryItemsController(KindDbContext context, TenantCacheService tenantCacheService, ILoggerFactory loggerFactory)
            :base(context, tenantCacheService, loggerFactory)
        {
        }

        // GET: api/InventoryItems
        [HttpGet]
        public IEnumerable<InventoryItem> GetInventoryItems([FromRoute]string tenant)
        {
            var tenantId = tenantCacheService.GetId(tenant);
            return context.InventoryItems.Where(e => e.TenantId == tenantId);
        }

        // GET: api/InventoryItems/5
        [HttpGet("{id}", Name = "GetInventoryItem")]
        public async Task<IActionResult> GetInventoryItem([FromRoute]string tenant, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenantId = tenantCacheService.GetId(tenant);
            InventoryItem inventoryItem = await context.InventoryItems.SingleAsync(m => m.InventoryItemId == id && m.TenantId == tenantId);

            if (inventoryItem == null)
            {
                return NotFound();
            }

            return Ok(inventoryItem);
        }

        // PUT: api/InventoryItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryItem([FromRoute]string tenant, [FromRoute] Guid id, [FromBody] InventoryItem inventoryItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inventoryItem.InventoryItemId)
            {
                return BadRequest();
            }

            var tenantId = tenantCacheService.GetId(tenant);
            inventoryItem.TenantId = tenantId; 
            context.Entry(inventoryItem).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryItemExists(id))
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

        // POST: api/InventoryItems
        [HttpPost]
        public async Task<IActionResult> PostInventoryItem([FromRoute]string tenant, [FromBody] InventoryItem inventoryItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenantId = tenantCacheService.GetId(tenant);
            inventoryItem.TenantId = tenantId; 
            context.InventoryItems.Add(inventoryItem);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InventoryItemExists(inventoryItem.InventoryItemId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetInventoryItem", new { id = inventoryItem.InventoryItemId }, inventoryItem);
        }

        // DELETE: api/InventoryItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryItem([FromRoute]string tenant, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenantId = tenantCacheService.GetId(tenant);
            InventoryItem inventoryItem = await context.InventoryItems.SingleAsync(m => m.InventoryItemId == id && m.TenantId == tenantId);

            if (inventoryItem == null)
            {
                return NotFound();
            }

            context.InventoryItems.Remove(inventoryItem);
            inventoryItem.State = TrackableEntityState.IsDeleted;
            context.Entry(inventoryItem).State = EntityState.Modified;
            
            await context.SaveChangesAsync();

            return Ok(inventoryItem);
        }        

        private bool InventoryItemExists(Guid id)
        {
            return context.InventoryItems.Count(e => e.InventoryItemId == id) > 0;
        }
    }
}