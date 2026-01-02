namespace Eshop.Domain
{
    public class Product
    {
        public Product(int id, string title, string description, decimal price, Category category)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            ValidateParameters(title, description, price, category);

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
        public Category Category { get; private set; }

        public void Update(string title, string description, decimal price, Category category)
        {
            ValidateParameters(title, description, price, category);

            Title = title;
            Description = description;
            Price = price;
            Category = category;
        }

        private static void ValidateParameters(string title, string description, decimal price, Category category)
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

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
        }
    }
}
