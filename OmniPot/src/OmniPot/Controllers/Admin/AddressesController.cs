using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmniPot.Data;
using System;
using Microsoft.AspNetCore.Authorization;
using OmniPot.Data.Models;

namespace OmniPot.Controllers
{
    //NOTE: This controller may actually not be needed. Addresses are added/updated with the entities they are a member of
    [Produces("application/json")]
    [Route("api/Addresses")]
    //TODO: Some way to restrict this to addresses that are added to locations. See note above, we may not need it at all. 
   // [Authorize]
    public class AddressesController : Controller
    {
        private KindDbContext context;

        public AddressesController(KindDbContext context)
        {
            this.context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public IEnumerable<Data.Models.Address> GetAddresses()
        {

            return context.Addresses.Where(e => e.State == TrackableEntityState.IsActive);
        }

        // GET: api/Addresses/5
        [HttpGet("{id}", Name = "GetAddress")]
        public async Task<IActionResult> GetAddress([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Address address = await context.Addresses.SingleAsync(m => m.AddressId == id);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // PUT: api/Addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress([FromRoute] Guid id, [FromBody] Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.AddressId)
            {
                return BadRequest();
            }

            context.Entry(address).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        [HttpPost]
        public async Task<IActionResult> PostAddress([FromBody] Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Addresses.Add(address);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AddressExists(address.AddressId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetAddress", new { id = address.AddressId }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Address address = await context.Addresses.SingleAsync(m => m.AddressId == id);
            if (address == null)
            {
                return NotFound();
            }

            //Keeping hard delete here for now...
            context.Addresses.Remove(address);
            await context.SaveChangesAsync();

            return Ok(address);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddressExists(Guid id)
        {
            return context.Addresses.Count(e => e.AddressId == id) > 0;
        }
    }
}