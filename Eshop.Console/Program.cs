using Eshop.Domain;

var categories = new List<Category>
{
    new Category(1, "Notebooks", "Lorem ipsum"),
    new Category(2, "Keyboards", "Lorem ipsum"),
    new Category(3, "Mice", "Lorem ipsum")
};

var products = new List<Product>
{
    new Product(1, "Notebook Dell 125", "Brand new from Dell", 649.99m, categories[0]),
    new Product(2, "Mouse Logitech 77", "Ergonomic and precise", 14.50m, categories[2]),
    new Product(3, "Keyboard Genius OP", "Your keys to success", 20.00m, categories[1])
};

foreach (var category in categories)
{
    Console.WriteLine($"{category.Title} . {category.Description}");
}

foreach (var product in products)
{
    Console.WriteLine($"{product.Title} . {product.Description} . {product.Price} . {product.Category?.Title}");
}
