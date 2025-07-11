using Eshop.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        public static List<Category> Categories = new List<Category>
        {
            new Category(1, "Notebooks", "Lorem ipsum"),
            new Category(2, "Keyboards", "Lorem ipsum"),
            new Category(3, "Mice", "Lorem ipsum")
        };

        [HttpGet]
        public List<Category> GetCategories()
        {
            return Categories;
        }

        [HttpGet("{id}")]
        public Category GetCategory(int id)
        {
            return Categories.First(x => x.Id == id);
        }

        [HttpPost]
        public Category CreateCategory(string title, string description)
        {
            var newId = Categories.Max(x => x.Id) + 1;
            var newCategory = new Category(newId, title, description);

            Categories.Add(newCategory);

            return newCategory;
        }

        [HttpDelete("{id}")]
        public void DeleteCategory(int id)
        {
            var categoryToBeDeleted = Categories.First(x => x.Id == id);
            Categories.Remove(categoryToBeDeleted);
        }

        [HttpPut("{id}")]
        public Category UpdateCategory(int id, string title, string description)
        {
            var categoryToBeUpdated = Categories.First(x => x.Id == id);
            categoryToBeUpdated.Update(title, description);

            return categoryToBeUpdated;
        }
    }
}
