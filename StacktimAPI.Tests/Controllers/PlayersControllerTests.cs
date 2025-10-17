using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StacktimAPI.Tests.Helpers;
using StacktimAPI_Chloe.Controllers;
using StacktimAPI_Chloe.Data;
using StacktimAPI_Chloe.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StacktimAPI.Tests.Controllers
{
    public class PlayersControllerTests
    {
        private readonly StacktimDbContext _context;
        private readonly PlayersController _controller;

        public PlayersControllerTests()
        {
            _context = TestDbContextFactory.Create();
            _controller = new PlayersController(_context);
        }

        [Fact]
        public void GetPlayers_ReturnsAllPlayers()
        {
            var result = _controller.GetPlayers() as OkObjectResult;

            result.Should().NotBeNull();
            var players = result.Value as IEnumerable<PlayerDto>;
            players.Should().HaveCount(3);
        }

        [Fact]
        public void GetPlayer_WithValidId_ReturnsPlayer()
        {
            var result = _controller.GetPlayer(1) as OkObjectResult;

            result.Should().NotBeNull();
            var player = result.Value as PlayerDto;
            player.Pseudo.Should().Be("test_player1");
        }

        [Fact]
        public void GetPlayer_WithInvalidId_ReturnsNotFound()
        {
            var result = _controller.GetPlayer(4);

            result.Should().BeOfType<NotFoundResult>();
        }

    }
}
