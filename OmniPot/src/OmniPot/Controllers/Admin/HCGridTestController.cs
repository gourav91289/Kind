using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using OmniPot.Data;
using OmniPot.Services;

namespace OmniPot.Controllers
{
    [Produces("application/json")]
    [Route("api/HCGridTest")]
    public class HCGridTestController : BaseController
    {
        //protected readonly ILogger logger;
        static List<string> allValues = new List<string> { "value1", "value2", "value3", "value4" };
        //static List<Tenant> myTenants = new List<Tenant>();

        public HCGridTestController(KindDbContext context, TenantCacheService tenantCacheService, ILoggerFactory loggerFactory)
        : base(context, tenantCacheService, loggerFactory) {
            //logger = loggerFactory.CreateLogger<TenantsController>();
            logMessage("HCGridTestController(constructor)", "RAN");
            //logger.LogDebug("\n\n\n\n\n =============================\n=============================\n============================= " + DateTime.Now + " == "
            //    + "HCGridTestController(constructor) Started =======");
        }

        // GET /api/values
        [HttpGet("GetValues")]
        public IEnumerable<string> GetValues() {
            logStart("HttpGet(\"GetValues\")] HCGridTestController.GetValues()");
            //logger.LogDebug("\n\n\n\n\n =============================\n=============================\n============================= " + DateTime.Now + " == "
            //    + "HCGridTestController.GetValues() Started... ");

            return allValues;
        }

        // GET /api/values/5
        [HttpGet("GetOne")]
        public string GetOne(int id) {
            logStart("HttpGet(\"GetOne\")] HCGridTestController.GetOne()");
            logMember("HttpGet(\"GetOne\")] HCGridTestController.GetOne()", "id", id);
            logMember("HttpGet(\"GetOne\")] HCGridTestController.GetOne()", "allValues[id]", allValues[id]);
            //logger.LogDebug("\n\n\n\n\n =============================\n=============================\n============================= " + DateTime.Now + " == "
            //    + "HCGridTestController.GetOne(int id {0}) Started... ", id);

            if (id < allValues.Count) {
                return allValues[id];
            } else {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }


        // POST /api/values
        [HttpPost("{id}")]
        //public HttpResponseMessage Post(string value) {
        public HttpResponseMessage Post([FromUri] int ID, [FromBody] String value) { 

            logStart("HttpPost(\"{id}\") HCGridTestController.Post()");
            logMember("HttpPos(\"{id}\") HCGridTestController.Post()", "ID", ID);
            logMember("HttpPos(\"{id}\") HCGridTestController.Post()", "value", value);
            //logger.LogDebug("\n\n\n\n\n =============================\n=============================\n============================= " + DateTime.Now + " == "
            //    + "HCGridTestController.Post(string value {0}) Started... ", value);

            allValues.Add(value);
            //return new HttpResponseMessage <int>( (allValues.Count - 1), HttpStatusCode.Created);
            return new HttpResponseMessage( HttpStatusCode.Created );
        }

        // PUT /api/values/5
        [HttpPut("{id}")]
        //public void Put(int id, string value) {
        public void Put([FromUri] int ID, [FromBody] String value) {
            logStart("HttpPut(\"{id}\") HCGridTestController.Put()");
            logMember("HttpPut(\"{id}\") HCGridTestController.Put()", "ID", ID);
            logMember("HttpPut(\"{id}\") HCGridTestController.Put()", "value", value);
            //logger.LogDebug("\n\n\n\n\n =============================\n=============================\n============================= " + DateTime.Now + " == "
            //    + "HCGridTestController.Put(int id {0}, string value {1}) Started... ", id, value);

            if (ID < allValues.Count) {
                allValues[ID] = value;
            } else {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        // DELETE /api/values/5
        [HttpDelete("{id}")]
        //public void Delete(int id) {
        //public void Delete([FromUri] int ID) {
        public void Delete([FromRoute] int ID) {
            logStart("HttpDelete(\"{id}\") HCGridTestController.Delete()");
            logMember("HttpDelete(\"{id}\") HCGridTestController.Delete()", "ID", ID);
            //logger.LogDebug("\n\n\n\n\n =============================\n=============================\n============================= " + DateTime.Now + " == "
            //    + "HCGridTestController.Delete(int id {0}) Started... ", id);

            if (ID < allValues.Count) {
                allValues.RemoveAt(ID);
            } else {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }


        // ================================================================================

        // GET: HCgridTest
        public ActionResult Index()
        {
            logStart("HCGridTestController.Index()");
            logger.LogDebug("\n\n\n\n\n =============================\n=============================\n============================= " + DateTime.Now + " == "
                + "HCGridTestController.Get() Started... ");

            return View();
        }

        // GET: HCgridTest/Details/5
        public ActionResult Details(int id)
        {
            logStart("HCGridTestController.Details()");
            logMember("HCGridTestController.Details()", "id", id);
            //logger.LogDebug("\n\n\n\n\n =============================\n=============================\n============================= " + DateTime.Now + " == "
            //    + "HCGridTestController.Get() Started... ");

            return View();
        }

        // GET: HCgridTest/Create
        public ActionResult Create()
        {
            logStart("HCGridTestController.Create()");
            logger.LogDebug("\n\n\n\n\n =============================\n=============================\n============================= " + DateTime.Now + " == "
                + "HCGridTestController.Get() Started... ");

            return View();
        }

        // POST: HCgridTest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            logStart("HttpPost HCGridTestController.Create()");
            logMember("HttpPost HCGridTestController.GetOne()", "collection", collection);
            try {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HCgridTest/Edit/5
        public ActionResult Edit(int id)
        {
            logStart("HCGridTestController.Edit()");
            logMember("HCGridTestController.Edit()", "id", id);
            return View();
        }

        // POST: HCgridTest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            logStart("HttpPost HCGridTestController.Edit()");
            logMember("HttpPost HCGridTestController.GetOne()", "id", id);
            logMember("HttpPost HCGridTestController.GetOne()", "collection", collection);
            try {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //// GET: HCgridTest/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: HCgridTest/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            logStart("HttpPost HCGridTestController.Delete()");
            logMember("HttpPost HCGridTestController.Delete()", "id", id);
            logMember("HttpPost HCGridTestController.Delete()", "collection", collection);
            try {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}