using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using GameBookASP.Data;
using GameBookASP.GameModels;

namespace MDAGameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShopsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShopsController(AppDbContext context)
        {
            _context = context;
        }

        // Get shop items for a location
        [HttpGet("location/{locationId}")]
        public async Task<ActionResult<Shop>> GetShopByLocation(int locationId)
        {
            var shop = await _context.Shops!
                .Include(s => s.ShopItems)
                    .ThenInclude(si => si.Item)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.LocationID == locationId);

            if (shop == null)
                return NotFound();

            // Return anonymous object to control exactly what gets serialized
            return Ok(new
            {
                shop.ShopID,
                shop.LocationID,
                ShopItems = shop.ShopItems.Select(si => new
                {
                    si.ShopItemID,
                    si.ItemID,
                    si.Price,
                    si.Quantity,
                    Item = new
                    {
                        si.Item.Name,
                        si.Item.Description,
                        si.Item.IsDrinkable,
                        si.Item.Effect
                    }
                })
            });
        }

        // Purchase an item
        [HttpPost("buy")]
        public async Task<IActionResult> BuyItem([FromBody] PurchaseRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var userPlayer = await _context.UserPlayers!
                .Include(up => up.Player)
                    .ThenInclude(p => p.Inventory)
                .FirstOrDefaultAsync(up => up.UserId == userId);

            if (userPlayer == null) return NotFound("Player not found");

            var shopItem = await _context.Shops!
                .Include(s => s.ShopItems)
                    .ThenInclude(si => si.Item)
                .SelectMany(s => s.ShopItems)
                .FirstOrDefaultAsync(si => si.ShopItemID == request.ShopItemId);

            if (shopItem == null) return NotFound("Item not found in shop");

            // Check if player has enough coins
            if (userPlayer.Player.Coins < shopItem.Price)
                return BadRequest("Not enough coins");

            // Deduct coins and add item to inventory
            userPlayer.Player.Coins -= shopItem.Price;
            userPlayer.Player.Inventory.Add(shopItem.Item);

            await _context.SaveChangesAsync();

            return Ok(new { 
                message = $"Successfully purchased {shopItem.Item.Name}",
                newBalance = userPlayer.Player.Coins
            });
        }
    }

    public class PurchaseRequest
    {
        public int ShopItemId { get; set; }
    }
} 