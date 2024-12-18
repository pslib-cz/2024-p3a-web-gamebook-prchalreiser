using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameBookASP.Data;
using GameBookASP.GameModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MDAGameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            return await _context.Locations.ToListAsync();
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var userPlayer = await _context.UserPlayers
                .Include(up => up.Player)
                    .ThenInclude(p => p.Inventory)
                .FirstOrDefaultAsync(up => up.UserId == userId);

            if (userPlayer == null)
            {
                return BadRequest("Player not found. Please create a player first.");
            }

            // Check if trying to access location 421 without the required item
            if (id == 421)
            {
                var hasRequiredItem = userPlayer.Player.Inventory?
                    .Any(item => item.ItemID == 1) ?? false;

                if (!hasRequiredItem)
                {
                    return BadRequest(new { message = "You need the Magic Key to enter this location!" });
                }
            }

            return location;
        }

        // PUT: api/Locations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, Location location)
        {
            if (id != location.LocationID)
            {
                return BadRequest();
            }

            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // POST: api/Locations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(Location location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocation", new { id = location.LocationID }, location);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Add this new endpoint
        [HttpPost("{id}/collect-item")]
        public async Task<ActionResult<Location>> CollectItem(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var userPlayer = await _context.UserPlayers
                .Include(up => up.Player)
                .ThenInclude(p => p.Inventory)
                .FirstOrDefaultAsync(up => up.UserId == userId);

            if (userPlayer == null)
            {
                return BadRequest("Player not found");
            }

            if (id == 420) // Hotbox location
            {
                // Check if player already has the item
                var hasItem = userPlayer.Player.Inventory?
                    .Any(item => item.ItemID == 1) ?? false;

                if (hasItem)
                {
                    return BadRequest(new { message = "You already have this item!" });
                }

                // Get the Magic Key item
                var magicKey = await _context.Items.FindAsync(1);
                if (magicKey == null)
                {
                    return BadRequest("Item not found");
                }

                // Initialize inventory if null
                if (userPlayer.Player.Inventory == null)
                {
                    userPlayer.Player.Inventory = new List<Item>();
                }

                // Add item to player's inventory
                userPlayer.Player.Inventory.Add(magicKey);
                
                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Item collected successfully!" });
                }
                catch (Exception)
                {
                    return StatusCode(500, new { message = "Failed to save item to inventory" });
                }
            }

            return BadRequest(new { message = "No item to collect at this location" });
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.LocationID == id);
        }
    }
}
