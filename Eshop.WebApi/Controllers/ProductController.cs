using Eshop.Domain;
using Eshop.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly EshopDbContext dbContext;

        public ProductController(EshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /*
        private static List<Product> Products = new List<Product>
        {
            new Product(1, "Notebook Dell 125", "Brand new from Dell", 649.99m, CategoryController.Categories[0]),
            new Product(2, "Mouse Logitech 77", "Ergonomic and precise", 14.50m, CategoryController.Categories[2]),
            new Product(3, "Keyboard Genius OP", "Your keys to success", 20.00m, CategoryController.Categories[1])
        };
        */

        [HttpGet]
        public List<Product> GetProducts()
        {
            return dbContext.ProductsViews.Include(x => x.Category).ToList();
        }

        [HttpGet("{id}")]
        public Product GetProduct(int id)
        {
            return dbContext.ProductsViews.Include(x => x.Category).First(x => x.Id == id);
        }

        [HttpPost]
        public Product CreateProduct(string title, string description, decimal price, int categoryId)
        {
            var category = dbContext.Categories.FirstOrDefault(x => x.Id == categoryId);
            var newProduct = new Product(0, title, description, price, category);
            
            dbContext.Products.Add(newProduct);
            dbContext.SaveChanges();

            return newProduct;
        }

        [HttpDelete("{id}")]
        public void DeleteProduct(int id)
        {
            var productToBeDeleted = dbContext.Products.First(x => x.Id == id);
            dbContext.Products.Remove(productToBeDeleted);
            dbContext.SaveChanges();
        }

        [HttpPut("{id}")]
        public Product UpdateProduct(int id, string title, string description, decimal price, int categoryId)
        {
            var category = dbContext.Categories.First(x => x.Id == categoryId);
            var productToBeUpdated = dbContext.Products.First(x => x.Id == id);
            productToBeUpdated.Update(title, description, price, category);
            dbContext.SaveChanges();

            return productToBeUpdated;
        }
    }
}
