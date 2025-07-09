using Eshop.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private static List<Category> Categories = new List<Category>
        {
            new Category(1, "Notebooks", "Lorem ipsum"),
            new Category(2, "Keyboards", "Lorem ipsum"),
            new Category(3, "Mice", "Lorem ipsum")
        };

        private static List<Product> Products = new List<Product>
        {
            new Product(1, "Notebook Dell 125", "Brand new from Dell", 649.99m, Categories[0]),
            new Product(2, "Mouse Logitech 77", "Ergonomic and precise", 14.50m, Categories[2]),
            new Product(3, "Keyboard Genius OP", "Your keys to success", 20.00m, Categories[1])
        };

        [HttpGet]
        public List<Product> GetProducts()
        {
            return Products;
        }

        [HttpGet("{id}")]
        public Product GetProduct(int id)
        {
            return Products.First(x => x.Id == id);
        }

        [HttpPost]
        public Product CreateProduct(string title, string description, decimal price, int categoryId)
        {
            var category = Categories.First(x => x.Id == categoryId);
            var newId = Products.Max(x => x.Id) + 1;
            var newProduct = new Product(newId, title, description, price, category);
            
            Products.Add(newProduct);

            return newProduct;
        }

        [HttpDelete("{id}")]
        public void DeleteProduct(int id)
        {
            var productToBeDeleted = Products.First(x => x.Id == id);
            Products.Remove(productToBeDeleted);
        }

        [HttpPut("{id}")]
        public Product UpdateProduct(int id, string title, string description, decimal price, int categoryId)
        {
            var category = Categories.First(x => x.Id == categoryId);
            var productToBeUpdated = Products.First(x => x.Id == id);
            productToBeUpdated.Update(title, description, price, category);

            return productToBeUpdated;
        }
    }
}
