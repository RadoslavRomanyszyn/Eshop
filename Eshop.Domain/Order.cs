using System.Diagnostics.CodeAnalysis;

namespace Eshop.Domain
{
    public class Order
    {
        private readonly List<OrderItem> items = [];

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        [ExcludeFromCodeCoverage]
        private Order() { } // private ctor for persistence – Entity Framework
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public Order(int id, string customer, string address)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(id);

            ValidateParameters(customer, address);

            Id = id;
            CreatedAt = DateTime.UtcNow;
            Customer = customer;
            Address = address;
        }

        public int Id { get; }
        public DateTime CreatedAt { get; }
        public string Customer { get; }
        public string Address { get; }
        public IReadOnlyCollection<OrderItem> Items => items;

        public void AddItem(OrderItem item)
        {
            if (items.Any(x => x.Product.Id == item.Product.Id))
                throw new InvalidOperationException($"Product with Id[{item.Product.Id}] already exists in the order.");
            
            items.Add(item);
        }

        private static void ValidateParameters(string customer, string address)
        {
            if (string.IsNullOrEmpty(customer?.Trim()))
                throw new ArgumentNullException(nameof(customer));

            if (customer.Length > 50)
                throw new ArgumentOutOfRangeException(nameof(customer));

            if (string.IsNullOrEmpty(address?.Trim()))
                throw new ArgumentNullException(nameof(address));

            if (address.Length > 500)
                throw new ArgumentOutOfRangeException(nameof(address));
        }
    }
}
