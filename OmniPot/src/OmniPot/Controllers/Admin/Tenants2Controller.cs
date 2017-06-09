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
using System.Web.Http;
using System.Net;

namespace OmniPot.Controllers
{
    [Produces("application/json")]
    [Route("api/Tenants2")]
   // [Authorize(Roles = "SuperAdmin,TechSupport")] 
    public class Tenants2Controller : BaseController
    {
        private KindDbContext _context;

        //static List<Tenant> myTenants = new List<Tenant> ();

        //{
        //      new Tenant("{B68E7913-991F-48B7-B1AE-19F16B5045A7}","Apple","appleRoute")
        //    , new Tenant("{E29F7051-7694-4DBE-B1B3-A77DF473AA20}","Beans","beansRoute")
        //    , new Tenant("{4EB0CA3F-A83B-4F1A-A48A-D50F8AFE0FF2}","Cranberry","cBeryRoute")
        //    , new Tenant("{D31B5D24-CD3B-4937-9A05-48242ADFC10}","Doughnut","doughnutsRoute")
        //};

        //public Tenants2Controller(KindDbContext context)
        //{
        //    _context = context;
        //}
        public Tenants2Controller(KindDbContext context, TenantCacheService tenantCacheService, ILoggerFactory loggerFactory)
        :base(context, tenantCacheService, loggerFactory)
        {
            _context = context;

            logStart("Tenants2Controller(constructor)");

            //if (myTenants.Count == 0) {

            //    Address addressKind = new Address {
            //        AddressId = Guid.Parse("{08632C01-EA07-4B49-A85E-9B8545086D9F}"),
            //        Addressee = "Kind Financial",
            //        CityName = "Los Angeles",
            //        DeliveryLine1 = "1680 Vine Street",
            //        DeliveryLine2 = "Suite 760",
            //        PostalCode = "90028",
            //        StateOrProvinceId = Guid.Parse("4C40FF45-C47C-455C-B055-D398F93A2DA3"),
            //        State = TrackableEntityState.IsActive
            //    };


            //    Guid tenantKindTenantId = Guid.Parse("{35DCFFC5-E31D-4C60-AC41-31417A700D3B}");
            //    var tenantKind = new Tenant {
            //        TenantId = tenantKindTenantId,
            //        DisplayName = "Kind Financial",
            //        RouteName = "Kind",
            //        Address = addressKind,
            //        State = TrackableEntityState.IsActive
            //    };
            //    myTenants.Add(tenantKind);

            //    Address addressAgrisoft246 = new Address {
            //        AddressId = Guid.Parse("C5FEC1A4-F102-4AC8-B148-AB4704EF141B"),
            //        Addressee = "Brian @ Agrisoft",
            //        CityName = "Kansas City",
            //        DeliveryLine1 = "1201 NW Briarcliff Parkway",
            //        DeliveryLine2 = "Suite 246",
            //        PostalCode = "64116",
            //        StateOrProvinceId = Guid.Parse("1325029C-D222-4FAB-9F39-437BEC96D48C"),
            //        State = TrackableEntityState.IsActive
            //    };

            //    Guid tenantAgrisoftTenantId = Guid.Parse("{2CB99036-31E4-4D98-836F-C150033EF438}");
            //    var tenantAgrisoft = new Tenant {
            //        TenantId = tenantAgrisoftTenantId,
            //        DisplayName = "Kind's Agrisoft",
            //        RouteName = "Kind-Agrisoft",
            //        Address = addressAgrisoft246,
            //        State = TrackableEntityState.IsActive
            //    };
            //    myTenants.Add(tenantAgrisoft);

            //    Address addressWonderbuds = new Address {
            //        AddressId = Guid.Parse("F9433517-0FE3-42A2-B45F-BE1A7387273B"),
            //        Addressee = "Wonderbuds",
            //        CityName = "Denver",
            //        DeliveryLine1 = "6901 SE Central Ave",
            //        DeliveryLine2 = "Wonder Building",
            //        PostalCode = "83221",
            //        StateOrProvinceId = Guid.Parse("16949803-9820-46F7-A0F2-66887FC5FD1C"),
            //        State = TrackableEntityState.IsActive
            //    };

            //    Guid tenantWonderbudsTenantId = Guid.Parse("{3501FC24-B374-488B-8862-6DB95BD39F3D}");
            //    var tenantWonderbuds = new Tenant {
            //        TenantId = tenantWonderbudsTenantId,
            //        DisplayName = "Wonderbuds Inc.",
            //        RouteName = "Wonderbuds",
            //        Address = addressWonderbuds,
            //        State = TrackableEntityState.IsActive
            //    };
            //    myTenants.Add(tenantWonderbuds);

            //}

            logEnd("Tenants2Controller(constructor)");
        }


