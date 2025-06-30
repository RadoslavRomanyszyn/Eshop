using Eshop.Domain;

var products = new List<Product>
{
    new Product("Notebook Dell 125", "Brand new from Dell", 649.99m),
    new Product ("Mouse Logitech 77", "Ergonomic and precise", 14.50m),
    new Product ("Keyboard Genius OP", "Your keys to success", 20.00m)
};

var product1 = products[0];
product1.ChangeTitle("Notebook Dell 120");

foreach (var product in products)
{
    Console.WriteLine($"{product.Title} . {product.Description} . {product.Price}");
}