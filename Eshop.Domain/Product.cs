namespace Eshop.Domain
{
    public class Product
    {
        public Product(int id, string title, string description, decimal price)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("id");
            }

            ValidateParameters(title, description, price);

            Id = id;
            Title = title;
            Description = description;
            Price = price;
        }

        public int Id { get; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public void Update(string title, string description, decimal price)
        {
            ValidateParameters(title, description, price);

            Title = title;
            Description = description;
            Price = price;
        }

        private static void ValidateParameters(string title, string description, decimal price)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("title");
            }

            if (title.Length > 50)
            {
                throw new ArgumentOutOfRangeException("title");
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("description");
            }

            if (description.Length > 500)
            {
                throw new ArgumentOutOfRangeException("description");
            }

            if (price < 0)
            {
                throw new ArgumentOutOfRangeException("price");
            }
        }
    }
}
