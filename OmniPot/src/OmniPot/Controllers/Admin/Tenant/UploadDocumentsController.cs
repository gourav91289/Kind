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
    [Route("api/{tenant}/Uploads")]
    [Authorize(Roles = "SuperAdmin,TenantAdmin,TechSupport")]
    public class UploadDocumentsController : BaseController
    {
        private readonly KindDbContext context;
        private readonly TenantCacheService tenantCache;

        public UploadDocumentsController(KindDbContext context, TenantCacheService tenantCacheService, ILoggerFactory loggerFactory)
            :base(context, tenantCacheService, loggerFactory)
        {
        }
        
        [HttpGet]
        public IEnumerable<UploadDocument> GetFiles([FromRoute] string tenant)
        {
            var tenantId = tenantCache.GetId(tenant);
            //This is not intented to send the entire entity back, just for listing if need be for files based just on a tenant. Get is used if the file should be fetched.
            return context.UploadDocuments.Where(d => d.TenantId == tenantId && d.State == TrackableEntityState.IsActive).Select(d => new UploadDocument { UploadDocumentId = d.UploadDocumentId, DisplayName = d.DisplayName, ContentType = d.ContentType, ModifiedByUserId = d.ModifiedByUserId, CreatedByUserId = d.CreatedByUserId, ModifiedUtc = d.ModifiedUtc, CreatedUtc = d.CreatedUtc });             
        }

        [HttpPost]
        public async Task<IActionResult> HandleUpload([FromRoute] string tenant)
        {
            var tenantId = tenantCache.GetId(tenant); 
            //TODO: for each file in the request, create a new UploadDocument
            var files = (await Request.ReadFormAsync()).Files; 
            throw new NotImplementedException();
        }


        
    }
}