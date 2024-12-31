using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameBookASP.Data;
using GameBookASP.GameModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MDAGameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LinksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LinksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Links
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Link>>> GetLinks()
        {
            return await _context.Links!.Include(l => l.ToLocation).ToListAsync();
        }

        // GET: api/Links/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Link>> GetLink(int id)
        {
            var link = await _context.Links!.Include(l => l.ToLocation).FirstOrDefaultAsync(l => l.LinkID == id);

            if (link == null)
            {
                return NotFound();
            }

            return link;
        }

        // PUT: api/Links/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLink(int id, Link link)
        {
            if (id != link.LinkID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Detach any existing navigation properties to prevent tracking conflicts
            link.FromLocation = null;
            link.ToLocation = null;

            // Verify that the locations exist
            var fromLocationExists = await _context.Locations!.AnyAsync(l => l.LocationID == link.FromLocationID);
            var toLocationExists = await _context.Locations!.AnyAsync(l => l.LocationID == link.ToLocationID);

            if (!fromLocationExists || !toLocationExists)
            {
                return BadRequest("One or both locations not found");
            }

            _context.Entry(link).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinkExists(id))
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

        // POST: api/Links
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Link>> PostLink(Link link)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Detach any existing navigation properties to prevent tracking conflicts
            link.FromLocation = null;
            link.ToLocation = null;

            // Verify that the locations exist
            var fromLocationExists = await _context.Locations!.AnyAsync(l => l.LocationID == link.FromLocationID);
            var toLocationExists = await _context.Locations!.AnyAsync(l => l.LocationID == link.ToLocationID);

            if (!fromLocationExists || !toLocationExists)
            {
                return BadRequest("One or both locations not found");
            }

            _context.Links!.Add(link);
            await _context.SaveChangesAsync();

            // Load the ToLocation for the response
            await _context.Entry(link)
                .Reference(l => l.ToLocation)
                .LoadAsync();

            return CreatedAtAction("GetLink", new { id = link.LinkID }, link);
        }

        // DELETE: api/Links/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLink(int id)
        {
            var link = await _context.Links!.FindAsync(id);
            if (link == null)
            {
                return NotFound();
            }

            _context.Links.Remove(link);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("from/{locationId}")]
        public async Task<ActionResult<IEnumerable<Link>>> GetLinksFromLocation(int locationId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var links = await _context.Links!
                .Include(l => l.ToLocation)
                .Where(l => l.FromLocationID == locationId)
                .ToListAsync();

            return Ok(links);
        }

        private bool LinkExists(int id)
        {
            return _context.Links!.Any(e => e.LinkID == id);
        }
    }
}
