using Eshop.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private static List<Product> Products = new List<Product>
        {
            new Product(1, "Notebook Dell 125", "Brand new from Dell", 649.99m),
            new Product(2, "Mouse Logitech 77", "Ergonomic and precise", 14.50m),
            new Product(3, "Keyboard Genius OP", "Your keys to success", 20.00m)
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
        public Product CreateProduct(string title, string description, decimal price)
        {
            var newId = Products.Max(x => x.Id) + 1;
            var newProduct = new Product(newId, title, description, price);
            
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
        public Product UpdateProduct(int id, string title, string description, decimal price)
        {
            var productToBeUpdated = Products.First(x => x.Id == id);
            productToBeUpdated.Update(title, description, price);

            return productToBeUpdated;
        }
    }
}
