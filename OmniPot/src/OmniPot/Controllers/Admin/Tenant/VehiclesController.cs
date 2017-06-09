using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmniPot.Data;
using OmniPot.Data.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using OmniPot.Services;
using Microsoft.Extensions.Logging;

namespace OmniPot.Controllers
{
    [Produces("application/json")]
    [Route("api/{tenant}/Vehicles")]
    [Authorize(Roles = "SuperAdmin,TenantAdmin,TechSupport")]
    public class VehiclesController : BaseController
    {

        public VehiclesController(KindDbContext context, TenantCacheService tenantCacheService, ILoggerFactory loggerFactory)
            :base(context, tenantCacheService, loggerFactory)
        {
        }
             
        [HttpGet]
        public IEnumerable<Vehicle> GetVehicles([FromRoute] string tenant)
        {
            var tenantId = tenantCacheService.GetId(tenant);
            return context.Vehicles.Include(v => v.ServiceRecords).Include(v => v.Documents).Where(v => v.TenantId == tenantId && v.State == TrackableEntityState.IsActive);
        }
                
        [HttpGet("{id}", Name = "GetVehicle")]
        public async Task<IActionResult> GetVehicle([FromRoute]string tenant, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenantId = tenantCacheService.GetId(tenant); 
            //Edge case where they have a bogus tenant route but a real vehicleId
            Vehicle vehicle = await context.Vehicles.Include(v => v.ServiceRecords).Include(v => v.Documents).SingleAsync(m => m.VehicleId == id && m.TenantId == tenantId);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle([FromRoute]string tenant, [FromRoute] Guid id, [FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicle.VehicleId)
            {
                return BadRequest();
            }

            var tenantId = tenantCacheService.GetId(tenant);
            vehicle.TenantId = tenantId;
            context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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
        public async Task<IActionResult> PostVehicle([FromRoute]string tenant, [FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenantId = tenantCacheService.GetId(tenant);
            vehicle.TenantId = tenantId; 
            context.Vehicles.Add(vehicle);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VehicleExists(vehicle.VehicleId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetVehicle", new { id = vehicle.VehicleId }, vehicle);
        }
                
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute]string tenant, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenantId = tenantCacheService.GetId(tenant);       
            Vehicle vehicle = await context.Vehicles.SingleAsync(m => m.VehicleId == id && m.TenantId == tenantId);

            if (vehicle == null)
            {
                return NotFound();
            }

            vehicle.State = TrackableEntityState.IsDeleted;
            context.Entry(vehicle).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return Ok(vehicle);
        }
        
        private bool VehicleExists(Guid id)
        {
            return context.Vehicles.Count(e => e.VehicleId == id) > 0;
        }
    }
}