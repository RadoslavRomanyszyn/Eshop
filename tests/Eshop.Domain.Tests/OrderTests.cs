using Eshop.Tests.Common.Mocks;
using Eshop.Tests.Common.Utils;

namespace Eshop.Domain.Tests
{
    [TestFixture]
    public class OrderTests
    {
        [Test]
        public void Order_WithValidParams_SetsPropertiesCorrectly()
        {
            // arrange
            var id = 0;
            var customer = StringUtils.GenerateRandomString(50);
            var address = StringUtils.GenerateRandomString(500);

            // act
            var sut = new Order(id, customer, address);

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(sut.Id, Is.EqualTo(id));
                Assert.That(sut.Customer, Is.EqualTo(customer));
                Assert.That(sut.Address, Is.EqualTo(address));
                Assert.That(sut.Items, Has.Count.EqualTo(0));
            });
        }

        [TestCase(-1, "Customer", "Address")]
        [TestCase(0, "kmP2xL8v9Q5zW1rY7bT4nU0qJ6sA3dF9hG2eK8xM4gs5u7u8jn4", "Address")]
        [TestCase(0, "Customer", "5vCQ5zW1rY7bT4nU0q6sA3dJF9hG2eK8xM4jN1B6mK4qW8tR5yU3iO7pL1kN9xV4zQ2mW8eR6tY3uI5oP0aS2dF4fG7hJ9kL1zX3cC5vB8nM2qW5eR8tY1uI4oP7aS9dF2fG5hJ8kL0zX2cC4vB7nM1qW3eR6tY9uI2oP5aS8dF1fG4hJ7kL9zX1cC3vB6nM0qW2eR5tY8uI1oP4aS7dF0fG3hJ6kL8zX0cC2vB5nM9qW1eR4tY7uI0oP3aS6dF9fG2hJ5kL7zX9cC1vB4nM8qW0eR3tY6uI9oP2aS5dF8fG1hJ4kL6zX8cC0vB3nM7qW9eR2tY5uI8oP1aS4dF7fG0hJ3kL5zX7cC9vB2nM6qW8eR1tY4uI7oP0aS3dF6fG9hJ2kL4zX6cC8vB1nM5qW7eR0tY3uI6oP9aS2dF5fG8hJ1kL3zX5cC7vB0nM4qW6eR9tY2uI5oP8aS1dF4fG7hJ0kL2zX4cC6vB9nM3qW5eR8tY1uI4oP")]
        public void Order_WithOutOfRangeParams_ThrowsException(int id, string customer, string address)
        {
            // act / assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Order(id, customer, address));
        }

        // Customer parameter tests
        [TestCase(null, "Address")]
        [TestCase(" ", "Address")]
        // Address parameter tests
        [TestCase("Customer", null)]
        [TestCase("Customer", " ")]
        public void Order_WithNullOrEmptyParams_ThrowsException(string? customer, string? address)
        {
            // act / assert
            Assert.Throws<ArgumentNullException>(() => new Order(0, customer!, address!));
        }

        [Test]
        public void Order_AddItem_AddsItemToOrder()
        {
            // arrange
            var sut = OrderMocks.GetOrder1();

            // act
            sut.AddItem(OrderItemMocks.GetOrderItem1());
            sut.AddItem(OrderItemMocks.GetOrderItem2());

            // assert
            Assert.That(sut.Items, Has.Count.EqualTo(2));
        }

        [Test]
        public void Order_AddItem_ThrowsException_WhenItemWithSameProductIdAlreadyExists()
        {
            // arrange
            var sut = OrderMocks.GetOrder1();
            var item1 = OrderItemMocks.GetOrderItem1();
            var item2 = new OrderItem(1, 9.99m, item1.Product);

            // act / assert
            sut.AddItem(item1);
            var exception = Assert.Throws<InvalidOperationException>(() => sut.AddItem(item2));
            StringAssert.Contains("already exists", exception.Message);
        }
    }
}
