using Eshop.Tests.Common.Mocks;
using Eshop.WebApi.Exceptions;
using Eshop.WebApi.Features.Products;
using Snapper;
using Snapper.Attributes;

namespace Eshop.WebApi.Tests.Features.Products
{
    public class GetProductTests : TestBase
    {
        [Test]
        // [UpdateSnapshots]
        public async Task GetProduct_ReturnsCorrectDto()
        {
            // arrange
            var product = ProductMocks.GetProduct1();
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync(CancellationToken.None);

            var query = new GetProduct.Query(product.Id);
            var handler = new GetProduct.Handler(dbContext);

            // act
            var result = await handler.Handle(query, CancellationToken.None);

            // assert
            result.ShouldMatchSnapshot();
        }

        [Test]
        public async Task GetProduct_WithInvalidProductId_ThrowsNotFoundException()
        {
            // arrange
            const int invalidProductId = 999; // Assuming this ID does not exist in the database
            var query = new GetProduct.Query(invalidProductId);
            var handler = new GetProduct.Handler(dbContext);

            // act / assert
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
        }
    }
}
