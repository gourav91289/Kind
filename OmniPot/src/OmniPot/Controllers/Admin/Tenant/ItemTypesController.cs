using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmniPot.Data;
using OmniPot.Data.Models;
using Microsoft.Extensions.Logging;
using OmniPot.Services;
using System;
using Microsoft.AspNetCore.Authorization;

namespace OmniPot.Controllers
{
    [Produces("application/json")]
    [Route("api/{tenant}/ItemTypes")]
    //TODO: Determine roles.
    [Authorize]
    public class ItemTypesController : BaseController
    {
        public ItemTypesController(KindDbContext context, TenantCacheService tenantCacheService, ILoggerFactory loggerFactory) : base(context, tenantCacheService, loggerFactory)
        {
            
        }

        // GET: api/ItemTypes
        [HttpGet]
        public IEnumerable<ItemType> GetItemTypes([FromRoute]string tenant)
        {
            //need to return both global (null tenant) and types specific to this tenant.
            var tenantId = tenantCacheService.GetId(tenant);

            return context.ItemTypes
                .Include(m => m.Children)
                .ThenInclude(m => m.Children)
                .Where(m => m.State == TrackableEntityState.IsActive && m.ParentItemTypeId == null && (m.TenantId == null || m.TenantId == tenantId));
        }

        // GET: api/ItemTypes/5
        [HttpGet("{id}", Name = "GetItemType")]
        public async Task<IActionResult> GetItemType([FromRoute]string tenant, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenantId = tenantCacheService.GetId(tenant);

            ItemType itemType = await context.ItemTypes.Include(m => m.Children).ThenInclude(m => m.Children).SingleAsync(m => m.ItemTypeId == id);

            if (itemType == null)
            {
                return NotFound();
            }

            return Ok(itemType);
        }

        // PUT: api/ItemTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemType([FromRoute]string tenant, [FromRoute] Guid id, [FromBody] ItemType itemType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemType.ItemTypeId)
            {
                return BadRequest();
            }

            var tenantId = tenantCacheService.GetId(tenant);
            itemType.TenantId = tenantId;
            context.Entry(itemType).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemTypeExists(id))
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

        // POST: api/ItemTypes
        [HttpPost]
        public async Task<IActionResult> PostItemType([FromRoute]string tenant, [FromBody] ItemType itemType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenantId = tenantCacheService.GetId(tenant);
            itemType.TenantId = tenantId;

            context.ItemTypes.Add(itemType);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemTypeExists(itemType.ItemTypeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetItemType", new { id = itemType.ItemTypeId }, itemType);
        }

        // DELETE: api/ItemTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemType([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ItemType itemType = await context.ItemTypes.SingleAsync(m => m.ItemTypeId == id);
            
            if (itemType == null)
            {
                return NotFound();
            }

            itemType.State = TrackableEntityState.IsDeleted;
            context.Entry(itemType).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return Ok(itemType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemTypeExists(Guid id)
        {
            return context.ItemTypes.Count(e => e.ItemTypeId == id) > 0;
        }
    }
}