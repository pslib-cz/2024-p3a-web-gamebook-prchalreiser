using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameBookASP.Data;
using GameBookASP.GameModels;

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

        [HttpGet("location/{locationId}")]
        public async Task<ActionResult<Minigame>> GetMinigameByLocation(int locationId)
        {
            var minigame = await _context.Minigames!
                .FirstOrDefaultAsync(m => m.LocationID == locationId);

            if (minigame == null)
            {
                return NotFound();
            }

            return minigame;
        }

        [HttpPost("rps/play")]
        public async Task<ActionResult<object>> PlayRPS([FromBody] PlayRPSRequest request)
        {
            var minigame = await _context.Minigames!
                .FirstOrDefaultAsync(m => m.MinigameID == request.MinigameID);

            if (minigame == null)
            {
                return NotFound();
            }

            var computerChoice = GetRandomChoice();
            var result = DetermineWinner(request.PlayerChoice, computerChoice);

            if (result == "win")
            {
                minigame.PlayerScore++;
            }
            else if (result == "lose")
            {
                minigame.ComputerScore++;
            }

            if (minigame.PlayerScore >= 3 || minigame.ComputerScore >= 3)
            {
                minigame.IsCompleted = true;
            }

            await _context.SaveChangesAsync();

            return new
            {
                PlayerChoice = request.PlayerChoice,
                ComputerChoice = computerChoice,
                Result = result,
                PlayerScore = minigame.PlayerScore,
                ComputerScore = minigame.ComputerScore,
                IsCompleted = minigame.IsCompleted
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
    }

    public class PlayRPSRequest
    {
        public Guid MinigameID { get; set; }
        public string PlayerChoice { get; set; } = string.Empty;
    }
} 