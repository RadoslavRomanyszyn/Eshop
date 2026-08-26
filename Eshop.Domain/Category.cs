using System.Diagnostics.CodeAnalysis;

namespace Eshop.Domain
{
    public class Category
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        [ExcludeFromCodeCoverage]
        private Category() { } // private ctor for persistence – Entity Framework
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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
            if (string.IsNullOrEmpty(title?.Trim()))
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (title.Length > 50)
            {
                throw new ArgumentOutOfRangeException(nameof(title));
            }

            if (string.IsNullOrEmpty(description?.Trim()))
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
