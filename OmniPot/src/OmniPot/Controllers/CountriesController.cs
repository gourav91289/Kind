//#define PG_DEBUG //Comment out or remove to not compile these sections of code  - must be at top of each file you want to use it, else "#if PG_DEBUG" will always be false

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmniPot.Data;
using System;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using OmniPot.Data.Models;
using Microsoft.Extensions.Logging;

namespace OmniPot.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[Authorize]
    public class CountriesController : BaseController
    {
        public CountriesController(KindDbContext context, ILoggerFactory loggerFactory)
        : base(context, null, loggerFactory) {
        }

        // GET: Countries
        public async Task<IActionResult> Index() {
            logStart("CountriesController.Index");
            List<Country> countyList = await context.Countries.ToListAsync();
            logMember("CountriesController.Index", "countyList", countyList);
            logEnd("CountriesController.Index");
            //return View(await context.Countries.ToListAsync());
            return View(countyList);
        }


        [HttpGet]
        public IEnumerable<Country> GetCountries()
        {
            logStart("CountriesController.GetCountries");

            return context.Countries
                .Where(e => e.State == TrackableEntityState.IsActive);

        }

        [HttpGet("GetCountriesList")]
        //public DataSourceResult GetList([ModelBinder(BinderType = typeof(DataSourceRequestModelBinder))] DataSourceRequest request)
        public DataSourceResult GetCountriesList([DataSourceRequest]DataSourceRequest request)
        {
            logStart("CountriesController.GetCountriesList");
            var result = context.Countries
                .Where(e => e.State == TrackableEntityState.IsActive)
                //.OrderBy(e => e.DisplayName)  // A sort set in controller causes duplicate where clauses from grid
                //.ToDataSourceResult(request, c => new { c.CountryId, c.DisplayName, c.Abbreviation });
                .ToDataSourceResult(request);
            //logMember("CountriesController.GetCountriesList", "result", result);
            logEnd("CountriesController.GetCountriesList");
            return result; 
        }

        [HttpGet("GetCountriesListDeleted")]
        //public DataSourceResult GetList([ModelBinder(BinderType = typeof(DataSourceRequestModelBinder))] DataSourceRequest request)
        public DataSourceResult GetCountriesListDeleted([DataSourceRequest]DataSourceRequest request) {
            logStart("CountriesController.GetCountriesListDeleted");
            var result = context.Countries
                //.Where(e => e.State == TrackableEntityState.IsActive)
                //.OrderBy(e => e.DisplayName)  // A sort set in controller causes duplicate where clauses from grid
                //.ToDataSourceResult(request, c => new { c.CountryId, c.DisplayName, c.Abbreviation });
                .ToDataSourceResult(request);
            //logMember("CountriesController.GetCountriesListDeleted", "result", result);
            logEnd("CountriesController.GetCountriesListDeleted");
            return result;
        }

        //[HttpGet("GetCountriesDropList")]
        ////public DataSourceResult GetList([ModelBinder(BinderType = typeof(DataSourceRequestModelBinder))] DataSourceRequest request)
        //public ActionResult GetCountriesDropList([DataSourceRequest]DataSourceRequest request) {
        //    logStart("CountriesController.GetCountriesDropList");
        //    var result = context.Countries
        //        .Where(e => e.State == TrackableEntityState.IsActive)
        //        //.OrderBy(e => e.DisplayName)  // A sort set in controller causes duplicate where cluases from grid
        //        .ToDataSourceResult(request, c => new { c.CountryId, c.DisplayName, c.Abbreviation });
        //    logMember("CountriesController.GetCountriesDropList", "result", result);
        //    logEnd("CountriesController.GetCountriesDropList");
        //    return Json(result);//, JsonRequestBehavior.AllowGet) //result;
        //}

        [HttpGet("GetCascadeCountries")]
        public JsonResult GetCascadeCountries() {
            logStart("CountriesController.GetCascadeCountries");
            var result = Json(context.Countries
                .Where(e => e.State == TrackableEntityState.IsActive)
                .Select(c => new { CountryId = c.CountryId, DisplayName = c.DisplayName })
                .OrderBy(s => s.DisplayName)
                );
            //logMember("CountriesController.GetCascadeCountries", "result", result);
            logEnd("CountriesController.GetCascadeCountries");
            return result;
        }

        ////[HttpGet("api/CountriesController/GetCountriesWithStateProvinceCount", Name = "GetCountriesWithStateProvinceCount")]
        ////public IEnumerable<object> GetCountriesWithStateProvinceCount()
        ////{
        ////    // Why is this method even necessary? 


        ////    try
        ////    {

        ////        var myQuery2 = from countries in context.Countries
        ////                      join statesProvinces in context.StatesOrProvinces on countries.CountryId equals statesProvinces.CountryId into statesProvincesInCountry
        ////                      orderby countries.DisplayName
        ////                      select new { Countries = countries, StateProvinceCount = statesProvincesInCountry.Count()  };


        ////        return myQuery2;
        ////    }
        ////    catch (Exception e)
        ////    {

        ////        return null;
        ////    }
        ////}

        [HttpGet("{id}", Name = "GetCountry")]
        public async Task<IActionResult> GetCountry([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Country country = await context.Countries.SingleAsync(m => m.CountryId.ToString() == id);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry([FromRoute] string id, [FromBody] Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Guid.Parse(id) != country.CountryId)
            {
                return BadRequest();
            }

            context.Entry(country).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(Guid.Parse(id)))
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

        [HttpPost("CountryCreate")]
        public async Task<ActionResult> CountryCreate([FromBody]Country country) {
            logStart("CountriesController.CountryCreate");
            logMember("CountriesController.CountryCreate", "country", country);

            if (!ModelState.IsValid) {
                logError("CountriesController.CountryCreate", new Exception("ModelState NOT Valid"));
                logMember("CountriesController.CountryCreate", "ModelState", ModelState);
                //return BadRequest(ModelState);
            }

            try {
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException e) {
                logError("CountriesController.CountryCreate  DbUpdateException", e);
                if (CountryExists(country.CountryId)) {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                } else {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception e) {
                logError("CountriesController.CountryCreate  Exception", e);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            logEnd("CountriesController.CountryCreate");
            return CreatedAtRoute("GetCountry", new { id = country.CountryId }, country);
        }

        //=======================================================================
        [HttpPut("CountryUpdate")]
        public async Task<ActionResult> CountryUpdate([FromBody]Country country) {
            logStart("CountriesController.CountryUpdate");
            logMember("CountriesController.CountryUpdate", "country", country);

            try {
                if (country == null) {
                    logError("CountriesController.CountryUpdate", new Exception("New Country Object was null!"));
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                if (!CountryExists(country.CountryId)) {
                    logError("CountriesController.CountryUpdate", new Exception("Country Not Found"));
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                context.Entry(country).State = EntityState.Modified;

                await context.SaveChangesAsync(true);

                logEnd("CountriesController.UpdateCountry");

                return Ok(country);
            }
            //----------------
            //catch (DbUpdateConcurrencyException ex) {
            //    //logError("CountriesController.CountryUpdate (DbUpdateConcurrencyException)", ex);
            //    //logMember("CountriesController.CountryUpdate", "DbUpdateConcurrencyException", ex);
            //    foreach (var entry in ex.Entries) {
            //        if (entry.Entity is Country) {
            //            // Using a NoTracking query means we get the entity but it is not tracked by the context
            //            // and will not be merged with existing entities in the context.
            //            var databaseEntity = context.Countries.AsNoTracking().Single(p => p.CountryId == ((Country)entry.Entity).CountryId);
            //            var databaseEntry = context.Entry(databaseEntity);
            //            var i = 0;
            //            foreach (var property in entry.Metadata.GetProperties()) {
            //                i++;
            //                logMember("* CountriesController.CountryUpdate "+ i + ": ", "property.Name", property.Name);
            //                var proposedValue = entry.Property(property.Name).CurrentValue;
            //                var originalValue = entry.Property(property.Name).OriginalValue;
            //                var databaseValue = databaseEntry.Property(property.Name).CurrentValue;
            //                logMember("   * CountriesController.CountryUpdate " + i + ": ", "proposedValue", proposedValue);
            //                logMember("   * CountriesController.CountryUpdate " + i + ": ", "originalValue", originalValue);
            //                logMember("   * CountriesController.CountryUpdate " + i + ": ", "databaseValue", databaseValue);

            //                // TODO: Logic to decide which value should be written to database
            //                // entry.Property(property.Name).CurrentValue = <value to be saved>;

            //                // Update original values to
            //                if ((property.Name == "CreatedUtc") || (property.Name == "CreatedByUserId")) {// 
            //                    entry.Property(property.Name).OriginalValue = databaseValue;// databaseEntry.Property(property.Name).CurrentValue;
            //                } else {
            //                    entry.Property(property.Name).OriginalValue = proposedValue;// databaseEntry.Property(property.Name).CurrentValue;
            //                }
            //            }
            //        } else {
            //            throw new NotSupportedException("Don't know how to handle concurrency conflicts for " + entry.Metadata.Name);
            //        }
            //    }

            //    // Retry the save operation
            //    context.SaveChanges();
            //    logEnd("CountriesController.UpdateCountry (DbUpdateConcurrencyException)");
            //    return Ok(country);
            //}

            //----------------
            catch (Exception ex) {
                logError("CountriesController.UpdateCountry", ex);
                logEnd("CountriesController.UpdateCountry");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        ////[HttpDelete("{id}")]
        ////public async Task<IActionResult> DeleteCountry([FromRoute] string id)
        //[HttpDelete("CountryDelete")]
        //public async Task<ActionResult> CountryDelete([FromBody]Country country) {
        //    logStart("CountriesController.CountryDelete");
        //    logMember("CountriesController.CountryDelete", "country", country);

        //    if (country != null) {
        //        country.State = TrackableEntityState.IsDeleted;
        //        context.Entry(country).State = EntityState.Modified;

        //        await context.SaveChangesAsync();
        //    } else {
        //        logError("CountriesController.CountryDelete", new Exception("Country Not Found"));
        //        return new StatusCodeResult(StatusCodes.Status400BadRequest);
        //    }

        //    ////if (!ModelState.IsValid)
        //    ////{
        //    ////    return BadRequest(ModelState);
        //    ////}

        //    ////Country country = await context.Countries.SingleAsync(m => m.CountryId == Guid.Parse(id));
        //    ////if (country == null)
        //    ////{
        //    ////    return NotFound();
        //    ////}

        //    ////country.State = TrackableEntityState.IsDeleted;
        //    ////context.Entry(country).State = EntityState.Modified;

        //    ////await context.SaveChangesAsync();

        //    logEnd("CountriesController.CountryDelete");
        //    return Ok(country);
        //}

        //=======================================================================
        [HttpDelete("CountryDelete")]
        public async Task<ActionResult> CountryDelete([FromBody]Country country) {
            logStart("CountriesController.CountryDelete");

            try {
                logMember("CountriesController.CountryDelete", "country", country);

                if ((country != null) && (CountryExists(country.CountryId)) ) {
                    country.State = TrackableEntityState.IsDeleted;
                    context.Entry(country).State = EntityState.Modified;

                    await context.SaveChangesAsync();
                } else {
                    logError("CountryDelete", new Exception("Country Not Found"));
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                logEnd("CountriesController.CountryDelete");

                //return Json(new[] { country }.ToDataSourceResult(request, ModelState));
                return Ok(country);
            }
            catch (Exception ex) {
                logError("CountriesController.CountryDelete", ex);
                logEnd("CountriesController.CountryDelete");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        //=======================================================================
        [HttpPost("CountryUndelete")]
        public async Task<ActionResult> CountryUndelete([FromBody]Country country) {
            logStart("CountriesController.CountryUndelete");

            try {
                logMember("CountriesController.CountryUndelete", "country", country);

                if ((country != null) && (CountryExists(country.CountryId))) {
                    //context.Destroy(country);

                    country.State = TrackableEntityState.IsActive;
                    context.Entry(country).State = EntityState.Modified;

                    await context.SaveChangesAsync();
                } else {
                    logError("CountryUndelete", new Exception("Country Not Found"));
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                logEnd("CountriesController.CountryUndelete");

                //return Json(new[] { country }.ToDataSourceResult( (request, ModelState));
                return Ok(country);
            }
            catch (Exception ex) {
                logError("CountriesController.CountryUndelete", ex);
                logEnd("CountriesController.CountryUndelete");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        //=======================================================================
        //!!!!  TODO add in appropriate user/role security for this action ( ONLY Super Admin? )
        [HttpPost("CountryHardDelete")]
        [HttpDelete("CountryHardDelete")]
        public async Task<ActionResult> CountryHardDelete([FromBody]Country country) {

            logStart("CountriesController.CountryHardDelete");

            try {
                logMember("CountriesController.CountryHardDelete", "country", country);
                string countryName = country.DisplayName;

                if ((country != null) && (CountryExists(country.CountryId))) {
                    context.Remove(country);
                    await context.SaveChangesAsync();
                    logMessage("CountriesController.CountryHardDelete", "Country " + countryName + " was permanently deleted.");
                } else {
                    logError("CountryHardDelete", new Exception("Country Not Found"));
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                logEnd("CountriesController.CountryHardDelete");

                return Ok(country);
            }
            catch (Exception ex) {
                logError("CountriesController.CountryHardDelete", ex);
                logEnd("CountriesController.CountryHardDelete");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                //throw;
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountryExists(Guid id)
        {
            return context.Countries.Count(e => e.CountryId == id) > 0;
        }
    }
}