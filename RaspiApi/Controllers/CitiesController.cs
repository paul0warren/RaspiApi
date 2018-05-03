﻿using System;
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
    [Route("api/Cities")]
    public class CitiesController : Controller
    {
        private readonly worldContext _context;

        public CitiesController(worldContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        [HttpGet]
        public IEnumerable<City> GetCity()
        {
            return _context.City;
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = await _context.City.SingleOrDefaultAsync(m => m.Id == id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        // PUT: api/Cities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity([FromRoute] int id, [FromBody] City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != city.Id)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST: api/Cities
        [HttpPost]
        public async Task<IActionResult> PostCity([FromBody] City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.City.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = city.Id }, city);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = await _context.City.SingleOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            _context.City.Remove(city);
            await _context.SaveChangesAsync();

            return Ok(city);
        }

        private bool CityExists(int id)
        {
            return _context.City.Any(e => e.Id == id);
        }
    }
}