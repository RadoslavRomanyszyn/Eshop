using Eshop.Tests.Common.Mocks;

namespace Eshop.Domain.Tests
{
    [TestFixture]
    public class OrderItemTests
    {
        [Test]
        public void OrderItem_WithValidParams_SetsPropertiesCorrectly()
        {
            // arrange
            var quantity = 2;
            var price = 1.23m;
            var product = ProductMocks.GetProduct1();

            // act
            var sut = new OrderItem(quantity, price, product);

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(sut.Quantity, Is.EqualTo(quantity));
                Assert.That(sut.Price, Is.EqualTo(price));
                Assert.That(sut.Product, Is.EqualTo(product));
            });
        }

        // Quantity parameter tests
        [TestCase(-1, 1, 1, typeof(ArgumentOutOfRangeException))]
        [TestCase(0, 1, 1, typeof(ArgumentOutOfRangeException))]
        // Price parameter tests
        [TestCase(1, -1, 1, typeof(ArgumentOutOfRangeException))]
        // Product parameter tests
        [TestCase(1, 0, 0, typeof(ArgumentNullException))]
        public void OrderItem_WithInvalidParams_ThrowsException(int quantity, decimal price, int productId, Type exceptionType)
        {
            Product? product = productId == 1 ? ProductMocks.GetProduct1() : null;

            // act / assert
            Assert.Throws(exceptionType, () => new OrderItem(quantity, price, product!));
        }
    }
}
