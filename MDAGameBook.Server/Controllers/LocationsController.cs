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
using System.Text.Json;

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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            return await _context.Locations!.ToListAsync();
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var location = await _context.Locations!
                .Include(l => l.Shop)
                    .ThenInclude(s => s!.ShopItems)
                        .ThenInclude(si => si.Item)
                .FirstOrDefaultAsync(l => l.LocationID == id);

            if (location == null)
            {
                return NotFound();
            }

            var userPlayer = await _context.UserPlayers!
                .Include(up => up.Player)
                    .ThenInclude(p => p.Inventory)
                .FirstOrDefaultAsync(up => up.UserId == userId);

            if (userPlayer == null)
            {
                // Create new player if doesn't exist
                var player = new Player
                {
                    PlayerID = Guid.NewGuid(),
                    Health = 100,
                    Withdrawal = 0,
                    Stamina = 100,
                    Coins = 0,
                    LastLocationID = id,
                    Inventory = new List<Item>()
                };

                userPlayer = new UserPlayer
                {
                    UserId = userId,
                    Player = player
                };

                _context.UserPlayers!.Add(userPlayer);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Update the player's last location
                userPlayer.Player.LastLocationID = id;
                await _context.SaveChangesAsync();
            }

            return location;
        }

        // PUT: api/Locations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Location>> PostLocation(Location location)
        {
            _context.Locations!.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocation", new { id = location.LocationID }, location);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _context.Locations!.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/collect-item")]
        public async Task<ActionResult<Location>> CollectItem(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var userPlayer = await _context.UserPlayers!
                .Include(up => up.Player)
                .ThenInclude(p => p.Inventory)
                .FirstOrDefaultAsync(up => up.UserId == userId);

            if (userPlayer == null)
            {
                return BadRequest("Player not found");
            }

            var location = await _context.Locations!.FindAsync(id);
            if (location == null)
            {
                return NotFound("Location not found");
            }

            try
            {
                var locationItems = JsonSerializer.Deserialize<int[]>(location.Items ?? "[]");
                if (locationItems == null || !locationItems.Any())
                {
                    return BadRequest(new { message = $"No items to collect at this location. Items: {location.Items}" });
                }

                foreach (var itemId in locationItems)
                {
                    var hasItem = userPlayer.Player.Inventory?
                        .Any(item => item.ItemID == itemId) ?? false;

                    if (hasItem)
                    {
                        continue;
                    }

                    var item = await _context.Items!.FindAsync(itemId);
                    if (item == null)
                    {
                        continue;
                    }

                    if (userPlayer.Player.Inventory == null)
                    {
                        userPlayer.Player.Inventory = new List<Item>();
                    }

                    userPlayer.Player.Inventory.Add(item);

                    var remainingItems = locationItems.Where(i => i != itemId).ToArray();
                    location.Items = JsonSerializer.Serialize(remainingItems);

                    await _context.SaveChangesAsync();
                    return Ok(new { message = $"{item.Name} collected successfully!" });
                }

                return BadRequest(new { message = "You already have all items from this location" });
            }
            catch (JsonException ex)
            {
                return BadRequest(new { message = $"Invalid items data format: {ex.Message}" });
            }
        }

        [HttpGet("last-location")]
        public async Task<ActionResult<Location>> GetLastLocation()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var userPlayer = await _context.UserPlayers!
                .Include(up => up.Player)
                .FirstOrDefaultAsync(up => up.UserId == userId);

            if (userPlayer == null)
            {
                return BadRequest("Player not found");
            }

            var location = await _context.Locations!.FindAsync(userPlayer.Player.LastLocationID - 1);
            if (location == null)
            {
                return NotFound("Last location not found");
            }

            return location;
        }

        [HttpGet("player")]
        public async Task<ActionResult<IEnumerable<Location>>> GetPlayerLocations()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            return await _context.Locations!.ToListAsync();
        }

        private bool LocationExists(int id)
        {
            return _context.Locations!.Any(e => e.LocationID == id);
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