        //[HttpGet]// this method gets called with calls ".Read(read => read.Url("/api/Tenants").Type(HttpVerbs.Get))"
        //         //                                 OR ".Read(read => read.Url("/api/Tenants").Type(HttpVerbs.Get))"
        //public IEnumerable<Tenant> GetTenants2()
        //{
        //    logger.LogDebug("\n\n\n\n\n =============================\n=============================\n============================= " + DateTime.Now + " == "
        //        + "Tenants2Controller.GetTenants Started... ");
        //    var result = _context.Tenants
        //        .Include(e => e.Address)
        //        .Include(e => e.Address.StateOrProvince)
        //        .Where(e => e.State == TrackableEntityState.IsActive);

        //    logger.LogDebug("" + DateTime.Now + "===============================\n GetTenants completed... result=> {0} <= \n==========================================\n\n\n\n\n", Newtonsoft.Json.JsonConvert.SerializeObject(result));

        //    return result;

        //}

        //================================================================================
        [HttpGet("TenantRead2")]
        public DataSourceResult TenantRead2([ModelBinder(BinderType = typeof(DataSourceRequestModelBinder))] DataSourceRequest request)
        {
            try {
                logStart("Tenants2Controller.TenantRead2");
                //logger.LogDebug("" +DateTime.Now + "=============================\n TenantRead2(request:{0})", request);
                logMember("Tenants2Controller.TenantRead2", "request", request);
                
                var result = _context.Tenants
                    .Include(e => e.Address)
                    .Include(e => e.Address.StateOrProvince)
                    .Where(e => e.State == TrackableEntityState.IsActive).ToDataSourceResult(request);

                //logger.LogDebug("   * " + DateTime.Now + " * SerializeObject(result.Data)=>" + Newtonsoft.Json.JsonConvert.SerializeObject(result.Data) + "<==");

                logMember("Tenants2Controller.TenantRead2", "result", result);

                logEnd("Tenants2Controller.TenantRead2");

                return result;
            }
            catch (Exception ex) {
                logError("Tenants2Controller.TenantRead2", ex);
                logEnd("Tenants2Controller.TenantRead2");
                throw;
            }
        }

        ////[HttpGet("GetTenantsList2")]
        ////public ActionResult GetTenantsList2([ModelBinder(BinderType = typeof(DataSourceRequestModelBinder))] DataSourceRequest request) 
        //// {
        ////    logger.LogDebug("" +DateTime.Now + "\n\n\n =============================\n=============================\n============================= "
        ////        + "Tenants2Controller.GetTenantsList2 Started... ");
        ////    logger.LogDebug("" +DateTime.Now + "=============================\n GetTenantsList2(request:{0})", request);

        ////    var result = _context.Tenants
        ////        .Include(e => e.Address) 
        ////        .Include(e => e.Address.StateOrProvince)
        ////        .Where(e => e.State == TrackableEntityState.IsActive).ToDataSourceResult(request);

        ////    logger.LogDebug("" +DateTime.Now + "===============================\nGetList nGetList2... \n  result=>" + Newtonsoft.Json.JsonConvert.SerializeObject(result.Data) + "==========================================\n\n");


        ////    return Json(result);
        ////}

        //[HttpGet("GetTenantsList3")]  //if this annotation not here, will got to "[HttpGet]", if "[HttpGet]" is not defined it will try to match all unannotated 0 parameter methods
        //public ActionResult GetTenantsList3([ModelBinder(BinderType = typeof(DataSourceRequestModelBinder))] DataSourceRequest request) {
        //    logger.LogDebug("\n\n\n\n\n =============================\n=============================\n============================= " + DateTime.Now + " == "
        //        + "Tenants2Controller.GetTenantsList3 Started... ");
        //    logger.LogDebug("" + DateTime.Now + "=============================\n GetTenantsList3(request:{0})", request);

