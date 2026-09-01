using Eshop.Tests.Common.Utils;

namespace Eshop.Domain.Tests
{
    public class ProductTests
    {
        [Test]
        public void Product_WithValidParams_SetsPropertiesCorrectly()
        {
            // arrange
            var id = 1;
            var title = StringUtils.GenerateRandomString(50);
            var description = StringUtils.GenerateRandomString(500);
            var price = 0.00m;
            var category = new Category(1, "Title", "Description");

            // act
            var sut = new Product(id, title, description, price, category);

            // assert
            Assert.That(sut.Id, Is.EqualTo(id));
            Assert.That(sut.Title, Is.EqualTo(title));
            Assert.That(sut.Description, Is.EqualTo(description));
            Assert.That(sut.Price, Is.EqualTo(price));
            Assert.That(sut.Category, Is.EqualTo(category));
        }

        [Test]
        public void Product_WithInvalidIdParam_ThrowsException()
        {
            // arrange
            var id = -1;

            // act / assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Product(id, null!, null!, 0, null));
        }

        [Test]
        public void Product_WithNullTitleParam_ThrowsException()
        {
            // arrange
            var id = 0;
            string title = null!;

            // act / assert
            Assert.Throws<ArgumentNullException>(() => new Product(id, title, null!, 0, null));
        }

        [Test]
        public void Product_WithEmptyTitleParam_ThrowsException()
        {
            // arrange
            var id = 0;
            var title = " ";

            // act / assert
            Assert.Throws<ArgumentNullException>(() => new Product(id, title, null!, 0, null));
        }

        [Test]
        public void Product_WithLongTitleParam_ThrowsException()
        {
            // arrange
            var id = 0;
            var title = StringUtils.GenerateRandomString(51);

            // act / assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Product(id, title, null!, 0, null));
        }

        [Test]
        public void Product_WithNullDescriptionParam_ThrowsException()
        {
            // arrange
            var id = 0;
            var title = "Valid Title";
            string description = null!;

            // act / assert
            Assert.Throws<ArgumentNullException>(() => new Product(id, title, description, 0, null));
        }

        [Test]
        public void Product_WithEmptyDescriptionParam_ThrowsException()
        {
            // arrange
            var id = 0;
            var title = "Valid Title";
            var description = " ";

            // act / assert
            Assert.Throws<ArgumentNullException>(() => new Product(id, title, description, 0, null));
        }

        [Test]
        public void Product_WithLongDescriptionParam_ThrowsException()
        {
            // arrange
            var id = 0;
            var title = "Valid Title";
            var description = StringUtils.GenerateRandomString(501);

            // act / assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Product(id, title, description, 0, null));
        }

        [Test]
        public void Product_WithInvalidPriceParam_ThrowsException()
        {
            // arrange
            var id = 0;
            var title = "Valid Title";
            var description = "Valid Description";
            var price = -0.01m;

            // act / assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Product(id, title, description, price, null));
        }

        [Test]
        public void Product_WithNullCategoryParam_SetsPropertiesCorrectly()
        {
            // arrange
            var id = 1;
            var title = "Valid Title";
            var description = "Valid Description";
            var price = 0.00m;

            // act
            var sut = new Product(id, title, description, price, null);

            // assert
            Assert.That(sut.Category, Is.Null);
        }

        [Test]
        public void ProductUpdate_WithValidParams_SetsPropertiesCorrectly()
        {
            // arrange
            var newProductTitle = StringUtils.GenerateRandomString(50);
            var newProductDescription = StringUtils.GenerateRandomString(500);
            var newProductPrice = 50.00m;
            var newProductCategory = new Category(0, "Title", "Description");
            var product = new Product(0, "Title", "Description", 0.00m, null);

            // act
            product.Update(newProductTitle, newProductDescription, newProductPrice, newProductCategory);

            // assert
            Assert.That(product.Title, Is.EqualTo(newProductTitle));
            Assert.That(product.Description, Is.EqualTo(newProductDescription));
            Assert.That(product.Price, Is.EqualTo(newProductPrice));
            Assert.That(product.Category, Is.EqualTo(newProductCategory));
        }

