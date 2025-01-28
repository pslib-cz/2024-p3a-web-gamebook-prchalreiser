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

        // GET: api/Shops
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<object>>> GetShops()
        {
            var shops = await _context.Shops!
                .Include(s => s.ShopItems)
                    .ThenInclude(si => si.Item)
                .AsNoTracking()
                .ToListAsync();

            var result = shops.Select(shop => new
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

            return Ok(result);
        }

        // GET: api/Shops/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<object>> GetShop(Guid id)
        {
            var shop = await _context.Shops!
                .Include(s => s.ShopItems)
                    .ThenInclude(si => si.Item)
                .FirstOrDefaultAsync(s => s.ShopID == id);

            if (shop == null)
            {
                return NotFound();
            }

            var result = new
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
            };

            return Ok(result);
        }

        // PUT: api/Shops/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutShop(Guid id, Shop shop)
        {
            if (id != shop.ShopID)
            {
                return BadRequest();
            }

            _context.Entry(shop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopExists(id))
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

        // POST: api/Shops
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Shop>> PostShop([FromBody] CreateShopRequest request)
        {
            // Verify that the location exists
            var locationExists = await _context.Locations!.AnyAsync(l => l.LocationID == request.LocationID);
            if (!locationExists)
            {
                return BadRequest("Location not found");
            }

            var shop = new Shop
            {
                ShopID = Guid.NewGuid(),
                LocationID = request.LocationID,
                ItemsForSale = "[]", // Required due to backward compatibility
                ShopItems = new List<ShopItem>()
            };

            _context.Shops!.Add(shop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShop", new { id = shop.ShopID }, shop);
        }

        public class CreateShopRequest
        {
            public int LocationID { get; set; }
        }

        // DELETE: api/Shops/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteShop(Guid id)
        {
            var shop = await _context.Shops!.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }

            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShopExists(Guid id)
        {
            return _context.Shops!.Any(e => e.ShopID == id);
        }

        // Additional endpoints for shop items management
        [HttpPost("{shopId}/items")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ShopItem>> AddShopItem(Guid shopId, [FromBody] AddShopItemRequest request)
        {
            var shop = await _context.Shops!.FindAsync(shopId);
            if (shop == null)
            {
                return NotFound("Shop not found");
            }

            var item = await _context.Items!.FindAsync(request.ItemID);
            if (item == null)
            {
                return NotFound("Item not found");
            }

            var shopItem = new ShopItem
            {
                ItemID = request.ItemID,
                Price = request.Price,
                Quantity = request.Quantity,
                ShopID = shopId
            };

            _context.Set<ShopItem>().Add(shopItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShop), new { id = shopId }, shopItem);
        }

        public class AddShopItemRequest
        {
            public int ItemID { get; set; }
            public int Price { get; set; }
            public int Quantity { get; set; }
        }

        [HttpDelete("items/{shopItemId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteShopItem(int shopItemId)
        {
            var shopItem = await _context.Set<ShopItem>().FindAsync(shopItemId);
            if (shopItem == null)
            {
                return NotFound();
            }

            _context.Set<ShopItem>().Remove(shopItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Add this new endpoint to get shop by location ID
        [HttpGet("location/{locationId}")]
        public async Task<ActionResult<object>> GetShopByLocation(int locationId)
        {
            var shop = await _context.Shops!
                .Include(s => s.ShopItems)
                    .ThenInclude(si => si.Item)
                .FirstOrDefaultAsync(s => s.LocationID == locationId);

            if (shop == null)
            {
                return NotFound();
            }

            var result = new
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
            };

            return Ok(result);
        }

        public class PurchaseItemRequest
        {
            public int ShopItemId { get; set; }
        }

        [HttpPost("buy")]
        public async Task<ActionResult<object>> PurchaseItem([FromBody] PurchaseItemRequest request)
        {
            // Get the current user's ID from claims
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            // Find the shop item
            var shopItem = await _context.Set<ShopItem>()
                .Include(si => si.Item)
                .FirstOrDefaultAsync(si => si.ShopItemID == request.ShopItemId);

            if (shopItem == null)
            {
                return NotFound("Shop item not found");
            }

            // Get the player through UserPlayers relationship
            var userPlayer = await _context.UserPlayers!
                .Include(up => up.Player)
                .FirstOrDefaultAsync(up => up.UserId == userId);

            if (userPlayer?.Player == null)
            {
                return NotFound("Player not found");
            }

            var player = userPlayer.Player;

            // Check if player has enough coins
            if (player.Coins < shopItem.Price)
            {
                return BadRequest("Not enough coins");
            }

            // Check if item is in stock
            if (shopItem.Quantity <= 0)
            {
                return BadRequest("Item out of stock");
            }

            try
            {
                // Deduct coins from player
                player.Coins -= shopItem.Price;

                // Add item to player's inventory
                var playerItem = new PlayerItem
                {
                    PlayerId = player.PlayerID,
                    ItemId = shopItem.ItemID,
                    Quantity = 1
                };

                // Apply item effects if it's drinkable
                if (shopItem.Item.IsDrinkable)
                {
                    var effect = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, int>>(shopItem.Item.Effect);
                    if (effect != null)
                    {
                        if (effect.TryGetValue("withdrawal", out int withdrawalChange))
                        {
                            player.Withdrawal = Math.Max(0, Math.Min(100, player.Withdrawal + withdrawalChange));
                        }
                        if (effect.TryGetValue("health", out int healthChange))
                        {
                            player.Health = Math.Max(0, Math.Min(100, player.Health + healthChange));
                        }
                        if (effect.TryGetValue("stamina", out int staminaChange))
                        {
                            player.Stamina = Math.Max(0, Math.Min(100, player.Stamina + staminaChange));
                        }
                    }
                }

                // Decrease shop item quantity
                shopItem.Quantity--;

                _context.PlayerItems!.Add(playerItem);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = $"Successfully purchased {shopItem.Item.Name}",
                    newBalance = player.Coins,
                    newWithdrawal = player.Withdrawal,
                    newHealth = player.Health,
                    newStamina = player.Stamina
                });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the purchase");
            }
        }
    }
}