        //    var result = _context.Tenants
        //        .Include(e => e.Address)
        //        .Include(e => e.Address.StateOrProvince)
        //        .Where(e => e.State == TrackableEntityState.IsActive).ToDataSourceResult(request);

        //    logger.LogDebug("" + DateTime.Now + "===============================\nGetList nGetList3... \n  result=>" + Newtonsoft.Json.JsonConvert.SerializeObject(result.Data) + "==========================================\n\n");


        //    return Json(result);
        //}

        //[HttpGet("{id}", Name = "GetTenantById")]
        //public async Task<IActionResult> GetTenantById([FromRoute] Guid id)
        //{
        //    logger.LogDebug("" +DateTime.Now + "\n\n\n =============================\n=============================\n============================= "
        //        + "Tenants2Controller.GetTenantById Started... ");
        //    logger.LogDebug("" +DateTime.Now + "=============================\n GetTenantById(id:{0})", id);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Tenant tenant = await _context.Tenants.Include(e => e.Address).SingleAsync(m => m.TenantId == id);

        //    if (tenant == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tenant);
        //}

        //[HttpGet("GetTenantById2/{id}", Name = "GetTenantById2")]
        //public async Task<IActionResult> GetTenantById2([FromRoute] Guid id)
        //{
        //    logger.LogDebug("" +DateTime.Now + "\n\n\n =============================\n=============================\n============================= "
        //        + "Tenants2Controller.GetTenantById2 Started... ");
        //    logger.LogDebug("" +DateTime.Now + "=============================\n GetTenantById2(id:{0})", id);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Tenant tenant = await _context.Tenants.Include(e => e.Address).SingleAsync(m => m.TenantId == id);

        //    if (tenant == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tenant);
        //}


        //[HttpGet("IsRouteAvailableByRoute2/{routeName}", Name = "IsRouteAvailableByRoute2")]
        //public async Task<IActionResult> IsRouteAvailableByRoute2(string routeName)
        //{
        //    logger.LogDebug("" +DateTime.Now + "\n\n\n =============================\n=============================\n============================= "
        //        + "Tenants2Controller.IsRouteAvailableByRoute2 Started... ");

        //    Tenant tenant = new Tenant();

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            tenant = await _context.Tenants.Include(e => e.Address).SingleAsync(m => m.RouteName.ToUpper() == routeName.ToUpper());
        //        }
        //        catch (Exception ex)
        //        {
        //        }

        //    }

        //    if (tenant.DisplayName == null)
        //        return Ok("{\"isRouteAvailable\":\"TRUE\"}");
        //    else
        //        return Ok("{\"isRouteAvailable\":\"FALSE\"}");
        //}

        //[HttpPost("IsRouteAvailableByRoute/{routeName}", Name = "IsRouteAvailableByRoute")]
        //public IActionResult IsRouteAvailableByRoute([FromBody]string routeName)
        //{
        //    logger.LogDebug("" +DateTime.Now + "\n\n\n =============================\n=============================\n============================= "
        //        + "Tenants2Controller.IsRouteAvailableByRoute Started... ");

        //    if (!_context.Tenants.Any(t => t.RouteName == routeName.ToLower()))
        //    {                
        //        return Ok(routeName.ToLower());
        //    }
        //    else
        //    {                
        //        return new StatusCodeResult(StatusCodes.Status409Conflict);
        //    }
        //}

        //[HttpPut("{id}")]
        //[HttpPut("PutTenant2")]
        //public async Task<IActionResult> PutTenant2([FromRoute] Guid id, [FromBody] Tenant tenant) {
        //    logStart("Tenants2Controller.PutTenant2 ( Update )");
        //    //logger.LogDebug("* " + DateTime.Now + "============================= EditingPopup_Create([DataSourceRequest] DataSourceRequest request=>{0}<=, Tenant tenant=>{1}<=))", request, tenant);
        //    logger.LogDebug("* " + DateTime.Now + "* id=>{0}<=", Newtonsoft.Json.JsonConvert.SerializeObject(id));
        //    logger.LogDebug("* " + DateTime.Now + "* tenant=>{0}<=", Newtonsoft.Json.JsonConvert.SerializeObject(tenant));


