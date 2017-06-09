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
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace OmniPot.Controllers
{
    [Produces("application/json")]
    [Route("api/{tenant}/Vendors")]
    //TODO: Determine actual roles.
    [Authorize]
    public class VendorsController : BaseController
    {
        public VendorsController(KindDbContext context, TenantCacheService tenantCacheService, ILoggerFactory loggerFactory)
            :base(context, tenantCacheService, loggerFactory)
        {
        }
        
        [HttpGet]
        public IEnumerable<Vendor> GetVendors()
        {
            return context.Vendors.Where(e => e.State == TrackableEntityState.IsActive);
        }
                
        [HttpGet("{id}", Name = "GetVendor")]
        public async Task<IActionResult> GetVendor([FromRoute]string tenant, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenantId = tenantCacheService.GetId(tenant);
            Vendor vendor = await context.Vendors.SingleAsync(m => m.VendorId == id && m.TenantId == tenantId);

            if (vendor == null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }
                
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendor([FromRoute]string tenant, [FromRoute] Guid id, [FromBody] Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendor.VendorId)
            {
                return BadRequest();
            }

            var tenantId = tenantCacheService.GetId(tenant);
            vendor.TenantId = tenantId;
            context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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
        public async Task<IActionResult> PostVendor([FromRoute]string tenant, [FromBody] Vendor vendor)
        {
            System.Diagnostics.Debug.WriteLine("\n\n\n============ VendorsController.PostVendor() started.... ");
            System.Diagnostics.Debug.WriteLine("* tenant =>" + tenant + "<=");
            System.Diagnostics.Debug.WriteLine("* vendor =>" + vendor + "<=");
            System.Diagnostics.Debug.WriteLine("* vendor =>" + Newtonsoft.Json.JsonConvert.SerializeObject(vendor) + "<=");

            if (!ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("* ModelState is Invalid!\n\n");
                return BadRequest(ModelState);
            }

            var tenantId = tenantCacheService.GetId(tenant);
            System.Diagnostics.Debug.WriteLine("* tenantId =>{0}<=", tenantId);
            vendor.TenantId = tenantId;            
            context.Vendors.Add(vendor);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException exc)
            {
                System.Diagnostics.Debug.WriteLine("* DbUpdateException in VendorsController.PostVendor() =>" + Newtonsoft.Json.JsonConvert.SerializeObject(exc) + "<=\n\n");
                if (VendorExists(vendor.VendorId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            System.Diagnostics.Debug.WriteLine("*Created vendor? =>" + Newtonsoft.Json.JsonConvert.SerializeObject(vendor) + "<=\n\n");
            return CreatedAtRoute("GetVendor", new { id = vendor.VendorId }, vendor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor([FromRoute]string tenant, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenantId = tenantCacheService.GetId(tenant);

            Vendor vendor = await context.Vendors.SingleAsync(m => m.VendorId == id && m.TenantId == tenantId);
            if (vendor == null)
            {
                return NotFound();
            }

            vendor.State = TrackableEntityState.IsDeleted;
            context.Entry(vendor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok(vendor);
        }

        private bool VendorExists(Guid id)
        {
            return context.Vendors.Count(e => e.VendorId == id) > 0;
        }
    }
}