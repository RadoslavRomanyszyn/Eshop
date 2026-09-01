using Eshop.Domain;

namespace Eshop.Tests.Common.Mocks
{
    public static class CategoryMocks
    {
        public static Category GetCategory1() => new(1, "Category 1", "Category Description 1");
        public static Category GetCategory2() => new(2, "Category 2", "Category Description 2");
        public static Category GetCategory3() => new(3, "Category 3", "Category Description 3");
    }
}
