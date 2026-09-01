using Eshop.Tests.Common.Mocks;
using Eshop.WebApi.Exceptions;
using Eshop.WebApi.Features.Products;
using Snapper;
using Snapper.Attributes;

namespace Eshop.WebApi.Tests.Features.Products
{
    public class UpdateProductTests : TestBase
    {
        [Test]
        // [UpdateSnapshots]
        public async Task UpdateProduct_ChangesProperties()
        {
            // arrange
            var newCategory = dbContext.Categories.Add(CategoryMocks.GetCategory2()).Entity;
            var product = dbContext.Products.Add(ProductMocks.GetProduct1()).Entity;
            await dbContext.SaveChangesAsync(CancellationToken.None);

            var requestDto = new UpdateProductRequestDto
            {
                Title = "Updated Product",
                Description = "Updated Description",
                Price = 200,
                CategoryId = newCategory.Id
            };

            var command = new UpdateProduct.Command(product.Id, requestDto);
            var handler = new UpdateProduct.Handler(dbContext);

            // act
            var result = await handler.Handle(command, CancellationToken.None);

            // assert
            result.ShouldMatchSnapshot();
        }

        [Test]
        public async Task UpdateProduct_WithInvalidProductId_ThrowsNotFoundException()
        {
            // arrange
            const int invalidProductId = 999; // Assuming this ID does not exist in the database
            var requestDto = new UpdateProductRequestDto
            {
                Title = "Updated Product",
                Description = "Updated Description",
                Price = 200
            };

            var command = new UpdateProduct.Command(invalidProductId, requestDto);
            var handler = new UpdateProduct.Handler(dbContext);

            // act / assert
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task UpdateProduct_WithInvalidCategoryId_ThrowsNotFoundException()
        {
            // arrange
            var product = dbContext.Products.Add(ProductMocks.GetProduct1()).Entity;
            await dbContext.SaveChangesAsync(CancellationToken.None);

            const int invalidCategoryId = 999; // Assuming this ID does not exist in the database
            var requestDto = new UpdateProductRequestDto
            {
                Title = "Updated Product",
                Description = "Updated Description",
                Price = 200,
                CategoryId = invalidCategoryId
            };

            var command = new UpdateProduct.Command(product.Id, requestDto);
            var handler = new UpdateProduct.Handler(dbContext);

            // act / assert
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
