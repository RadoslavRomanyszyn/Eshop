using Eshop.WebApi.Features.Categories;
using Snapper;
using Snapper.Attributes;

namespace Eshop.WebApi.Tests.Features.Categories
{
    public class AddCategoryTests : TestBase
    {
        [Test]
        // [UpdateSnapshots]
        public async Task AddCategory_ReturnsCorrectDto()
        {
            // arrange
            var requestDto = new AddCategoryRequestDto
            {
                Title = "New Category",
                Description = "This is a new category"
            };

            var command = new AddCategory.Command(requestDto);
            var handler = new AddCategory.Handler(dbContext);

            // act
            var result = await handler.Handle(command, CancellationToken.None);
            
            // assert
            result.ShouldMatchSnapshot();
        }
    }
}
