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
    public class PlayersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlayersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Players!.ToListAsync();
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(Guid id)
        {
            var player = await _context.Players!.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(Guid id, Player player)
        {
            if (id != player.PlayerID)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // POST: api/Players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            if (string.IsNullOrWhiteSpace(player.Name))
            {
                return BadRequest("Player name is required.");
            }

            // Check if user already has a player
            var existingUserPlayer = await _context.UserPlayers!
                .Include(up => up.Player)
                .FirstOrDefaultAsync(up => up.UserId == userId);

            if (existingUserPlayer != null)
            {
                return Ok(existingUserPlayer.Player);
            }

            // Set initial coins to 50
            player.Coins = 50;

            _context.Players!.Add(player);
            await _context.SaveChangesAsync();

            // UserPlayer link
            var userPlayer = new UserPlayer
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                PlayerId = player.PlayerID
            };

            _context.UserPlayers!.Add(userPlayer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayer", new { id = player.PlayerID }, player);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(Guid id)
        {
            var player = await _context.Players!.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players!.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerExists(Guid id)
        {
            return _context.Players!.Any(e => e.PlayerID == id);
        }

        // Add this new endpoint
        [HttpGet("has-item/{itemId}")]
        public async Task<ActionResult<bool>> HasItem(int itemId)
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
                return BadRequest();
            }

            var hasItem = userPlayer.Player.Inventory?
                .Any(item => item.ItemID == itemId) ?? false;

            return Ok(hasItem);
        }

        [HttpGet("current")]
        public async Task<ActionResult<Player>> GetCurrentPlayer()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var userPlayer = await _context.UserPlayers!
                .Include(up => up.Player)
                .FirstOrDefaultAsync(up => up.UserId == userId);

            if (userPlayer?.Player == null)
            {
                return NotFound();
            }

            return userPlayer.Player;
        }

    }
}
