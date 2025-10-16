using Microsoft.EntityFrameworkCore;
using StacktimAPI_Chloe.Data;
using StacktimAPI_Chloe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StacktimAPI.Tests.Helpers
{
    public static class TestDbContextFactory
    {
        public static StacktimDbContext Create()
        {
            var options = new DbContextOptionsBuilder<StacktimDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new StacktimDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Players.AddRange(
                new Player { Id = 1, Pseudo = "TestPlayer1", Email = "tp1@test.com", Rank = "Gold", TotalScore = 1000 },
                new Player { Id = 2, Pseudo = "TestPlayer2", Email = "tp2@test.com", Rank = "Diamond", TotalScore = 2500 },
                new Player { Id = 3, Pseudo = "TestPlayer3", Email = "tp3@test.com", Rank = "Bronze", TotalScore = 450 }
            );

            context.SaveChanges();

            return context;
        }
    }
}
