using Eshop.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.Tests
{
    public abstract class TestBase
    {
        protected EshopDbContext dbContext;
        protected DbContextOptions<EshopDbContext> dbContextOptions;
        
        [SetUp]
        public async Task SetUp()
        {
            dbContextOptions = new DbContextOptionsBuilder<EshopDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            dbContext = new EshopDbContext(dbContextOptions);

            await dbContext.Database.OpenConnectionAsync();
            await dbContext.Database.EnsureCreatedAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.CloseConnectionAsync();
            await dbContext.DisposeAsync();
        }
    }
}