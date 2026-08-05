using Eshop.Domain;

Console.WriteLine("CATEGORIES");
var categories = new List<Category>
{
    new Category(1, "Laptops", "Laptops Desciption"),
    new Category(2, "Keyboards", "Keyboards Description"),
    new Category(3, "Mice", "Mice Description")
};
foreach (var category in categories)
{
    Console.WriteLine($"{category.Id} | {category.Title} | {category.Description}");
}
Console.WriteLine("---");

Console.WriteLine("PRODUCTS");
var products = new List<Product>
{
    new Product(1, "Laptop Dell 125", "Brand new from Dell", 649.99m, categories[0]),
    new Product(2, "Mouse Logitech 77", "Ergonomic and precise", 14.50m, categories[2]),
    new Product(3, "Keyboard Genius OP", "Your keys to success", 20.00m, categories[1])
};
foreach (var product in products)
{
    Console.WriteLine($"{product.Id} | {product.Title} | {product.Description} | {product.Price} | {product.Category?.Title}");
}
Console.WriteLine("---");

// Orders
Console.WriteLine("ORDERS");
var order = new Order(1, "John", "Address 1");
order.AddItem(new OrderItem(1, products[0].Price, products[0]));
order.AddItem(new OrderItem(4, products[1].Price, products[1]));
order.AddItem(new OrderItem(2, products[2].Price, products[2]));
Console.WriteLine($"{order.Id} | {order.Customer} | {order.Address} | {order.CreatedAt}");
Console.WriteLine("---");

Console.WriteLine("ORDER ITEMS");
foreach (var orderItem in order.Items)
{
    Console.WriteLine($"{orderItem.Quantity} | {orderItem.Price} | {orderItem.Product.Title}");
}
Console.WriteLine("---");