        //    if (!ModelState.IsValid) {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != tenant.TenantId) {
        //        return BadRequest();
        //    }

        //    _context.Entry(tenant).State = EntityState.Modified;
        //    _context.Entry(tenant.Address).State = EntityState.Modified;

        //    try {
        //        await _context.SaveChangesAsync(true);
        //    }
        //    catch (DbUpdateConcurrencyException ex) {
        //        if (!TenantExists(id)) {
        //            return NotFound();
        //        } else {
        //            throw;
        //        }
        //    }
        //    logEnd("Tenants2Controller.PutTenant2 ( Update )");
        //    return new OkObjectResult(tenant);
        //}

        //=======================================================================
        [HttpPut("TenantUpdate2")]
        public ActionResult TenantUpdate2([DataSourceRequest] DataSourceRequest request, Tenant tenant) {
            logStart("Tenants2Controller.TenantUpdate2");
            //logger.LogDebug("* " + DateTime.Now + "* request=>{0}<=", Newtonsoft.Json.JsonConvert.SerializeObject(request));
            //logger.LogDebug("* " + DateTime.Now + "* tenant=>{0}<=", Newtonsoft.Json.JsonConvert.SerializeObject(tenant));

            logMember("Tenants2Controller.TenantUpdate2", "request", request);
            logMember("Tenants2Controller.TenantUpdate2", "tenant", tenant);

            try {
                if (tenant == null || !ModelState.IsValid) {
                    logError("TenantUpdate2", new Exception("Tenant Not Found"));
                    return NotFound();
                }

                _context.Entry(tenant).State = EntityState.Modified;
                _context.Entry(tenant.Address).State = EntityState.Modified;

                _context.SaveChangesAsync(true);

                logEnd("Tenants2Controller.TenantUpdate2");

                return Json(new[] { tenant }.ToDataSourceResult(request, ModelState));
            }
            catch (DbUpdateConcurrencyException ex) {
                //logger.LogError("DbUpdateConcurrencyException in ex=>{0}<=", Newtonsoft.Json.JsonConvert.SerializeObject(ex));
                logError("UpdateTenant", ex);
                logEnd("Tenants2Controller.TenantUpdate2");
                throw;
                //if (!TenantExists(id)) {
                //    return NotFound();
                //} else {
                //    throw;
                //}
            }
            catch (Exception ex) {
                //logger.LogError("Exception in TenantUpdate2 ex=>{0}<=", Newtonsoft.Json.JsonConvert.SerializeObject(ex));
                logError("Tenants2Controller.TenantUpdate2", ex);
                logEnd("Tenants2Controller.TenantUpdate2");
                throw;
            }
        }

        //=======================================================================
        //[HttpPost("PostTenant2")]  //this failed to work
        //[HttpPost("PostTenant2")]
        //public async Task<IActionResult> PostTenant2([FromBody] Tenant tenant) {

        //    logStart("Tenants2Controller.PostTenant2 ( Create )");
        //    logger.LogDebug("* " + DateTime.Now + "* tenant=>{0}<=", Newtonsoft.Json.JsonConvert.SerializeObject(tenant));
        //    if (!ModelState.IsValid) {
        //        return BadRequest(ModelState);
        //    }

        //    var newAddress = new Address {
        //        AddressId = Guid.NewGuid(),
        //        Addressee = tenant.Address.Addressee,
        //        CityName = tenant.Address.CityName,
        //        DeliveryLine1 = tenant.Address.DeliveryLine1,
        //        DeliveryLine2 = tenant.Address.DeliveryLine2,
        //        PostalCode = tenant.Address.PostalCode,
        //        StateOrProvinceId = tenant.Address.StateOrProvinceId,
        //        State = TrackableEntityState.IsActive
        //    };

        //    var newTenant = new Tenant {
        //        TenantId = Guid.NewGuid(),
        //        DisplayName = tenant.DisplayName,
        //        RouteName = tenant.RouteName,
        //        State = TrackableEntityState.IsActive,
        //        AddressId = newAddress.AddressId
        //    };

