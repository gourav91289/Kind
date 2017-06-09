#define PG_DEBUG //Comment out or remove to not compile these sections of code  - must be at top of each file you want to use it, else "#if PG_DEBUG" will always be false
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
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace OmniPot.Controllers
{
    [Produces("application/json")]
    //[Route("api/{tenant}/{location}/TaxItems")]
    //[A/uthorize]
    public class TaxItemsController : BaseController
    {

        //-----------------------------------------------------------------------------------------------------------------
        public TaxItemsController(KindDbContext context, TenantCacheService tenantCacheService, ILoggerFactory loggerFactory)
            :base(context, tenantCacheService, loggerFactory)
        {
        }

        //-----------------------------------------------------------------------------------------------------------------
        [Route("api/{tenant}/{location}/TaxItems")]
        [HttpGet]
        [HttpGet("api/{tenant}/{location}/GetTaxItems", Name = "GetTaxItems")]
        // GET: api/TaxItems
        public IEnumerable<TaxItem> GetTaxItems([FromRoute]string tenant, [FromRoute]string location)
        {
            var tenantId = tenantCacheService.GetId(tenant);
            return context.TaxItems.Where(e => e.TenantId == tenantId);
        }

        //-----------------------------------------------------------------------------------------------------------------
        //TODO: Some way of maintaining active/inactive entities needs to be taken into account. 
        // GET: api/StatesOrProvinces
        [HttpGet("api/{tenant}/{location}/TaxItems/GetTaxItemsForTaxGroup", Name = "GetTaxItemsForTaxGroup")]
        //public IEnumerable<TaxItem> GetTaxItemsForTaxGroup([FromRoute]string tenant, [FromRoute]string location, string taxGroupId)
        public IEnumerable<object> GetTaxItemsForTaxGroup([FromRoute]string tenant, [FromRoute]string location, string taxGroupId)
        {
#if PG_DEBUG
            Debug.WriteLine("\n\n==============TaxItemsController.GetTaxItemsForTaxGroup() STARTED === Time {0}==============", DateTime.Now);
            Debug.WriteLine("*  Time {0} ============== tenant =>{1}<=", DateTime.Now, tenant);
            Debug.WriteLine("*  Time {0} ============== location =>{1}<=", DateTime.Now, location);
            Debug.WriteLine("*  Time {0} ============== taxGroupId =>{1}<=", DateTime.Now, taxGroupId);

            var tenantId = tenantCacheService.GetId(tenant);
            //return _context.TaxItems.Where(e => e.TenantId == tenantId);

            //object myObbj2 = _context.TaxItems.Where(e => e.TenantId == tenantId);
            //object myObbj = _context.TaxGroups.Include(ti => ti.TaxGroupItems).Where(e => e.Location.RouteName.ToLower() == location.ToLower());  //.Include(ti => ti.TaxGroupItems)

            //Debug.WriteLine("*  Time {0} ============== myObbj2 =>{1}<=", DateTime.Now, JsonConvert.SerializeObject(myObbj2));
            //return (IEnumerable<TaxItem>)myObbj;// _context.StatesOrProvinces;//.Include(e => e.Address).Include(e => e.Address.StateOrProvince);



            object myObbj = context.TaxGroupItems.Where(tgi => tgi.TaxGroupId == Guid.Parse(taxGroupId)).Include( ti => ti.TaxItem ).OrderBy( ti2 => ti2.TaxItem.DisplayName );
            //object myObbj = _context.TaxGroups.Include(ti => ti.TaxGroupItems).Where(e => e.Location.RouteName.ToLower() == location.ToLower());  //.Include(ti => ti.TaxGroupItems)
            return (IEnumerable<object>)myObbj;

            //Debug.WriteLine("*  Time {0} ============== myObbj =>{1}<=", DateTime.Now, JsonConvert.SerializeObject(myObbj));



            ////var myQuery = from taxGroups in _context.TaxGroups
            ////              join myLocation in _context.Locations on taxGroups.LocationId equals myLocation.LocationId
            ////              join taxGroupItems in _context.TaxGroupItems on taxGroups.TaxGroupId equals taxGroupItems.TaxGroupId// into tgItems
            ////              join taxItems in _context.TaxItems on taxGroupItems.TaxItemId equals taxItems.TaxItemId// into BAR.
            ////              where myLocation.RouteName.ToUpper() == location.ToUpper()

            ////              orderby taxGroups.DisplayName
            ////              select new { TaxGroups = taxGroups, Location = myLocation, TaxGroupItems = taxGroupItems, TaxItems = taxItems }//, StateProvinceCount = statesProvincesInCountry.Count() };
            ////              ;

            //////Debug.WriteLine("*  Time {0} ============== myQuery =>{1}<=", DateTime.Now, JsonConvert.SerializeObject(myQuery));
            ////return myQuery;


            //select TaxGroup.DisplayName
            //      ,TaxGroup.TaxGroupId
            //      ,TaxGroupItem.*
            //      , TaxItem.*
            // from TaxGroup
            // inner join TaxGroupItem on TaxGroup.TaxGroupId = TaxGroupItem.TaxGroupId
            // inner join TaxItem on TaxGroupItem.TaxItemId = TaxItem.TaxItemId
            // order by TaxGroup.DisplayName            




            Debug.WriteLine("==============TaxItemsController.GetTaxItemsForTaxGroup() E N D S === Time {0}==============\n\n", DateTime.Now);

#endif
            //return _context.TaxItems.Where(e => e.TenantId == tenantId);

        }

        
        [HttpGet("{id}", Name = "GetTaxItem")]
        public async Task<IActionResult> GetTaxItem([FromRoute]string tenant, [FromRoute]string location, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tenantId = tenantCacheService.GetId(tenant);
            TaxItem taxItem = await context.TaxItems.SingleAsync(m => m.TaxItemId == id && m.TenantId == tenantId);

            if (taxItem == null)
            {
                return NotFound();
            }

            return Ok(taxItem);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaxItem([FromRoute]string tenant, [FromRoute]string location, [FromRoute] Guid id, [FromBody] TaxItem taxItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taxItem.TaxItemId)
            {
                return BadRequest();
            }

            context.Entry(taxItem).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaxItemExists(id))
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
        public async Task<IActionResult> PostTaxItem([FromRoute]string tenant, [FromRoute]string location, [FromBody] TaxItem taxItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.TaxItems.Add(taxItem);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TaxItemExists(taxItem.TaxItemId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetTaxItem", new { id = taxItem.TaxItemId }, taxItem);
        }
        
        [HttpDelete("api/TaxItems/{id}")]
        public async Task<IActionResult> DeleteTaxItem([FromRoute] Guid id)
        {
#if PG_DEBUG
            Debug.WriteLine("\n\n\n  ============== TaxItemsController.DeleteTenant(id=>" + id + "<=) == RAN === Time {0} =============================\n\n\n", DateTime.Now);
#endif

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaxItem taxItem = await context.TaxItems.SingleAsync(m => m.TaxItemId == id);
            if (taxItem == null)
            {
                return NotFound();
            }

            taxItem.State = TrackableEntityState.IsDeleted; 
            context.Entry(taxItem).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok(taxItem);
        }
                
        private bool TaxItemExists(Guid id)
        {
            return context.TaxItems.Count(e => e.TaxItemId == id) > 0;
        }
    }
}