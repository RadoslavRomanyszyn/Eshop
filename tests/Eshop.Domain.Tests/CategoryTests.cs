using Eshop.Domain.Tests.Utils;

namespace Eshop.Domain.Tests
{
    public class CategoryTests
    {
        [Test]
        public void Category_WithValidParams_SetsPropertiesCorrectly()
        {
            // arrange
            var id = 1;
            var title = StringUtils.GenerateRandomString(50);
            var description = StringUtils.GenerateRandomString(500);

            // act
            var sut = new Category(id, title, description);

            // assert
            Assert.That(sut.Id, Is.EqualTo(id));
            Assert.That(sut.Title, Is.EqualTo(title));
            Assert.That(sut.Description, Is.EqualTo(description));
        }

        [Test]
        public void Category_WithInvalidIdParam_ThrowsException()
        {
            // arrange
            var id = -1;

            // act / assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Category(id, null!, null!));
        }

        [Test]
        public void Category_WithNullTitleParam_ThrowsException()
        {
            // arrange
            var id = 0;
            string title = null!;

            // act / assert
            Assert.Throws<ArgumentNullException>(() => new Category(id, title, null!));
        }

        [Test]
        public void Category_WithEmptyTitleParam_ThrowsException()
        {
            // arrange
            var id = 0;
            var title = " ";

            // act / assert
            Assert.Throws<ArgumentNullException>(() => new Category(id, title, null!));
        }

        [Test]
        public void Category_WithLongTitleParam_ThrowsException()
        {
            // arrange
            var id = 0;
            var title = StringUtils.GenerateRandomString(51);

            // act / assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Category(id, title, null!));
        }

        [Test]
        public void Category_WithNullDescriptionParam_ThrowsException()
        {
            // arrange
            var id = 0;
            var title = "Valid Title";
            string description = null!;

            // act / assert
            Assert.Throws<ArgumentNullException>(() => new Category(id, title, description));
        }

        [Test]
        public void Category_WithEmptyDescriptionParam_ThrowsException()
        {
            // arrange
            var id = 0;
            var title = "Valid Title";
            var description = " ";

            // act / assert
            Assert.Throws<ArgumentNullException>(() => new Category(id, title, description));
        }

        [Test]
        public void Category_WithLongDescriptionParam_ThrowsException()
        {
            // arrange
            var id = 0;
            var title = "Valid Title";
            var description = StringUtils.GenerateRandomString(501);

            // act / assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Category(id, title, description));
        }

        [Test]
        public void CategoryUpdate_WithValidParams_SetsPropertiesCorrectly()
        {
            // arrange
            var newCategoryTitle = StringUtils.GenerateRandomString(50);
            var newCategoryDescription = StringUtils.GenerateRandomString(500);
            var category = new Category(0, "Laptop", "Lorem ipsum");

            // act
            category.Update(newCategoryTitle, newCategoryDescription);

            // assert
            Assert.That(category.Title, Is.EqualTo(newCategoryTitle));
            Assert.That(category.Description, Is.EqualTo(newCategoryDescription));
        }

        [Test]
        public void CategoryUpdate_WithNullTitleParam_ThrowsException()
        {
            // arrange
            string newCategoryTitle = null!;
            var newCategoryDescription = "New Description";
            var category = new Category(0, "Laptop", "Lorem ipsum");

            // act / assert
            Assert.Throws<ArgumentNullException>(() => category.Update(newCategoryTitle, newCategoryDescription));
        }

        [Test]
        public void CategoryUpdate_WithEmptyTitleParam_ThrowsException()
        {
            // arrange
            var newCategoryTitle = " ";
            var newCategoryDescription = "New Description";
            var category = new Category(0, "Laptop", "Lorem ipsum");

            // act / assert
            Assert.Throws<ArgumentNullException>(() => category.Update(newCategoryTitle, newCategoryDescription));
        }

        [Test]
        public void CategoryUpdate_WithLongTitleParam_ThrowsException()
        {
            // arrange
            var newCategoryTitle = StringUtils.GenerateRandomString(51);
            var newCategoryDescription = "New Description";
            var category = new Category(0, "Laptop", "Lorem ipsum");

            // act / assert
            Assert.Throws<ArgumentOutOfRangeException>(() => category.Update(newCategoryTitle, newCategoryDescription));
        }

        [Test]
        public void CategoryUpdate_WithNullDescriptionParam_ThrowsException()
        {
            // arrange
            var newCategoryTitle = "New Title";
            string newCategoryDescription = null!;
            var category = new Category(0, "Laptop", "Lorem ipsum");

            // act / assert
            Assert.Throws<ArgumentNullException>(() => category.Update(newCategoryTitle, newCategoryDescription));
        }

        [Test]
        public void CategoryUpdate_WithEmptyDescriptionParam_ThrowsException()
        {
            // arrange
            var newCategoryTitle = "New Title";
            var newCategoryDescription = " ";
            var category = new Category(0, "Laptop", "Lorem ipsum");

            // act / assert
            Assert.Throws<ArgumentNullException>(() => category.Update(newCategoryTitle, newCategoryDescription));
        }

        [Test]
        public void CategoryUpdate_WithLongDescriptionParam_ThrowsException()
        {
            // arrange
            var newCategoryTitle = "New Title";
            var newCategoryDescription = StringUtils.GenerateRandomString(501);
            var category = new Category(0, "Laptop", "Lorem ipsum");

            // act / assert
            Assert.Throws<ArgumentOutOfRangeException>(() => category.Update(newCategoryTitle, newCategoryDescription));
        }
    }
}