        //    try {
        //        this._context.Addresses.Add(newAddress);
        //        this._context.Tenants.Add(newTenant);

        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException ex) {
        //        if (TenantExists(tenant.TenantId)) {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        } else {

        //            throw;
        //        }
        //    }

        //    logEnd("Tenants2Controller.PostTenant2 ( Create ) ");

        //    return CreatedAtRoute("GetTenant", new { id = newTenant.TenantId }, newTenant);
        //}


        //================================================================================
        [HttpPost("TenantCreate2")]
        public ActionResult TenantCreate2([DataSourceRequest] DataSourceRequest request, Tenant tenant) {
            logStart("Tenants2Controller.TenantCreate2");
            logMember("Tenants2Controller.TenantCreate2", "request", request);
            logMember("Tenants2Controller.TenantCreate2", "tenant", tenant);

            try {
                            
                if (!ModelState.IsValid) {
                    logError("Tenants2Controller.TenantCreate2", new Exception("ModelState NOT Valid"));
                    //return BadRequest(ModelState);  //ModelState.IsValid is always false!
                }

                var newAddress = new Address {
                    AddressId = Guid.NewGuid(),
                    Addressee = "HC Addressee",// tenant.Address.Addressee,
                    CityName = "HC CityName",// tenant.Address.CityName,
                    DeliveryLine1 = "HC DeliveryLine1",// tenant.Address.DeliveryLine1,
                    DeliveryLine2 = "HC DeliveryLine2",// tenant.Address.DeliveryLine2,
                    PostalCode = "12345-7890",// tenant.Address.PostalCode,
                    StateOrProvinceId = new Guid("3d9b654f-9a3d-4b8e-bb75-04ff0ce617af"),//thats WV// tenant.Address.StateOrProvinceId,
                    State = TrackableEntityState.IsActive
                };
                logMember("Tenants2Controller.TenantCreate2", "newAddress", newAddress);

                var newTenant = new Tenant {
                    TenantId = Guid.NewGuid(),
                    DisplayName = tenant.DisplayName,
                    RouteName = tenant.RouteName,
                    State = TrackableEntityState.IsActive,
                    AddressId = newAddress.AddressId
                };
                logMember("Tenants2Controller.TenantCreate2", "newTenant", newTenant);

                this._context.Addresses.Add(newAddress);
                this._context.Tenants.Add(newTenant);

                //await _context.SaveChangesAsync();
                _context.SaveChangesAsync();

                //logger.LogDebug("* " + DateTime.Now + "* newTenant=>{0}<=", Newtonsoft.Json.JsonConvert.SerializeObject(newTenant));

                logEnd("Tenants2Controller.TenantCreate2");

                return CreatedAtRoute("GetTenant", new { id = newTenant.TenantId }, newTenant);
                //return Json(new[] { product }.ToDataSourceResult(request, ModelState));
            }
            catch (DbUpdateException ex) {
                //logger.LogError("DbUpdateException in ex=>{0}<=", Newtonsoft.Json.JsonConvert.SerializeObject(ex));
                logError("Tenants2Controller.TenantCreate2 DbUpdateException", ex);
                logEnd("Tenants2Controller.TenantCreate2");
                if (TenantExists(tenant.TenantId)) {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                } else {

                    throw;
                }
            }
            catch (Exception ex) {
                logError("Tenants2Controller.TenantCreate2", ex);
                throw;
            }


        }


        //===========================================================================

        //[HttpPost()] //this failed to work
        //[HttpPost("CreateTenant2")]  // this failed to work when called by .Create(create => create.Url("api/Tenants").Type(HttpVerbs.Post))
        //public async Task<IActionResult> CreateTenant2([FromBody] Tenant tenant) {
        //    logger.LogDebug("" +DateTime.Now + "\n\n\n =============================\n=============================\n============================= "
        //        + "Tenants2Controller.CreateTenant2 Started... ");
        //    logger.LogDebug("" +DateTime.Now + "============================= CreateTenant2(tenant:{0})", tenant);

        //    if (!ModelState.IsValid) {
        //        return BadRequest(ModelState);
        //    }

        //    var newAddress = new Address {
        //        AddressId = Guid.NewGuid(),
        //        Addressee = tenant.Address.Addressee,
        //        CityName = tenant.Address.CityName,
        //        DeliveryLine1 = tenant.Address.DeliveryLine1,
        //        DeliveryLine2 = tenant.Address.DeliveryLine2,
        //        PostalCode = tenant.Address.PostalCode,
        //        StateOrProvinceId = tenant.Address.StateOrProvinceId,
        //        State = TrackableEntityState.IsActive
        //    };

        //    var newTenant = new Tenant {
        //        TenantId = Guid.NewGuid(),
        //        DisplayName = tenant.DisplayName,
        //        RouteName = tenant.RouteName,
        //        State = TrackableEntityState.IsActive,
        //        AddressId = newAddress.AddressId
        //    };

        //    try {
        //        this._context.Addresses.Add(newAddress);
        //        this._context.Tenants.Add(newTenant);

        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException ex) {
        //        if (TenantExists(tenant.TenantId)) {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        } else {

        //            throw;
        //        }
        //    }

        //    Console.WriteLine("===============================\n CreateTenant2 completed... \n  newTenant=>" + newTenant + "==========================================\n\n");
        //    System.Diagnostics.Debug.WriteLine("===============================\n CreateTenant2 completed... \n==========================================\n\n");

        //    return CreatedAtRoute("GetTenant", new { id = newTenant.TenantId }, newTenant);
        //}

        //===========================================================================
        //[HttpDelete("{id}")]
        //[HttpDelete("DeleteTenant2")]
        //public async Task<IActionResult> DeleteTenant2([FromRoute] Guid id) {
        //    logStart("Tenants2Controller.DeleteTenant2");
        //    logger.LogDebug("* " + DateTime.Now + "* id=>{0}<=", Newtonsoft.Json.JsonConvert.SerializeObject(id));
        //    logger.LogDebug("* " + DateTime.Now + "* not serialized id=>{0}<=", id);

        //    if (!ModelState.IsValid) {
        //        return BadRequest(ModelState);
        //    }

        //    Tenant tenant = await _context.Tenants.SingleAsync(m => m.TenantId == id);
        //    if (tenant == null) {
        //        return NotFound();
        //    }

        //    tenant.State = TrackableEntityState.IsDeleted;
        //    _context.Entry(tenant).State = EntityState.Modified;

        //    await _context.SaveChangesAsync();

        //    logEnd("Tenants2Controller.DeleteTenant2");

        //    return Ok(tenant);
        //}


        [HttpDelete("TenantDelete2")]
        public ActionResult TenantDelete2([DataSourceRequest] DataSourceRequest request, Tenant tenant) {
            logStart("Tenants2Controller.TenantDelete2");
            //logger.LogDebug("* " + DateTime.Now + "* request=>{0}<=", Newtonsoft.Json.JsonConvert.SerializeObject(request));
            //logger.LogDebug("* " + DateTime.Now + "* tenant=>{0}<=", Newtonsoft.Json.JsonConvert.SerializeObject(tenant));
            
            logMember("Tenants2Controller.TenantDelete2", "request", request);
            logMember("Tenants2Controller.TenantDelete2", "tenant", tenant);

            try {
                if (tenant != null) {
                    //_context.Destroy(tenant);

                    tenant.State = TrackableEntityState.IsDeleted;
                    _context.Entry(tenant).State = EntityState.Modified;

                    _context.SaveChangesAsync();
                } else {
                    logError("TenantDelete2", new Exception("Tenant Not Found") );
                    return NotFound();
                }

                logEnd("Tenants2Controller.TenantDelete2");

                return Json(new[] { tenant }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex) {
                logError("Tenants2Controller.TenantDelete2", ex);
                logEnd("Tenants2Controller.TenantDelete2");
                throw;
            }
        }

        private bool TenantExists(Guid id) {
            logger.LogDebug("\n\n\n\n\n =============================\n=============================\n============================= " + DateTime.Now + " == "
                + "Tenants2Controller.TenantExists Started... ");
            logger.LogDebug("" + DateTime.Now + "============================= TenantExists(Guid id)", id);
            return _context.Tenants.Count(e => e.TenantId == id) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            //logger.LogDebug("\n\n\n\n\n =============================\n=============================\n============================= " + DateTime.Now + " == "
            //    + "Tenants2Controller.Dispose Started... ");
            //logger.LogDebug("" + DateTime.Now + "============================= Dispose(bool disposing)", disposing);
            logStart("Tenants2Controller.Dispose");
            logMember("Tenants2Controller.Dispose", "disposing", disposing);
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
            logEnd("Tenants2Controller.Dispose");
        }
    }//end class
}//end namespace