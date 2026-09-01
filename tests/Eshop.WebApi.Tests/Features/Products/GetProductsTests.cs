using Eshop.Tests.Common.Mocks;
using Eshop.WebApi.Features.Products;
using Snapper;
using Snapper.Attributes;

namespace Eshop.WebApi.Tests.Features.Products
{
    public class GetProductsTests : TestBase
    {
        [Test]
        // [UpdateSnapshots]
        public async Task GetProducts_ReturnsCorrectDto()
        {
            // arrange
            var product1 = ProductMocks.GetProduct1();
            var product2 = ProductMocks.GetProduct2();
            dbContext.Products.AddRange(product1, product2);
            await dbContext.SaveChangesAsync(CancellationToken.None);

            var query = new GetProducts.Query();
            var handler = new GetProducts.Handler(dbContext);

            // act
            var result = await handler.Handle(query, CancellationToken.None);

            // assert
            result.ShouldMatchSnapshot();
        }
    }
}
