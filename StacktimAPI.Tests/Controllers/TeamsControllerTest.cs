using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using StacktimAPI.Tests.Helpers;
using StacktimAPI_Chloe.Controllers;
using StacktimAPI_Chloe.Data;
using StacktimAPI_Chloe.DTOs;
using StacktimAPI_Chloe.Models;

namespace StacktimAPI.Tests.Controllers
{
    public class TeamsControllerTests
    {
        private readonly StacktimDbContext _context;
        private readonly TeamsController _controller;

        public TeamsControllerTests()
        {
            _context = TestDbContextFactory.Create();
            _controller = new TeamsController(_context);
        }

        [Fact]
        public void GetTeams_ReturnsAllTeams()
        {
            var result = _controller.GetTeams() as OkObjectResult;

            result.Should().NotBeNull();
            var teams = result.Value as IEnumerable<TeamDto>;
            teams.Should().HaveCount(3);
        }

        [Fact]
        public void GetTeam_ExistingId_ReturnsOk()
        {
            var result = _controller.GetTeam(1) as OkObjectResult;

            result.Should().NotBeNull();
            var team = result.Value as TeamDto;
            team.Name.Should().Be("test_team1");
        }

        [Fact]
        public void GetTeam_InvalidId_ReturnsNotFound()
        {
            var result = _controller.GetTeam(4);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void CreateTeam_ValidData_ReturnsCreatedTeam()
        {
            var dto = new CreateTeamDto
            {
                Name = "test_team4",
                Tag = "TTD",
                CaptainId = 1
            };

            var result = _controller.CreateTeam(dto) as CreatedAtActionResult;

            result.Should().NotBeNull();
            var created = result.Value as TeamDto;
            created.Name.Should().Be("test_team4");
            _context.Teams.Should().Contain(t => t.Name == "test_team4");
        }

        [Fact]
        public void CreateTeam_DuplicateName_ReturnsBadRequest()
        {
            var dto = new CreateTeamDto
            {
                Name = "test_team1",
                Tag = "TTA",
                CaptainId = 1
            };

            var result = _controller.CreateTeam(dto) as BadRequestObjectResult;

            result.Should().NotBeNull();
        }

        [Fact]
        public void GetTeamPlayers_ExistingTeam_ReturnsRoster()
        {
            _context.TeamPlayers.AddRange(
                new TeamPlayer { TeamId = 1, PlayerId = 1 },
                new TeamPlayer { TeamId = 1, PlayerId = 2 }
            );
            _context.SaveChanges();

            var result = _controller.GetTeamPlayers(1) as OkObjectResult;

            result.Should().NotBeNull();
            var roster = result.Value as IEnumerable<object>;
            roster.Should().HaveCount(2);
        }

        [Fact]
        public void GetTeamPlayers_InvalidTeam_ReturnsNotFound()
        {
            var result = _controller.GetTeamPlayers(4);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
