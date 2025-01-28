using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameBookASP.Data;
using GameBookASP.GameModels;
using System.Security.Claims;

namespace MDAGameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MinigamesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MinigamesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{locationId}")]
        public async Task<ActionResult<object>> GetMinigameByLocation(int locationId)
        {
            var minigame = await _context.Minigames!
                .FirstOrDefaultAsync(m => m.LocationID == locationId);

            if (minigame == null)
            {
                return NotFound();
            }

            return new
            {
                minigame.MinigameID,
                minigame.LocationID,
                minigame.Description,
                minigame.Type,
                minigame.OpponentName,
                minigame.WinLocationID,
                minigame.LoseLocationID
            };
        }

        [HttpGet("play/{locationId}")]
        public async Task<ActionResult<object>> GetPlayerMinigame(int locationId)
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

            var minigame = await _context.Minigames!
                .FirstOrDefaultAsync(m => m.LocationID == locationId);

            if (minigame == null)
            {
                return NotFound();
            }

            var playerMinigame = await _context.PlayerMinigames!
                .FirstOrDefaultAsync(pm =>
                    pm.PlayerID == userPlayer.Player.PlayerID &&
                    pm.MinigameID == minigame.MinigameID);

            if (playerMinigame == null)
            {
                playerMinigame = new PlayerMinigame
                {
                    PlayerMinigameID = Guid.NewGuid(),
                    PlayerID = userPlayer.Player.PlayerID,
                    MinigameID = minigame.MinigameID,
                    IsCompleted = false,
                    PlayerScore = 0,
                    ComputerScore = 0
                };
                _context.PlayerMinigames!.Add(playerMinigame);
                await _context.SaveChangesAsync();
            }

            return new
            {
                minigame.MinigameID,
                minigame.LocationID,
                minigame.Description,
                minigame.Type,
                minigame.OpponentName,
                minigame.WinLocationID,
                minigame.LoseLocationID,
                playerMinigame.IsCompleted,
                playerMinigame.PlayerScore,
                playerMinigame.ComputerScore
            };
        }

        [HttpPost("{minigameId}/play")]
        public async Task<ActionResult<object>> PlayGame(Guid minigameId, [FromBody] PlayRPSRequest request)
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

            var playerMinigame = await _context.PlayerMinigames!
                .FirstOrDefaultAsync(pm =>
                    pm.PlayerID == userPlayer.Player.PlayerID &&
                    pm.MinigameID == minigameId);

            if (playerMinigame == null)
            {
                return NotFound("Game session not found");
            }

            if (playerMinigame.IsCompleted)
            {
                return BadRequest("Game is already completed");
            }

            // RPS Game Logic
            var computerChoice = GetRandomChoice();
            var result = DetermineWinner(request.PlayerChoice, computerChoice);

            if (result == "win")
            {
                playerMinigame.PlayerScore++;
            }
            else if (result == "lose")
            {
                playerMinigame.ComputerScore++;
            }

            if (playerMinigame.PlayerScore >= 3 || playerMinigame.ComputerScore >= 3)
            {
                playerMinigame.IsCompleted = true;
            }

            await _context.SaveChangesAsync();

            return new
            {
                PlayerChoice = request.PlayerChoice,
                ComputerChoice = computerChoice,
                Result = result,
                PlayerScore = playerMinigame.PlayerScore,
                ComputerScore = playerMinigame.ComputerScore,
                IsCompleted = playerMinigame.IsCompleted
            };
        }

        [HttpPost("{minigameId}/play-numbers")]
        public async Task<ActionResult<object>> PlayNumberGame(Guid minigameId, [FromBody] PlayNumbersRequest request)
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

            var minigame = await _context.Minigames!
                .FirstOrDefaultAsync(m => m.MinigameID == minigameId);

            if (minigame == null)
            {
                return NotFound("Game not found");
            }

            var playerMinigame = await _context.PlayerMinigames!
                .FirstOrDefaultAsync(pm =>
                    pm.PlayerID == userPlayer.Player.PlayerID &&
                    pm.MinigameID == minigameId);

            if (playerMinigame == null)
            {
                playerMinigame = new PlayerMinigame
                {
                    PlayerMinigameID = Guid.NewGuid(),
                    PlayerID = userPlayer.Player.PlayerID,
                    MinigameID = minigameId,
                    IsCompleted = false,
                    PlayerScore = 0,
                    ComputerScore = 0
                };
                _context.PlayerMinigames!.Add(playerMinigame);
            }

            bool isCorrect = (request.Number1.ToString() == minigame.Number1 &&
                             request.Number2.ToString() == minigame.Number2) ||
                            (request.Number1.ToString() == minigame.Number2 &&
                             request.Number2.ToString() == minigame.Number1);

            playerMinigame.IsCompleted = true;
            playerMinigame.PlayerScore = isCorrect ? 1 : 0;

            await _context.SaveChangesAsync();

            return new
            {
                IsCorrect = isCorrect,
                IsCompleted = true
            };
        }

        [HttpPost("{locationId}")]
        public async Task<ActionResult<object>> CreateOrUpdateMinigame(int locationId, [FromBody] MinigameUpdateRequest request)
        {
            var minigame = await _context.Minigames!
                .FirstOrDefaultAsync(m => m.LocationID == locationId);

            if (minigame == null)
            {
                minigame = new Minigame
                {
                    MinigameID = Guid.NewGuid(),
                    LocationID = locationId,
                    Type = request.Type
                };
                _context.Minigames!.Add(minigame);
            }

            minigame.Description = request.Description;
            minigame.OpponentName = request.OpponentName;
            minigame.WinLocationID = request.WinLocationID;
            minigame.LoseLocationID = request.LoseLocationID;
            minigame.Type = request.Type;
            minigame.Number1 = request.Number1;
            minigame.Number2 = request.Number2;

            await _context.SaveChangesAsync();

            return new
            {
                minigame.MinigameID,
                minigame.LocationID,
                minigame.Description,
                minigame.Type,
                minigame.OpponentName,
                minigame.WinLocationID,
                minigame.LoseLocationID,
                minigame.Number1,
                minigame.Number2
            };
        }

        private string GetRandomChoice()
        {
            string[] choices = { "rock", "paper", "scissors" };
            return choices[new Random().Next(choices.Length)];
        }

        private string DetermineWinner(string playerChoice, string computerChoice)
        {
            if (playerChoice == computerChoice) return "tie";

            if ((playerChoice == "rock" && computerChoice == "scissors") ||
                (playerChoice == "paper" && computerChoice == "rock") ||
                (playerChoice == "scissors" && computerChoice == "paper"))
            {
                return "win";
            }

            return "lose";
        }

        public class PlayRPSRequest
        {
            public string PlayerChoice { get; set; } = string.Empty;
        }

        public class MinigameUpdateRequest
        {
            public string Description { get; set; } = string.Empty;
            public string OpponentName { get; set; } = string.Empty;
            public int WinLocationID { get; set; }
            public int LoseLocationID { get; set; }
            public string Type { get; set; } = "RPS";
            public string? Number1 { get; set; }
            public string? Number2 { get; set; }
        }

        public class PlayNumbersRequest
        {
            public int Number1 { get; set; }
            public int Number2 { get; set; }
        }
    }
}