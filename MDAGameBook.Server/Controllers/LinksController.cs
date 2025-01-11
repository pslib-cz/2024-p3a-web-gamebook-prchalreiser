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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

            var userPlayer = await _context.UserPlayers!
                .Include(up => up.Player)
                .Include(up => up.Player.Inventory)
                .FirstOrDefaultAsync(up => up.UserId == userId);

            if (userPlayer == null)
            {
                return BadRequest("Player not found");
            }

            // Get all available links
            var links = await _context.Links!
                .Include(l => l.ToLocation)
                .Where(l => l.FromLocationID == locationId)
                .ToListAsync();

            // Filter links based on required items
            var filteredLinks = links.Where(link =>
            {
                if (link.RequiredItemId == null)
                    return true;

                return userPlayer.Player.Inventory?
                    .Any(item => item.ItemID == link.RequiredItemId) ?? false;
            });

            return Ok(filteredLinks);
        }

        private bool LinkExists(int id)
        {
            return _context.Links!.Any(e => e.LinkID == id);
        }

        private async Task<bool> IsUserAdmin()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return false;
            }

            var userRoles = await _context.UserRoles.Where(ur => ur.UserId == userId).ToListAsync();
            var userRoleIds = userRoles.Select(ur => ur.RoleId).ToList();
            var adminRoleId = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");

            return adminRoleId != null && userRoleIds.Contains(adminRoleId.Id);
        }
    }
}
