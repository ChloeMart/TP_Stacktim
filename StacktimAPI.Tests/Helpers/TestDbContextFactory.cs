using Microsoft.EntityFrameworkCore;
using StacktimAPI_Chloe.Data;
using StacktimAPI_Chloe.Models;

namespace StacktimAPI.Tests.Helpers
{
    public static class TestDbContextFactory
    {
        public static StacktimDbContext Create()
        {
            var options = new DbContextOptionsBuilder<StacktimDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new StacktimDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Players.AddRange(
                new Player { Id = 1, Pseudo = "test_player1", Email = "test1@example.com", Rank = "Gold", TotalScore = 1500 },
                new Player { Id = 2, Pseudo = "test_player2", Email = "test2@example.com", Rank = "Platinum", TotalScore = 2300 },
                new Player { Id = 3, Pseudo = "test_player3", Email = "test3@example.com", Rank = "Silver", TotalScore = 900 }
            );

            context.SaveChanges();
            return context;
        }
    }
}
