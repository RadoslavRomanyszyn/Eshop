using Eshop.Domain;
using Eshop.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.WebApi.Features.Categories
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly EshopDbContext dbContext;

        public CategoryController(EshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public List<Category> GetCategories()
        {
            return dbContext.CategoriesViews.ToList();
        }

        [HttpGet("{id}")]
        public Category GetCategory(int id)
        {
            return dbContext.CategoriesViews.First(x => x.Id == id);
        }

        [HttpPost]
        public Category AddCategory(string title, string description)
        {
            var newCategory = new Category(0, title, description);

            dbContext.Categories.Add(newCategory);
            dbContext.SaveChanges();

            return newCategory;
        }

        [HttpDelete("{id}")]
        public void DeleteCategory(int id)
        {
            var categoryToBeDeleted = dbContext.Categories.First(x => x.Id == id);
            dbContext.Categories.Remove(categoryToBeDeleted);
            dbContext.SaveChanges();
        }

        [HttpPut("{id}")]
        public Category UpdateCategory(int id, string title, string description)
        {
            var categoryToBeUpdated = dbContext.Categories.First(x => x.Id == id);
            categoryToBeUpdated.Update(title, description);
            dbContext.SaveChanges();

            return categoryToBeUpdated;
        }
    }
}
