using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmniPot.Data;
using OmniPot.Data.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Newtonsoft.Json;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using OmniPot.Services;
using Microsoft.Extensions.Logging;

namespace OmniPot.Controllers
{
    [Produces("application/json")]
    [Route("api/Tenants")]
   // [Authorize(Roles = "SuperAdmin,TechSupport")] 
    public class TenantsController : BaseController
    {
        private KindDbContext _context;
                
        public TenantsController(KindDbContext context, TenantCacheService tenantCacheService, ILoggerFactory loggerFactory)
        : base(context, tenantCacheService, loggerFactory) {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Tenant> GetTenants()
        {
            return _context.Tenants.Include(e => e.Address).Include(e => e.Address.StateOrProvince).Where(e => e.State == TrackableEntityState.IsActive);
        }

        [HttpGet("GetList")]
        public DataSourceResult GetList([ModelBinder(BinderType = typeof(DataSourceRequestModelBinder))] DataSourceRequest request)
        {
            return _context.Tenants.Include(e => e.Address).Include(e => e.Address.StateOrProvince).Where(e => e.State == TrackableEntityState.IsActive).ToDataSourceResult(request);
        }

        [HttpGet("{id}", Name = "GetTenant")]
        public async Task<IActionResult> GetTenant([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tenant tenant = await _context.Tenants.Include(e => e.Address).SingleAsync(m => m.TenantId == id);

            if (tenant == null)
            {
                return NotFound();
            }

            return Ok(tenant);
        }
                
        [HttpGet("GetTenant2/{id}", Name = "GetTenant2")]
        public async Task<IActionResult> GetTenant2([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tenant tenant = await _context.Tenants.Include(e => e.Address).SingleAsync(m => m.TenantId == id);

            if (tenant == null)
            {
                return NotFound();
            }

            return Ok(tenant);
        }

        
        [HttpGet("IsRouteAvailable2/{routeName}", Name = "IsRouteAvailable2")]
        public async Task<IActionResult> IsRouteAvailable2(string routeName)
        {
            Tenant tenant = new Tenant();

            if (ModelState.IsValid)
            {
                try
                {
                    tenant = await _context.Tenants.Include(e => e.Address).SingleAsync(m => m.RouteName.ToUpper() == routeName.ToUpper());
                }
                catch (Exception ex)
                {
                }

            }

            if (tenant.DisplayName == null)
                return Ok("{\"isRouteAvailable\":\"TRUE\"}");
            else
                return Ok("{\"isRouteAvailable\":\"FALSE\"}");
        }

        [HttpPost("IsRouteAvailable/{routeName}", Name = "IsRouteAvailable")]
        public IActionResult IsRouteAvailable([FromBody]string routeName)
        {
            if (!_context.Tenants.Any(t => t.RouteName == routeName.ToLower()))
            {                
                return Ok(routeName.ToLower());
            }
            else
            {                
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
        }
                
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTenant([FromRoute] Guid id, [FromBody] Tenant tenant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tenant.TenantId)
            {
                return BadRequest();
            }

            _context.Entry(tenant).State = EntityState.Modified;
            _context.Entry(tenant.Address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(true);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TenantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new OkObjectResult(tenant); 
        }
                
        [HttpPost()]
        public async Task<IActionResult> PostTenant([FromBody] Tenant tenant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newAddress = new Address
            {
                AddressId = Guid.NewGuid(),
                Addressee = tenant.Address.Addressee,
                CityName = tenant.Address.CityName,
                DeliveryLine1 = tenant.Address.DeliveryLine1,
                DeliveryLine2 = tenant.Address.DeliveryLine2,
                PostalCode = tenant.Address.PostalCode,
                StateOrProvinceId = tenant.Address.StateOrProvinceId,
                State = TrackableEntityState.IsActive
            };

            var newTenant = new Tenant
            {
                TenantId = Guid.NewGuid(),
                DisplayName = tenant.DisplayName,
                RouteName = tenant.RouteName,
                State = TrackableEntityState.IsActive,
                AddressId = newAddress.AddressId
            };

            try
            {
                this._context.Addresses.Add(newAddress);
                this._context.Tenants.Add(newTenant);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (TenantExists(tenant.TenantId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {

                    throw;
                }
            }

            return CreatedAtRoute("GetTenant", new { id = newTenant.TenantId }, newTenant);
        }

        [HttpDelete("{id}")]        
        public async Task<IActionResult> DeleteTenant([FromRoute] Guid id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tenant tenant = await _context.Tenants.SingleAsync(m => m.TenantId == id);
            if (tenant == null)
            {
                return NotFound();
            }

            tenant.State = TrackableEntityState.IsDeleted; 
            _context.Entry(tenant).State = EntityState.Modified; 

            await _context.SaveChangesAsync();

            return Ok(tenant);
        }
        
        private bool TenantExists(Guid id)
        {
            return _context.Tenants.Count(e => e.TenantId == id) > 0;
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }


        ////================================================================================
        //[HttpPost("TenantCreateOld")]
        //public async Task<ActionResult> TenantCreateOld([DataSourceRequest] DataSourceRequest request, Tenant tenant) {
        //    logStart("TenantsController.TenantCreateOld");
        //    //logMember("TenantsController.TenantCreateOld", "tenant", tenant);

        //    try {

        //        if (!ModelState.IsValid) {
        //            logError("TenantsController.TenantCreateOld", new Exception("ModelState NOT Valid"));
        //            //return BadRequest(ModelState);  //ModelState.IsValid is always false!
        //        }

        //        Address newAddress;
        //        if (tenant.Address != null) { 
        //            logMember("TenantsController.TenantCreateOld", "tenant.Address.Addressee", tenant.Address.Addressee);
        //            newAddress = new Address {
        //                AddressId = Guid.NewGuid(),
        //                Addressee = tenant.Address.Addressee,
        //                CityName = tenant.Address.CityName,
        //                DeliveryLine1 = tenant.Address.DeliveryLine1,
        //                DeliveryLine2 = tenant.Address.DeliveryLine2,
        //                PostalCode = tenant.Address.PostalCode,
        //                StateOrProvinceId = tenant.Address.StateOrProvinceId,
        //                State = TrackableEntityState.IsActive
        //            };
        //        } else {  // we should be able to get rid of the else section, will do so after more testing..
        //            newAddress = new Address {
        //                AddressId = Guid.NewGuid(),
        //                Addressee = tenant.RouteName + " HC Addressee",
        //                CityName = tenant.RouteName + " HC CityName",
        //                DeliveryLine1 = tenant.RouteName + " HC DeliveryLine1",
        //                DeliveryLine2 = tenant.RouteName + " HC DeliveryLine2",
        //                PostalCode = "12345-7890",
        //                StateOrProvinceId = new Guid("3d9b654f-9a3d-4b8e-bb75-04ff0ce617af"),//thats WV// tenant.Address.StateOrProvinceId,
        //                State = TrackableEntityState.IsActive
        //            };

        //        }

        //        logMember("TenantsController.TenantCreateOld", "newAddress", newAddress);

        //        var newTenant = new Tenant {
        //            TenantId = Guid.NewGuid(),
        //            DisplayName = tenant.DisplayName,
        //            RouteName = tenant.RouteName,
        //            State = TrackableEntityState.IsActive,
        //            AddressId = newAddress.AddressId
        //        };
        //        logMember("TenantsController.TenantCreateOld", "newTenant", newTenant);

        //        this._context.Addresses.Add(newAddress);
        //        this._context.Tenants.Add(newTenant);

        //        await _context.SaveChangesAsync();

        //        logEnd("TenantsController.TenantCreateOld");

        //        return CreatedAtRoute("GetTenant", new { id = newTenant.TenantId }, newTenant);
        //    }
        //    catch (DbUpdateException ex) {
        //        logError("TenantsController.TenantCreateOld DbUpdateException", ex);
        //        logEnd("TenantsController.TenantCreateOld");
        //        if (TenantExists(tenant.TenantId)) {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        } else {
        //            throw;
        //        }
        //    }
        //    catch (Exception ex) {
        //        logError("TenantsController.TenantCreateOld", ex);
        //        throw;
        //    }
        //}

        //================================================================================
        [HttpPost("TenantCreate")]
        public async Task<ActionResult> TenantCreate([FromBody]Tenant tenant) {
            
            logStart("TenantsController.TenantCreate");
            logMember("TenantsController.TenantCreate", "tenant", tenant);
            if(tenant == null) {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }

            try {
                Address newAddress;
                if (tenant.Address != null) {
                    logMember("TenantsController.TenantCreate", "tenant.Address.Addressee", tenant.Address.Addressee);
                    newAddress = new Address {
                        AddressId = Guid.NewGuid(),
                        Addressee = tenant.Address.Addressee,
                        CityName = tenant.Address.CityName,
                        DeliveryLine1 = tenant.Address.DeliveryLine1,
                        DeliveryLine2 = tenant.Address.DeliveryLine2,
                        PostalCode = tenant.Address.PostalCode,
                        StateOrProvinceId = tenant.Address.StateOrProvinceId,
                        State = TrackableEntityState.IsActive
                    };
                } else {  //shouldn't need the else clause, will remove after more testing
                    newAddress = new Address {
                        AddressId = Guid.NewGuid(),
                        Addressee = tenant.RouteName + " HC Addressee",
                        CityName = tenant.RouteName + " HC CityName",
                        DeliveryLine1 = tenant.RouteName + " HC DeliveryLine1",
                        DeliveryLine2 = tenant.RouteName + " HC DeliveryLine2",
                        PostalCode = "12345-7890",
                        StateOrProvinceId = new Guid("3d9b654f-9a3d-4b8e-bb75-04ff0ce617af"),//thats WV// tenant.Address.StateOrProvinceId,
                        State = TrackableEntityState.IsActive
                    };

                }

                logMember("TenantsController.TenantCreate", "newAddress", newAddress);

                var newTenant = new Tenant {
                    TenantId = Guid.NewGuid(),
                    DisplayName = tenant.DisplayName,
                    RouteName = tenant.RouteName,
                    State = TrackableEntityState.IsActive,
                    AddressId = newAddress.AddressId
                };
                logMember("TenantsController.TenantCreate", "newTenant", newTenant);

                _context.Addresses.Add(newAddress);
                this._context.Tenants.Add(newTenant);

                await _context.SaveChangesAsync();

                logEnd("TenantsController.TenantCreate");

                return CreatedAtRoute("GetTenant", new { id = newTenant.TenantId }, newTenant);
            }
            catch (DbUpdateException ex) {
                logError("TenantsController.TenantCreate DbUpdateException", ex);
                logEnd("TenantsController.TenantCreate");
                if (TenantExists(tenant.TenantId)) {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                } else {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex) {
                logError("TenantsController.TenantCreate", ex);
                logEnd("TenantsController.TenantCreate");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        //================================================================================
        [HttpGet("TenantRead")]
        public DataSourceResult TenantRead([ModelBinder(BinderType = typeof(DataSourceRequestModelBinder))] DataSourceRequest request) {
            try {
                logStart("TenantsController.TenantRead");

                var result = _context.Tenants
                    .Include(e => e.Address)
                    .Include(e => e.Address.StateOrProvince)
                    .Where(e => e.State == TrackableEntityState.IsActive)
                    //.OrderBy(e => e.DisplayName)  //can't apply sorts here, they get added dynamically by the kendo grid
                    .ToDataSourceResult(request);

                //logMember("TenantsController.TenantUpdate", "result", result);

                logEnd("TenantsController.TenantRead");

                return result;
            }
            catch (Exception ex) {
                logError("TenantsController.TenantRead", ex);
                logEnd("TenantsController.TenantRead");
                throw;
            }
        }

        //================================================================================
        [HttpGet("TenantReadDeleted")]
        public DataSourceResult TenantReadDeleted([ModelBinder(BinderType = typeof(DataSourceRequestModelBinder))] DataSourceRequest request) {
            try {
                logStart("TenantsController.TenantReadDeleted");
                //logMember("TenantsController.TenantRead", "request", request);

                var result = _context.Tenants
                    .Include(e => e.Address)
                    .Include(e => e.Address.StateOrProvince)
                    //.OrderBy(e => e.DisplayName)  //can't apply sorts here, they get added dynamically by the kendo grid
                    .ToDataSourceResult(request);

                //logMember("TenantsController.TenantReadDeleted", "result", result);

                logEnd("TenantsController.TenantReadDeleted");

                return result;
            }
            catch (Exception ex) {
                logError("TenantsController.TenantReadDeleted", ex);
                logEnd("TenantsController.TenantReadDeleted");
                throw;
            }
        }

        //=======================================================================
        [HttpPut("TenantUpdate")]
        public async Task<ActionResult> TenantUpdate([FromBody]Tenant tenant) {
            logStart("TenantsController.TenantUpdate");
            logMember("TenantsController.TenantUpdate", "tenant", tenant);

            try {
                if (tenant == null || !ModelState.IsValid) {
                    logError("TenantsController.UpdateTenant", new Exception("Tenant Not Found"));
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                if (!TenantExists(tenant.TenantId)) {
                    return new StatusCodeResult(StatusCodes.Status404NotFound);
                }

                _context.Entry(tenant).State = EntityState.Modified;
                _context.Entry(tenant.Address).State = EntityState.Modified;

                await _context.SaveChangesAsync(true);

                logEnd("TenantsController.UpdateTenant");

                return Ok(tenant);
            }
            catch (Exception ex) {
                logError("TenantsController.UpdateTenant", ex);
                logEnd("TenantsController.UpdateTenant");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        //=======================================================================
        [HttpDelete("TenantDelete")]
        public async Task<ActionResult> TenantDelete([FromBody]Tenant tenant) {
            logStart("TenantsController.TenantDelete");

            try {
                logMember("TenantsController.TenantDelete", "tenant", tenant);

                if (tenant != null) {
                    tenant.State = TrackableEntityState.IsDeleted;
                    _context.Entry(tenant).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                } else {
                    logError("TenantDelete", new Exception("Tenant Not Found"));
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                logEnd("TenantsController.TenantDelete");

                //return Json(new[] { tenant }.ToDataSourceResult(request, ModelState));
                return Ok(tenant);
            }
            catch (Exception ex) {
                logError("TenantsController.TenantDelete", ex);
                logEnd("TenantsController.TenantDelete");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        //=======================================================================
        [HttpPost("TenantUndelete")]
        public async Task<ActionResult> TenantUndelete([FromBody]Tenant tenant) {
            logStart("TenantsController.TenantUndelete");

            try {
                logMember("TenantsController.TenantUndelete", "tenant", tenant);

                if (tenant != null) {
                    //_context.Destroy(tenant);

                    tenant.State = TrackableEntityState.IsActive;
                    _context.Entry(tenant).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                } else {
                    logError("TenantsController.TenantUndelete", new Exception("Tenant Not Found"));
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                logEnd("TenantsController.TenantUndelete");

                //return Json(new[] { tenant }.ToDataSourceResult( (request, ModelState));
                return Ok(tenant);
            }
            catch (Exception ex) {
                logError("TenantsController.TenantUndelete", ex);
                logEnd("TenantsController.TenantUndelete");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        //=======================================================================
        //!!!!  TODO add in appropriate user/role security for this action ( ONLY Super Admin? )
        [HttpPost("TenantHardDelete")]
        [HttpDelete("TenantHardDelete")]
        public async Task<ActionResult> TenantHardDelete([FromBody]Tenant tenant) {

            logStart("TenantsController.TenantHardDelete");

            try {
                logMember("TenantsController.TenantHardDelete", "tenant", tenant);
                string tenantName = tenant.DisplayName;

                if (tenant != null) {
                    _context.Remove(tenant.Address);
                    _context.Remove(tenant);
                    await _context.SaveChangesAsync();
                    logMessage("TenantsController.TenantHardDelete", "Tenant " + tenantName + " was permanently deleted.");
                } else {
                    logError("TenantHardDelete", new Exception("Tenant Not Found"));
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                logEnd("TenantsController.TenantHardDelete");

                return Ok(tenant);
            }
            catch (Exception ex) {
                logError("TenantsController.TenantHardDelete", ex);
                logEnd("TenantsController.TenantHardDelete");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                //throw;
            }
        }


    }
}

