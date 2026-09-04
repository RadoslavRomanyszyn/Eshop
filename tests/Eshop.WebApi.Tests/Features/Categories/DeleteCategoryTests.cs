using Eshop.Tests.Common.Mocks;
using Eshop.WebApi.Exceptions;
using Eshop.WebApi.Features.Categories;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.Tests.Features.Categories
{
    public class DeleteCategoryTests : TestBase
    {
        [Test]
        public async Task DeleteCategory_RemovesCategoryFromDb()
        {
            // arrange
            var category = dbContext.Categories.Add(CategoryMocks.GetCategory1()).Entity;
            await dbContext.SaveChangesAsync(CancellationToken.None);

            var command = new DeleteCategory.Command(category.Id);
            var handler = new DeleteCategory.Handler(dbContext);

            // act
            await handler.Handle(command, CancellationToken.None);

            // assert
            var remainingCategories = await dbContext.Categories.ToListAsync(CancellationToken.None);
            Assert.That(remainingCategories, Is.Empty);
        }

        [Test]
        public async Task DeleteCategory_WithInvalidCategoryId_ThrowsNotFoundException()
        {
            // arrange
            const int invalidCategoryId = 999; // Assuming this ID does not exist in the database
            var command = new DeleteCategory.Command(invalidCategoryId);
            var handler = new DeleteCategory.Handler(dbContext);

            // act / assert
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
