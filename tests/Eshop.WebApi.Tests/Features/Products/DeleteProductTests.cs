using Eshop.Tests.Common.Mocks;
using Eshop.WebApi.Exceptions;
using Eshop.WebApi.Features.Products;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.Tests.Features.Products
{
    public class DeleteProductTests : TestBase
    {
        [Test]
        public async Task DeleteProduct_RemovesProductFromDb()
        {
            // arrange
            var product = dbContext.Products.Add(ProductMocks.GetProduct1()).Entity;
            await dbContext.SaveChangesAsync(CancellationToken.None);

            var command = new DeleteProduct.Command(product.Id);
            var handler = new DeleteProduct.Handler(dbContext);

            // act
            await handler.Handle(command, CancellationToken.None);

            // assert
            var remainingProducts = await dbContext.Products.ToListAsync(CancellationToken.None);
            Assert.That(remainingProducts, Is.Empty);
        }

        [Test]
        public async Task DeleteProduct_WithInvalidProductId_ThrowsNotFoundException()
        {
            // arrange
            const int invalidProductId = 999; // Assuming this ID does not exist in the database
            var command = new DeleteProduct.Command(invalidProductId);
            var handler = new DeleteProduct.Handler(dbContext);

            // act / assert
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
