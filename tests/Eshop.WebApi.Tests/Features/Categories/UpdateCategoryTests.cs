using Eshop.Tests.Common.Mocks;
using Eshop.WebApi.Exceptions;
using Eshop.WebApi.Features.Categories;
using Snapper;
using Snapper.Attributes;

namespace Eshop.WebApi.Tests.Features.Categories
{
    public class UpdateCategoryTests : TestBase
    {
        [Test]
        // [UpdateSnapshots]
        public async Task UpdateCategory_ChangeProperties()
        {
            // arrange
            var category = dbContext.Categories.Add(CategoryMocks.GetCategory1()).Entity;
            await dbContext.SaveChangesAsync(CancellationToken.None);

            var requestDto = new UpdateCategoryRequestDto
            {
                Title = "Updated Category",
                Description = "Updated Description"
            };

            var command = new UpdateCategory.Command(category.Id, requestDto);
            var handler = new UpdateCategory.Handler(dbContext);

            // act
            var result = await handler.Handle(command, CancellationToken.None);

            // assert
            result.ShouldMatchSnapshot();
        }

        [Test]
        public async Task UpdateCategory_WithInvalidCategoryId_ThrowsNotFoundException()
        {
            // arrange
            const int invalidCategoryId = 999; // Assuming this ID does not exist in the database
            var requestDto = new UpdateCategoryRequestDto
            {
                Title = "Updated Category",
                Description = "Updated Description"
            };

            var command = new UpdateCategory.Command(invalidCategoryId, requestDto);
            var handler = new UpdateCategory.Handler(dbContext);

            // act / assert
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
