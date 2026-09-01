using Eshop.Tests.Common.Mocks;
using Eshop.WebApi.Exceptions;
using Eshop.WebApi.Features.Products;
using Snapper;
using Snapper.Attributes;

namespace Eshop.WebApi.Tests.Features.Products
{
    public class AddProductTests : TestBase
    {
        [Test]
        // [UpdateSnapshots]
        public async Task AddProduct_ReturnsCorrectDto()
        {
            // arrange
            var category = CategoryMocks.GetCategory1();
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync(CancellationToken.None);

            var requestDto = new AddProductRequestDto
            {
                Title = "New Product",
                Description = "This is a new product",
                Price = 19.99m,
                CategoryId = category.Id
            };

            var command = new AddProduct.Command(requestDto);
            var handler = new AddProduct.Handler(dbContext);

            // act
            var result = await handler.Handle(command, CancellationToken.None);

            // assert
            result.ShouldMatchSnapshot();
        }

        [Test]
        public async Task AddProduct_WithInvalidCategoryId_ThrowsNotFoundException()
        {
            // arrange
            const int invalidCategoryId = 999; // Assuming this ID does not exist in the database
            var command = new AddProduct.Command(new AddProductRequestDto
            {
                Title = "New Product",
                Description = "This is a new product",
                Price = 19.99m,
                CategoryId = invalidCategoryId
            });

            var handler = new AddProduct.Handler(dbContext);

            // act / assert
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
