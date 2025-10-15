using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StacktimAPI_Chloe.Data;
using StacktimAPI_Chloe.DTOs;

namespace StacktimAPI_Chloe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly StacktimDbContext _context;
        public PlayersController(StacktimDbContext dbContext) 
        {
            _context = dbContext;
        }

        [HttpGet]
        public IActionResult GetPlayers()
        {
            var players = _context.Players.ToList();

            var playersDto = new List<PlayerDto>();

            foreach (var player in players)
            {
                PlayerDto playerDto = new PlayerDto
                {
                    Id = player.Id,
                    Pseudo = player.Pseudo,
                    Email = player.Email,
                    Rank = player.Rank,
                    TotalScore = player.TotalScore
                };

                playersDto.Add(playerDto);
            }

            return Ok(playersDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayer(int id)
        {
            var player = _context.Players.Find(id);
            PlayerDto playerDto = new PlayerDto
            {
                Id = player.Id,
                Pseudo = player.Pseudo,
                Email = player.Email,
                Rank = player.Rank,
                TotalScore = player.TotalScore
            };

            return Ok(playerDto);
        }
    }
}
