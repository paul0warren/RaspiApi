using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaspiApi.World;

namespace RaspiApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Countrylanguages")]
    public class CountrylanguagesController : Controller
    {
        private readonly worldContext _context;

        public CountrylanguagesController(worldContext context)
        {
            _context = context;
        }

        // GET: api/Countrylanguages
        [HttpGet]
        public IEnumerable<Countrylanguage> GetCountrylanguage()
        {
            return _context.Countrylanguage;
        }

        // GET: api/Countrylanguages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountrylanguage([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countrylanguage = await _context.Countrylanguage.SingleOrDefaultAsync(m => m.CountryCode == id);

            if (countrylanguage == null)
            {
                return NotFound();
            }

            return Ok(countrylanguage);
        }

        // PUT: api/Countrylanguages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountrylanguage([FromRoute] string id, [FromBody] Countrylanguage countrylanguage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != countrylanguage.CountryCode)
            {
                return BadRequest();
            }

            _context.Entry(countrylanguage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountrylanguageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countrylanguages
        [HttpPost]
        public async Task<IActionResult> PostCountrylanguage([FromBody] Countrylanguage countrylanguage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Countrylanguage.Add(countrylanguage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountrylanguage", new { id = countrylanguage.CountryCode }, countrylanguage);
        }

        // DELETE: api/Countrylanguages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountrylanguage([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countrylanguage = await _context.Countrylanguage.SingleOrDefaultAsync(m => m.CountryCode == id);
            if (countrylanguage == null)
            {
                return NotFound();
            }

            _context.Countrylanguage.Remove(countrylanguage);
            await _context.SaveChangesAsync();

            return Ok(countrylanguage);
        }

        private bool CountrylanguageExists(string id)
        {
            return _context.Countrylanguage.Any(e => e.CountryCode == id);
        }
    }
}