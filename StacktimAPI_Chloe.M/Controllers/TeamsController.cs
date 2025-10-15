using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StacktimAPI_Chloe.Data;
using StacktimAPI_Chloe.DTOs;
using StacktimAPI_Chloe.Models;

namespace StacktimAPI_Chloe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly StacktimDbContext _context;
        public TeamsController(StacktimDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public IActionResult GetTeams()
        {
            var teams = _context.Teams.ToList();

            if (teams != null)
            {
                var teamsDto = new List<TeamDto>();

                foreach (var team in teams)
                {
                    TeamDto teamDto = new TeamDto
                    {
                        Id = team.Id,
                        Name = team.Name,
                        Tag = team.Tag,
                        CaptainId = team.CaptainId
                    };

                    teamsDto.Add(teamDto);
                }

                return Ok(teamsDto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTeam(int id)
        {
            var team = _context.Teams.Find(id);

            if (team != null)
            {
                TeamDto teamDto = new TeamDto
                {
                    Id = team.Id,
                    Name = team.Name,
                    Tag = team.Tag,
                    CaptainId = team.CaptainId
                };

                return Ok(teamDto);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
