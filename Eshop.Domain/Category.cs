namespace Eshop.Domain
{
    public class Category
    {
        public Category(int id, string title, string description)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            ValidateParameters(title, description);

            Id = id;
            Title = title;
            Description = description;
        }

        public int Id { get; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        public void Update(string title, string description)
        {
            ValidateParameters(title, description);

            Title = title;
            Description = description;
        }

        private static void ValidateParameters(string title, string description)
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
        }
    }
}
