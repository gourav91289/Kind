//#define PG_DEBUG //Comment out or remove to not compile these sections of code  - must be at top of each file you want to use it, else "#if PG_DEBUG" will always be false
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
using Microsoft.Extensions.Logging;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace OmniPot.Controllers
{
    [Produces("application/json")]
    [Route("api/StatesOrProvinces")]
    //[Authorize(Roles = "SuperAdmin,TechSupport,TenantAdmin")] 
    public class StatesOrProvincesController : BaseController
    {
        private KindDbContext _context;

        public StatesOrProvincesController(KindDbContext context, ILoggerFactory loggerFactory)
        : base(context, null, loggerFactory) {
            _context = context;
        }

        [HttpGet]
        [HttpGet()]
        public IEnumerable<StateOrProvince> GetStateOrProvinces()
        {

            return _context.StatesOrProvinces.Include(e => e.Country).Where(e => e.State == TrackableEntityState.IsActive);

        }

        [HttpGet("GetStatesList")]
        //public DataSourceResult GetStatesList([ModelBinder(BinderType = typeof(DataSourceRequestModelBinder))] DataSourceRequest request)
        public DataSourceResult GetStatesList([DataSourceRequest]DataSourceRequest request) {
            logStart("StatesOrProvincesController.GetStatesList");
            var result = _context.StatesOrProvinces
                .Include(e => e.Country)
                .Where(e => e.State == TrackableEntityState.IsActive)
                .OrderBy(e => e.DisplayName)  // A sort set in controller causes duplicate where clauses from grid
                //.ToDataSourceResult(request, c => new { c.StateOrProvinceId, c.DisplayName, c.Abbreviation, c.Country });
                .ToDataSourceResult(request);
            //logMember("StatesOrProvincesController.GetStatesList", "result", result);
            logEnd("StatesOrProvincesController.GetStatesList");
            return result;
        }

        [HttpGet("GetStatesListDeleted")]
        //public DataSourceResult GetStatesList([ModelBinder(BinderType = typeof(DataSourceRequestModelBinder))] DataSourceRequest request)
        public DataSourceResult GetStatesListDeleted([DataSourceRequest]DataSourceRequest request) {
            logStart("StatesOrProvincesController.GetStatesListDeleted");
            var result = _context.StatesOrProvinces
                .Include(e => e.Country)
                //.ToDataSourceResult(request, c => new { c.StateOrProvinceId, c.DisplayName, c.Abbreviation, c.Country });
                .ToDataSourceResult(request);
            //logMember("StatesOrProvincesController.GetStatesListDeleted", "result", result);
            logEnd("StatesOrProvincesController.GetStatesListDeleted");
            return result;
        }

        [HttpPost("StateOrProvinceCreate")]
        public async Task<ActionResult> StateOrProvinceCreate([FromBody]StateOrProvince stateOrProvince) {
            logStart("StatesOrProvincesController.StateOrProvinceCreate");
            logMember("StatesOrProvincesController.StateOrProvinceCreate", "stateOrProvince", stateOrProvince);

            if (!ModelState.IsValid) {
                logError("StatesOrProvincesController.StateOrProvinceCreate", new Exception("ModelState NOT Valid"));
                logMember("StatesOrProvincesController.StateOrProvinceCreate", "ModelState", ModelState);
                //return BadRequest(ModelState);
            }

            try {
                context.StatesOrProvinces.Add(stateOrProvince);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException e) {
                logError("StatesOrProvincesController.StateOrProvinceCreate  DbUpdateException", e);
                if (StateOrProvinceExists(stateOrProvince.StateOrProvinceId)) {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                } else {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception e) {
                logError("StatesOrProvincesController.StateOrProvinceCreate  Exception", e);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            logEnd("StatesOrProvincesController.StateOrProvinceCreate");
            return CreatedAtRoute("GetStateOrProvince", new { id = stateOrProvince.StateOrProvinceId }, stateOrProvince);
        }

        //=======================================================================
        [HttpPut("StateOrProvinceUpdate")]
        public async Task<ActionResult> StateOrProvinceUpdate([FromBody]StateOrProvince stateOrProvince) {
            logStart("StatesOrProvincesController.StateOrProvinceUpdate");
            logMember("StatesOrProvincesController.StateOrProvinceUpdate", "stateOrProvince", stateOrProvince);

            try {
                if (stateOrProvince == null || !ModelState.IsValid) {
                    logError("StatesOrProvincesController.StateOrProvinceUpdate", new Exception("StateOrProvince Not Found"));
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                if (!StateOrProvinceExists(stateOrProvince.StateOrProvinceId)) {
                    return new StatusCodeResult(StatusCodes.Status404NotFound);
                }

                context.Entry(stateOrProvince).State = EntityState.Modified;

                await context.SaveChangesAsync(true);

                logEnd("StatesOrProvincesController.UpdateStateOrProvince");

                return Ok(stateOrProvince);
            }
            catch (Exception ex) {
                logError("StatesOrProvincesController.UpdateStateOrProvince", ex);
                logEnd("StatesOrProvincesController.UpdateStateOrProvince");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        //=======================================================================
        [HttpDelete("StateOrProvinceDelete")]
        public async Task<ActionResult> StateOrProvinceDelete([FromBody]StateOrProvince stateOrProvince) {
            logStart("StatesOrProvincesController.StateOrProvinceDelete");

            try {
                logMember("StatesOrProvincesController.StateOrProvinceDelete", "stateOrProvince", stateOrProvince);

                if (stateOrProvince != null) {
                    stateOrProvince.State = TrackableEntityState.IsDeleted;
                    _context.Entry(stateOrProvince).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                } else {
                    logError("StatesOrProvincesController.StateOrProvinceDelete", new Exception("StateOrProvince Not Found"));
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                logEnd("StatesOrProvincesController.StateOrProvinceDelete");

                //return Json(new[] { stateOrProvince }.ToDataSourceResult(request, ModelState));
                return Ok(stateOrProvince);
            }
            catch (Exception ex) {
                logError("StatesOrProvincesController.StateOrProvinceDelete", ex);
                logEnd("StatesOrProvincesController.StateOrProvinceDelete");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        //=======================================================================
        [HttpPost("StateOrProvinceUndelete")]
        public async Task<ActionResult> StateOrProvinceUndelete([FromBody]StateOrProvince stateOrProvince) {
            logStart("StatesOrProvincesController.StateOrProvinceUndelete");

            try {
                logMember("StatesOrProvincesController.StateOrProvinceUndelete", "stateOrProvince", stateOrProvince);

                if (stateOrProvince != null) {
                    //_context.Destroy(stateOrProvince);

                    stateOrProvince.State = TrackableEntityState.IsActive;
                    _context.Entry(stateOrProvince).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                } else {
                    logError("StatesOrProvincesController.StateOrProvinceUndelete", new Exception("StateOrProvince Not Found"));
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                logEnd("StatesOrProvincesController.StateOrProvinceUndelete");

                //return Json(new[] { stateOrProvince }.ToDataSourceResult( (request, ModelState));
                return Ok(stateOrProvince);
            }
            catch (Exception ex) {
                logError("StatesOrProvincesController.StateOrProvinceUndelete", ex);
                logEnd("StatesOrProvincesController.StateOrProvinceUndelete");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        //=======================================================================
        //!!!!  TODO add in appropriate user/role security for this action ( ONLY Super Admin? )
        [HttpPost("StateOrProvinceHardDelete")]
        [HttpDelete("StateOrProvinceHardDelete")]
        public async Task<ActionResult> StateOrProvinceHardDelete([FromBody]StateOrProvince stateOrProvince) {

            logStart("StatesOrProvincesController.StateOrProvinceHardDelete");

            try {
                logMember("StatesOrProvincesController.StateOrProvinceHardDelete", "stateOrProvince", stateOrProvince);
                string stateOrProvinceName = stateOrProvince.DisplayName;

                if (stateOrProvince != null) {
                    _context.Remove(stateOrProvince);
                    await _context.SaveChangesAsync();
                    logMessage("StatesOrProvincesController.StateOrProvinceHardDelete", "StateOrProvince " + stateOrProvinceName + " was permanently deleted.");
                } else {
                    logError("StatesOrProvincesController.StateOrProvinceHardDelete", new Exception("StateOrProvince Not Found"));
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                logEnd("StatesOrProvincesController.StateOrProvinceHardDelete");

                return Ok(stateOrProvince);
            }
            catch (Exception ex) {
                logError("StatesOrProvincesController.StateOrProvinceHardDelete", ex);
                logEnd("StatesOrProvincesController.StateOrProvinceHardDelete");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                //throw;
            }
        }

        //[HttpGet("FilterMenuCustomization_Countries")]
        //public ActionResult FilterMenuCustomization_Countries() {
        //    logStart("StatesOrProvincesController.FilterMenuCustomization_Countries");
        //    var result = _context.StatesOrProvinces
        //        .Include(e => e.StateOrProvince)
        //        .Where(e => e.State == TrackableEntityState.IsActive)
        //        .ToDataSourceResult();
        //    //return Json(db.Employees.Select(e => e.City).Distinct(), JsonRequestBehavior.AllowGet);
        //    logEnd("StatesOrProvincesController.FilterMenuCustomization_Countries");
        //    return result;
        //}

        [HttpGet("GetCascadeStates")]
        public JsonResult GetCascadeStates(Guid? countryId, string filterStates) {
            logStart("StatesOrProvincesController.GetCascadeStates");
            logMember("StatesOrProvincesController.GetCascadeStates", "countryId", countryId);
            logMember("StatesOrProvincesController.GetCascadeStates", "filterStates", filterStates);

            var states = context.StatesOrProvinces.AsQueryable();

            states = states.Where(p => p.State == TrackableEntityState.IsActive);

            if (countryId != null) {
                states = states.Where(p => p.CountryId == countryId);//.OrderBy(e => e.DisplayName);
            }

            if (!string.IsNullOrEmpty(filterStates)) {
                states = states.Where(p => p.DisplayName.Contains(filterStates));//.OrderBy(e => e.DisplayName);
            }

            states = states.OrderBy(e => e.DisplayName);

            var result = Json(states.Select(p => new { StateOrProvinceId = p.StateOrProvinceId, DisplayName = p.DisplayName }));

            logMember("StatesOrProvincesController.GetCascadeStates", "result", result);
            logEnd("StatesOrProvincesController.GetCascadeStates");
            return result;
        }

        [HttpGet("{id}", Name = "GetStateOrProvince")]
        public async Task<IActionResult> GetStateOrProvince([FromRoute] Guid id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StateOrProvince stateOrProvince = await _context.StatesOrProvinces.SingleAsync(m => m.StateOrProvinceId == id);

            if (stateOrProvince == null)
            {
                return NotFound();
            }

            return Ok(stateOrProvince);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStateOrProvince([FromRoute] Guid id, [FromBody] StateOrProvince stateOrProvince)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stateOrProvince.StateOrProvinceId)
            {
                return BadRequest();
            }

            _context.Entry(stateOrProvince).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!StateOrProvinceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new OkObjectResult(stateOrProvince);
        }

        [HttpPost]
        public async Task<IActionResult> PostStateOrProvince([FromBody] StateOrProvince stateOrProvince)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newStateOrProvince = new StateOrProvince
            {
                StateOrProvinceId = Guid.NewGuid(),
                DisplayName = stateOrProvince.DisplayName,
                State = TrackableEntityState.IsActive,
            };

            try
            {
                this._context.StatesOrProvinces.Add(newStateOrProvince);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (StateOrProvinceExists(stateOrProvince.StateOrProvinceId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {

                    throw;
                }
            }

            return CreatedAtRoute("GetStateOrProvince", new { id = newStateOrProvince.StateOrProvinceId }, newStateOrProvince);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStateOrProvince([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StateOrProvince stateOrProvince = await _context.StatesOrProvinces.SingleAsync(m => m.StateOrProvinceId == id);
            if (stateOrProvince == null)
            {
                return NotFound();
            }

            stateOrProvince.State = TrackableEntityState.IsDeleted;
            _context.Entry(stateOrProvince).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(stateOrProvince);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StateOrProvinceExists(Guid id)
        {
            return _context.StatesOrProvinces.Count(e => e.StateOrProvinceId == id) > 0;
        }
    }
}




























////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Threading.Tasks;
////using Microsoft.AspNetCore.Mvc;

////// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

////namespace OmniPot.Controllers.Admin
////{
////    [Route("api/[controller]")]
////    public class StateOrProvinceController : Controller
////    {
////        // GET: api/values
////        [HttpGet]
////        public IEnumerable<string> Get()
////        {
////            return new string[] { "value1", "value2" };
////        }

////        // GET api/values/5
////        [HttpGet("{id}")]
////        public string Get(int id)
////        {
////            return "value";
////        }

////        // POST api/values
////        [HttpPost]
////        public void Post([FromBody]string value)
////        {
////        }

////        // PUT api/values/5
////        [HttpPut("{id}")]
////        public void Put(int id, [FromBody]string value)
////        {
////        }

////        // DELETE api/values/5
////        [HttpDelete("{id}")]
////        public void Delete(int id)
////        {
////        }
////    }
////}
