using Eshop.Tests.Common.Mocks;
using Eshop.WebApi.Features.Categories;
using Snapper;
using Snapper.Attributes;

namespace Eshop.WebApi.Tests.Features.Categories
{
    public class GetCategoriesTests : TestBase
    {
        [Test]
        // [UpdateSnapshots]
        public async Task GetCategories_ReturnsCorrectDto()
        {
            // arrange
            var category1 = CategoryMocks.GetCategory1();
            var category2 = CategoryMocks.GetCategory2();
            dbContext.Categories.AddRange(category1, category2);
            await dbContext.SaveChangesAsync(CancellationToken.None);


            var query = new GetCategories.Query();
            var handler = new GetCategories.Handler(dbContext);

            // act
            var result = await handler.Handle(query, CancellationToken.None);

            // assert
            result.ShouldMatchSnapshot();
        }
    }
}
