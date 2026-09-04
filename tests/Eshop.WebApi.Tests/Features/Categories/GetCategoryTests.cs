using Eshop.Tests.Common.Mocks;
using Eshop.WebApi.Exceptions;
using Eshop.WebApi.Features.Categories;
using Snapper;
using Snapper.Attributes;

namespace Eshop.WebApi.Tests.Features.Categories
{
    public class GetCategoryTests : TestBase
    {
        [Test]
        // [UpdateSnapshots]
        public async Task GetCategory_ReturnsCorrectDto()
        {
            // arrange
            var category = CategoryMocks.GetCategory1();
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync(CancellationToken.None);

            var query = new GetCategory.Query(category.Id);
            var handler = new GetCategory.Handler(dbContext);

            // act
            var result = await handler.Handle(query, CancellationToken.None);

            // assert
            result.ShouldMatchSnapshot();
        }

        [Test]
        public async Task GetCategory_WithInvalidCategoryId_ThrowsNotFoundException()
        {
            // arrange
            const int invalidCategoryId = 999; // Assuming this ID does not exist in the database
            var query = new GetCategory.Query(invalidCategoryId);
            var handler = new GetCategory.Handler(dbContext);

            // act / assert
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
        }
    }
}