        [Test]
        public void ProductUpdate_WithNullTitleParam_ThrowsException()
        {
            // arrange
            string newProductTitle = null!;
            var newProductDescription = "New Description";
            var newProductPrice = 50.00m;
            var newProductCategory = new Category(0, "Title", "Description");
            var product = new Product(0, "Title", "Description", 0.00m, null);

            // act / assert
            Assert.Throws<ArgumentNullException>(() => product.Update(newProductTitle, newProductDescription, newProductPrice, newProductCategory));
        }

        [Test]
        public void ProductUpdate_WithEmptyTitleParam_ThrowsException()
        {
            // arrange
            var newProductTitle = " ";
            var newProductDescription = "New Description";
            var newProductPrice = 50.00m;
            var newProductCategory = new Category(0, "Title", "Description");
            var product = new Product(0, "Title", "Description", 0.00m, null);

            // act / assert
            Assert.Throws<ArgumentNullException>(() => product.Update(newProductTitle, newProductDescription, newProductPrice, newProductCategory));
        }

        [Test]
        public void ProductUpdate_WithLongTitleParam_ThrowsException()
        {
            // arrange
            var newProductTitle = StringUtils.GenerateRandomString(51);
            var newProductDescription = "New Description";
            var newProductPrice = 50.00m;
            var newProductCategory = new Category(0, "Title", "Description");
            var product = new Product(0, "Title", "Description", 0.00m, null);

            // act / assert
            Assert.Throws<ArgumentOutOfRangeException>(() => product.Update(newProductTitle, newProductDescription, newProductPrice, newProductCategory));
        }

        [Test]
        public void ProductUpdate_WithNullDescriptionParam_ThrowsException()
        {
            // arrange
            var newProductTitle = "New Title";
            string newProductDescription = null!;
            var newProductPrice = 50.00m;
            var newProductCategory = new Category(0, "Title", "Description");
            var product = new Product(0, "Title", "Description", 0.00m, null);

            // act / assert
            Assert.Throws<ArgumentNullException>(() => product.Update(newProductTitle, newProductDescription, newProductPrice, newProductCategory));
        }

        [Test]
        public void ProductUpdate_WithEmptyDescriptionParam_ThrowsException()
        {
            // arrange
            var newProductTitle = "New Title";
            var newProductDescription = " ";
            var newProductPrice = 50.00m;
            var newProductCategory = new Category(0, "Title", "Description");
            var product = new Product(0, "Title", "Description", 0.00m, null);

            // act / assert
            Assert.Throws<ArgumentNullException>(() => product.Update(newProductTitle, newProductDescription, newProductPrice, newProductCategory));
        }

        [Test]
        public void ProductUpdate_WithLongDescriptionParam_ThrowsException()
        {
            // arrange
            var newProductTitle = "New Title";
            var newProductDescription = StringUtils.GenerateRandomString(501);
            var newProductPrice = 50.00m;
            var newProductCategory = new Category(0, "Title", "Description");
            var product = new Product(0, "Title", "Description", 0.00m, null);

            // act / assert
            Assert.Throws<ArgumentOutOfRangeException>(() => product.Update(newProductTitle, newProductDescription, newProductPrice, newProductCategory));
        }

        [Test]
        public void ProductUpdate_WithInvalidPriceParam_ThrowsException()
        {
            // arrange
            var newProductTitle = "New Title";
            var newProductDescription = "New Description";
            var newProductPrice = -0.01m;
            var newProductCategory = new Category(0, "Title", "Description");
            var product = new Product(0, "Title", "Description", 0.00m, null);

            // act / assert
            Assert.Throws<ArgumentOutOfRangeException>(() => product.Update(newProductTitle, newProductDescription, newProductPrice, newProductCategory));
        }

        [Test]
        public void ProductUpdate_WithNullCategoryParam_SetsPropertiesCorrectly()
        {
            // arrange
            var newProductTitle = "New Title";
            var newProductDescription = "New Description";
            var newProductPrice = 50.00m;
            var oldProductCategory = new Category(0, "Title", "Description");
            var product = new Product(0, "Title", "Description", 0.00m, oldProductCategory);

            // act
            product.Update(newProductTitle, newProductDescription, newProductPrice, null);

            // assert
            Assert.That(product.Category, Is.Null);
        }
    }
}
