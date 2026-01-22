namespace Eshop.Domain
{
    public class Product
    {
        // private ctor for Entity Framework
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private Product() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public Product(int id, string title, string description, decimal price, Category? category)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            ValidateParameters(title, description, price);

            Id = id;
            Title = title;
            Description = description;
            Price = price;
            Category = category;
        }

        public int Id { get; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public Category? Category { get; private set; }

        public void Update(string title, string description, decimal price, Category? category)
        {
            ValidateParameters(title, description, price);

            Title = title;
            Description = description;
            Price = price;
            Category = category;
        }

        private static void ValidateParameters(string title, string description, decimal price)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (title.Length > 50)
            {
                throw new ArgumentOutOfRangeException(nameof(title));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(nameof(description));
            }

            if (description.Length > 500)
            {
                throw new ArgumentOutOfRangeException(nameof(description));
            }

            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price));
            }
        }
    }
}
