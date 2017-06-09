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
    [Route("api/{tenant}/{location}/Batches")]
    //[A/uthorize]
    public class BatchesController : BaseController
    {

        public BatchesController(KindDbContext context, TenantCacheService tenantCacheService, ILoggerFactory loggerFactory)
            :base(context, tenantCacheService, loggerFactory)
        {
         
        }

        // GET: api/Batches
        [HttpGet]
        public IEnumerable<Batch> GetBatches([FromRoute]string tenant, [FromRoute]string location)
        {
            logger.LogDebug("Entering GetBatches(tenant:{0}, location:{1})", tenant, location);
            var locationId = tenantCacheService.GetLocationId(tenant, location);

            return context.Batches.Where(e => e.LocationId == locationId && e.State == TrackableEntityState.IsActive);
        }


        //TODO: GetBatchByBarcode
        // GET: api/Batches/5
        [HttpGet("{id}", Name = "GetBatch")]
        public async Task<IActionResult> GetBatch([FromRoute]string tenant, [FromRoute]string location, [FromRoute] Guid id)
        {
            logger.LogDebug("Entering GetBatch(tenant:{0}, location:{1}, id:{2})", tenant, location, id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var locationId = tenantCacheService.GetLocationId(tenant, location);

            Batch batch = await context.Batches.SingleAsync(m => m.BatchId == id && m.LocationId == locationId);

            if (batch == null)
            {
                logger.LogWarning("Batch not found");
                return NotFound();
            }

            return Ok(batch);
        }

        // PUT: api/Batches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBatch([FromRoute]string tenant, string location, [FromRoute] Guid id, [FromBody] Batch batch)
        {
            logger.LogDebug("Entering PutBatch(tenant:{0}, location:{1}, id:{2})", tenant, location, id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != batch.BatchId)
            {
                logger.LogWarning("Batch id missmatch");
                return BadRequest();
            }

            var locationId = tenantCacheService.GetLocationId(tenant, location);
            batch.LocationId = locationId;

            context.Entry(batch).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!BatchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    logger.LogError("Error updating batch", ex);
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        // POST: api/Batches
        [HttpPost]
        public async Task<IActionResult> PostBatch([FromRoute]string tenant, [FromRoute]string location, [FromBody] Batch batch)
        {
            logger.LogDebug("Entering PostBatch(tenant:{0}, location{1})", tenant, location);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationId = tenantCacheService.GetLocationId(tenant, location);
            batch.LocationId = locationId;
            context.Batches.Add(batch);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (BatchExists(batch.BatchId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    logger.LogError("Error updating batch", ex);
                    throw;
                }
            }

            return CreatedAtRoute("GetBatch", new { tenant = tenant, location = location, id = batch.BatchId }, batch);
        }

        // DELETE: api/Batches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatch([FromRoute]string tenant, [FromRoute]string location, [FromRoute] Guid id)
        {
            logger.LogDebug("Entering DeleteBatch(tenant:{0}, location:{1}, id:{2})", tenant, location, id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var locationId = tenantCacheService.GetLocationId(tenant, location);
            Batch batch = await context.Batches.SingleAsync(m => m.BatchId == id && m.LocationId == locationId);
            if (batch == null)
            {
                logger.LogWarning("Batch not found");
                return NotFound();
            }

            batch.State = TrackableEntityState.IsDeleted;
            context.Entry(batch).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return Ok(batch);
        }
        
        private bool BatchExists(Guid id)
        {
            return context.Batches.Count(e => e.BatchId == id) > 0;
        }
    }
}